using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace FpgaTraceAnalysis
{
    class AgilentBinReader : DsoReader
    {
        public abstract class tPBWaveformAbstract: DsoWaveform
        {
            protected AgilentBinStructures.tPBWaveformHeader waveformHeader;

            protected tPBWaveformAbstract(AgilentBinStructures.tPBWaveformHeader header)
            {
                waveformHeader = header;
            }

            public override ePBWaveformType getWaveformType()
            {
                return (ePBWaveformType)waveformHeader.WaveformType;
            }

            public override double getTime(int index)
            {
                return ((double)index * waveformHeader.XIncrement) + waveformHeader.XOrigin;
            }

            // Not overwritten...
            //public abstract ePBDataType getDatasetType(int dataset);
            //public abstract double getVoltage(int index, int dataset);
            //public abstract int getCount(int dataset);
        }

        public class tPBWaveformNormal : tPBWaveformAbstract
        {
            private AgilentBinStructures.tPBWaveformDataHeader dataHeader;
            private float[] waveformData;

            public override ePBDataType getDatasetType(int dataset)
            {
                if (dataset > 0)
                {
                    return ePBDataType.PB_DATA_UNKNOWN;
                }

                return (ePBDataType)dataHeader.BufferType;
            }
            
            public override int getCount(int dataset)
            {
                return waveformData.Length;
            }

            public override double getVoltage(int index, int dataset)
            {
                return waveformData[index];
            }

            public tPBWaveformNormal(BinaryReader reader, AgilentBinStructures.tPBWaveformHeader waveformHeader)
                : base(waveformHeader)
            {
                dataHeader = readWaveformDataHeader(reader);

                try
                {
                    waveformData = readAnalogWaveform(reader, waveformHeader, dataHeader);
                }
                catch (Exception e)
                {
                    log = e.Message;
                }
            }
        }

        public class tPBWaveformPeakDetect : tPBWaveformAbstract
        {
            private AgilentBinStructures.tPBWaveformDataHeader dataHeader1;
            private AgilentBinStructures.tPBWaveformDataHeader dataHeader2;
            private float[] waveData1;
            private float[] waveData2;

            public override int getCount(int dataset)
            {
                if (dataset == 0)
                {
                    return waveData1.Length;
                }

                return waveData2.Length;
            }

            public override ePBDataType getDatasetType(int dataset)
            {
                if (dataset == 0)
                {
                    return (ePBDataType)dataHeader1.BufferType;
                }
                else if (dataset == 1)
                {
                    return (ePBDataType)dataHeader2.BufferType;
                }

                return ePBDataType.PB_DATA_UNKNOWN;
            }

            public override double getVoltage(int index, int dataset)
            {
                if (dataset == 0)
                {
                    return waveData1[index];
                }

                return waveData2[index];
            }

            public tPBWaveformPeakDetect(BinaryReader reader, AgilentBinStructures.tPBWaveformHeader waveformHeader)
                : base(waveformHeader)
            {
                dataHeader1 = readWaveformDataHeader(reader);
                waveData1 = readAnalogWaveform(reader, waveformHeader, dataHeader1);
                dataHeader2 = readWaveformDataHeader(reader);
                waveData2 = readAnalogWaveform(reader, waveformHeader, dataHeader2);
            }
        }

        class tPBWaveformHistogram : tPBWaveformAbstract
        {
            private AgilentBinStructures.tPBWaveformDataHeader dataHeader;
            private int[] histData;

            public override ePBDataType getDatasetType(int dataset)
            {
                if (dataset > 0)
                {
                    return ePBDataType.PB_DATA_UNKNOWN;
                }

                return (ePBDataType)dataHeader.BufferType;
            }

            public override int getCount(int dataset)
            {
                return histData.Length;
            }

            public override double getVoltage(int index, int dataset)
            {
                return histData[index];
            }

            private int[] readHistogramWaveform(BinaryReader reader, 
                AgilentBinStructures.tPBWaveformHeader header,
                AgilentBinStructures.tPBWaveformDataHeader dataHeader)
            {
                int[] pHistogramData = null;

                // Make sure everything is the expected format
                int actualNumberOfPoints = dataHeader.BufferSize / dataHeader.BytesPerPoint;

                if ((dataHeader.BytesPerPoint == 4) &&
                    ((ePBDataType)dataHeader.BufferType == ePBDataType.PB_DATA_COUNTS) &&
                    (actualNumberOfPoints == header.Points))
                {
                    // Now let's read in the histogram count data
                    pHistogramData = new int[actualNumberOfPoints];

                    for (int x = 0; x < actualNumberOfPoints; x++)
                    {
                        pHistogramData[x] = reader.ReadInt32();
                    }
                }
                else
                {
                    // ignore dataHeader.BufferSize because we didn't
                    // recognize the data format
                    reader.ReadBytes(dataHeader.BufferSize);
                }

                if ((ePBDataType)dataHeader.BufferType != ePBDataType.PB_DATA_COUNTS)
                {
                    throw new Exception("Invalid data type!");
                }

                if (actualNumberOfPoints != header.Points)
                {
                    throw new Exception("Inconsistent number of points!");
                }

                if (dataHeader.BytesPerPoint != 4)
                {
                    throw new Exception("Unrecognized point size!");
                }

                return pHistogramData;
            }

            public tPBWaveformHistogram(BinaryReader reader, AgilentBinStructures.tPBWaveformHeader waveformHeader)
                : base(waveformHeader)
            {
                dataHeader = readWaveformDataHeader(reader);

                try
                {
                    histData = readHistogramWaveform(reader, waveformHeader, dataHeader);
                }
                catch (Exception e)
                {
                    log = e.Message;
                }
            }
        }

        class tPBWaveformLogic : tPBWaveformAbstract
        {
            private byte[] podData1;
            private byte[] podData2;
            private AgilentBinStructures.tPBWaveformDataHeader dataHeader1;
            private AgilentBinStructures.tPBWaveformDataHeader dataHeader2;

            public override ePBDataType getDatasetType(int dataset)
            {
                if (dataset == 0)
                {
                    return (ePBDataType)dataHeader1.BufferType;
                }
                else if (dataset == 1)
                {
                    return (ePBDataType)dataHeader2.BufferType;
                }

                return ePBDataType.PB_DATA_UNKNOWN;
            }

            public override int getCount(int dataset)
            {
                if (dataset == 0)
                {
                    return podData1.Length;
                }

                return podData2.Length;
            }

            public override double getVoltage(int index, int dataset)
            {
                if (dataset == 0)
                {
                    return podData1[index];
                }

                return podData2[index];
            }

            private static byte[] readLogicWaveform(BinaryReader reader, 
                AgilentBinStructures.tPBWaveformHeader header, 
                AgilentBinStructures.tPBWaveformDataHeader dataHeader)
            {
                byte[] pLogicData = null;
                int actualNumberOfPoints = dataHeader.BufferSize / dataHeader.BytesPerPoint;

                if ((dataHeader.BytesPerPoint == 1) &&
                    ((ePBDataType)dataHeader.BufferType == ePBDataType.PB_DATA_LOGIC) &&
                    (actualNumberOfPoints == header.Points))
                {
                    // Now let's read in the logic data
                    pLogicData = new byte[actualNumberOfPoints];

                    for (int x = 0; x < actualNumberOfPoints; x++)
                    {
                        pLogicData[x] = reader.ReadByte();
                    }
                }
                else
                {
                    // ignore dataHeader.BufferSize because we didn't
                    // recognize the data format
                    reader.ReadBytes(dataHeader.BufferSize);
                }

                if ((ePBDataType)dataHeader.BufferType != ePBDataType.PB_DATA_LOGIC)
                {
                    throw new Exception("Invalid data type!");
                }

                if (actualNumberOfPoints != header.Points)
                {
                    throw new Exception("Inconsistent number of points!");
                }

                if (dataHeader.BytesPerPoint != 1)
                {
                    throw new Exception("Unrecognized point size!");
                }

                return pLogicData;
            }

            public tPBWaveformLogic(BinaryReader reader, AgilentBinStructures.tPBWaveformHeader waveformHeader)
                : base(waveformHeader)
            {
                if (waveformHeader.NWaveformBuffers == 2)
                {
                    // Two Pods stored
                    dataHeader1 = readWaveformDataHeader(reader);
                    try
                    {
                        podData1 = readLogicWaveform(reader, waveformHeader, dataHeader1);
                    }
                    catch (Exception e)
                    {
                        log = e.Message;
                    }

                    dataHeader2 = readWaveformDataHeader(reader);
                    try
                    {
                        podData2 = readLogicWaveform(reader, waveformHeader, dataHeader2);
                    }
                    catch (Exception e)
                    {
                        log = e.Message;
                    }
                }
                else
                {
                    // Only a single pod
                    dataHeader1 = readWaveformDataHeader(reader);
                    try
                    {
                        podData1 = readLogicWaveform(reader, waveformHeader, dataHeader1);
                    }
                    catch (Exception e)
                    {
                        log = e.Message;
                    }

                    dataHeader2.HeaderSize = 0;
                    dataHeader2.BytesPerPoint = 0;
                    dataHeader2.BufferSize = 0;
                    dataHeader2.BufferType = (short)ePBDataType.PB_DATA_UNKNOWN;
                    podData2 = null;
                }
            }
        }

        private static AgilentBinStructures.tPBFileHeader readPBFileHeader(BinaryReader reader)
        {
            AgilentBinStructures.tPBFileHeader fileHeader;
            byte[] hdrBytes = reader.ReadBytes(Marshal.SizeOf(typeof(AgilentBinStructures.tPBFileHeader)));

            GCHandle pinnedArray = GCHandle.Alloc(hdrBytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            fileHeader = (AgilentBinStructures.tPBFileHeader)Marshal.PtrToStructure(pointer, typeof(AgilentBinStructures.tPBFileHeader));

            pinnedArray.Free();

            return fileHeader;
        }

        private static AgilentBinStructures.tPBWaveformHeader readPBWaveformHeader(BinaryReader reader)
        {
            AgilentBinStructures.tPBWaveformHeader waveformHeader;
            byte[] hdrBytes = reader.ReadBytes(Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformHeader)));

            GCHandle pinnedArray = GCHandle.Alloc(hdrBytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            waveformHeader = (AgilentBinStructures.tPBWaveformHeader)Marshal.PtrToStructure(pointer, typeof(AgilentBinStructures.tPBWaveformHeader));

            pinnedArray.Free();

            return waveformHeader;
        }

        private static AgilentBinStructures.tPBWaveformDataHeader readWaveformDataHeader(BinaryReader reader)
        {
            AgilentBinStructures.tPBWaveformDataHeader waveformDataHeader;
            byte[] hdrBytes = reader.ReadBytes(Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformDataHeader)));

            GCHandle pinnedArray = GCHandle.Alloc(hdrBytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            waveformDataHeader = (AgilentBinStructures.tPBWaveformDataHeader)Marshal.PtrToStructure(pointer, typeof(AgilentBinStructures.tPBWaveformDataHeader));

            pinnedArray.Free();

            int extraDataSize = waveformDataHeader.HeaderSize - Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformDataHeader));

            byte[] extraBytes = reader.ReadBytes(extraDataSize);

            // Just is case WaveformType has been enhanced
            if (waveformDataHeader.BufferType > (short)ePBDataType.PB_DATA_LOGIC)
            {
                waveformDataHeader.BufferType = (short)ePBDataType.PB_DATA_UNKNOWN;
            }

            return waveformDataHeader;
        }

        private static float[] readAnalogWaveform(BinaryReader reader, 
            AgilentBinStructures.tPBWaveformHeader header,
            AgilentBinStructures.tPBWaveformDataHeader dataHeader)
        {
            // Make sure everything is the expected format
            int actualNumberOfPoints = dataHeader.BufferSize / dataHeader.BytesPerPoint;
            float[] pWaveformData = null;

            bool validDataType = ((ePBDataType)dataHeader.BufferType == ePBDataType.PB_DATA_NORMAL) ||
                            ((ePBDataType)dataHeader.BufferType == ePBDataType.PB_DATA_MIN) ||
                            ((ePBDataType)dataHeader.BufferType == ePBDataType.PB_DATA_MAX);

            if ((dataHeader.BytesPerPoint == 4) &&
                validDataType &&
                (actualNumberOfPoints == header.Points))
            {
                // Now let's read in the data
                pWaveformData = new float[actualNumberOfPoints];

                for (int x = 0; x < actualNumberOfPoints; x++)
                {
                    pWaveformData[x] = reader.ReadSingle();
                }
            }
            else
            {
                // ignore dataHeader.BufferSize because we didn't
                // recognize the data format
                reader.ReadBytes(dataHeader.BufferSize);
            }

            if (!validDataType)
            {
                throw new Exception("Invalid data type!");
            }

            if (actualNumberOfPoints != header.Points)
            {
                throw new Exception("Inconsistent number of points!");
            }

            if (dataHeader.BytesPerPoint != 4)
            {
                throw new Exception("Unrecognized point size!");
            }

            return pWaveformData;
        }

        private tPBWaveformAbstract readWaveform(BinaryReader reader)
        {
            AgilentBinStructures.tPBWaveformHeader waveformHeader = readPBWaveformHeader(reader);

            if (waveformHeader.HeaderSize > 0)
            {
                switch ((ePBWaveformType)waveformHeader.WaveformType)
                {
                    case ePBWaveformType.PB_NORMAL:
                    case ePBWaveformType.PB_AVERAGE:
                        return new tPBWaveformNormal(reader, waveformHeader);
                    case ePBWaveformType.PB_PEAK_DETECT:
                        return new tPBWaveformPeakDetect(reader, waveformHeader);
                    case ePBWaveformType.PB_HORZ_HISTOGRAM:
                    case ePBWaveformType.PB_VERT_HISTOGRAM:
                        return new tPBWaveformHistogram(reader, waveformHeader);
                    case ePBWaveformType.PB_LOGIC:
                        return new tPBWaveformLogic(reader, waveformHeader);
                    case ePBWaveformType.PB_UNKNOWN:
                    default:
                        // we don't know what this is so skip it
                        AgilentBinStructures.tPBWaveformDataHeader dataHdr = readWaveformDataHeader(reader);
                        reader.ReadBytes(dataHdr.BufferSize);
                        break;
                }
            }

            return null;
        }

        private FileStream binFile;
        private BinaryReader binReader;

        public AgilentBinReader(string file) : base()
        {
            AgilentBinStructures.tPBFileHeader binHeader;

            binFile = File.Open(file, FileMode.Open, FileAccess.Read);
            binReader = new BinaryReader(binFile);
            binHeader = readPBFileHeader(binReader);

            if ((binHeader.cookie_[0] == AgilentBinStructures.PB_COOKIE[0]) && 
                (binHeader.cookie_[1] == AgilentBinStructures.PB_COOKIE[1]))
            {

                for (int w = 0; w < binHeader.numberOfWaveforms_; w++)
                {
                    tPBWaveformAbstract waveform = readWaveform(binReader);

                    if (waveform != null)
                    {
                        waveforms.Add(waveform);
                    }
                }
            }
            else
            {
                throw new Exception("Unrecognized file header magic!");
            }

            binReader.Close();
            binFile.Close();
        }
    }
}
