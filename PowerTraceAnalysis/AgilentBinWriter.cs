using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace FpgaTraceAnalysis
{
    class AgilentBinWriter
    {
        private FileStream binFile;
        private BinaryWriter binWriter;
        private AgilentBinStructures.tPBFileHeader binHeader;

        public class tPBWaveformNormal : DsoReader.DsoWaveform
        {
            private float[] waveformData;

            public override FpgaTraceAnalysis.DsoReader.ePBWaveformType getWaveformType()
            {
                return FpgaTraceAnalysis.DsoReader.ePBWaveformType.PB_NORMAL;
            }

            public override double getTime(int index)
            {
                return 0;
            }

            public override FpgaTraceAnalysis.DsoReader.ePBDataType getDatasetType(int dataset)
            {
                if (dataset > 0)
                {
                    return FpgaTraceAnalysis.DsoReader.ePBDataType.PB_DATA_UNKNOWN;
                }

                return FpgaTraceAnalysis.DsoReader.ePBDataType.PB_DATA_NORMAL;
            }

            public override int getCount(int dataset)
            {
                return waveformData.Length;
            }

            public override double getVoltage(int index, int dataset)
            {
                return waveformData[index];
            }

            public void setCount(int count, int dataset)
            {
                waveformData = new float[count];
            }

            public void setVoltage(float value, int index, int dataset)
            {
                waveformData[index] = value;
            }

            public tPBWaveformNormal()
                : base()
            {
                waveformData = null;
            }
        }

        private void writePBWaveformHeader(AgilentBinStructures.tPBWaveformHeader waveformHeader)
        {
            byte[] hdrBytes = new byte[Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformHeader))];

            GCHandle pinnedArray = GCHandle.Alloc(hdrBytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            Marshal.StructureToPtr(waveformHeader, pointer, false);

            binWriter.Write(hdrBytes);

            pinnedArray.Free();
        }

        private void writeWaveformDataHeader(AgilentBinStructures.tPBWaveformDataHeader waveformDataHeader)
        {
            byte[] hdrBytes = new byte[Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformDataHeader))];

            GCHandle pinnedArray = GCHandle.Alloc(hdrBytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            Marshal.StructureToPtr(waveformDataHeader, pointer, false);

            binWriter.Write(hdrBytes);

            pinnedArray.Free();
        }

        private void writePBFileHeader(AgilentBinStructures.tPBFileHeader header)
        {
            byte[] hdrBytes = new byte[Marshal.SizeOf(typeof(AgilentBinStructures.tPBFileHeader))];

            GCHandle pinnedArray = GCHandle.Alloc(hdrBytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            Marshal.StructureToPtr(header, pointer, false);

            binWriter.Write(hdrBytes);

            pinnedArray.Free();
        }

        public void writeWaveform(DsoReader.DsoWaveform waveform)
        {
            AgilentBinStructures.tPBWaveformHeader waveformHeader = new AgilentBinStructures.tPBWaveformHeader(); ;
            binHeader.numberOfWaveforms_++;
            binHeader.fileSize_ += Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformHeader));

            // Count the number of waveforms
            int numDatasets = 0;
            int numPoints = 0;

            DsoReader.ePBDataType wavet = waveform.getDatasetType(numDatasets);

            while (wavet == DsoReader.ePBDataType.PB_DATA_NORMAL)
            {
                numPoints = waveform.getCount( numDatasets);

                numDatasets++;
                wavet = waveform.getDatasetType(numDatasets);
            }
            
            waveformHeader.Date = new char[AgilentBinStructures.DATE_TIME_STRING_LENGTH];
            waveformHeader.Time = new char[AgilentBinStructures.DATE_TIME_STRING_LENGTH];
            waveformHeader.Frame = new char[AgilentBinStructures.FRAME_STRING_LENGTH];
            waveformHeader.WaveformLabel = new char[AgilentBinStructures.SIGNAL_STRING_LENGTH];
            waveformHeader.Count = numDatasets;
            waveformHeader.Points = numPoints;
            waveformHeader.WaveformType = (int)DsoReader.ePBWaveformType.PB_NORMAL;
            waveformHeader.HeaderSize = Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformHeader));

            writePBWaveformHeader(waveformHeader);

            int d = 0;

            wavet = waveform.getDatasetType(d);

            while (wavet == DsoReader.ePBDataType.PB_DATA_NORMAL)
            {
                AgilentBinStructures.tPBWaveformDataHeader waveformDataHeader = new AgilentBinStructures.tPBWaveformDataHeader();
                int size = waveform.getCount(d);

                waveformDataHeader.HeaderSize = Marshal.SizeOf(typeof(AgilentBinStructures.tPBWaveformDataHeader));
                waveformDataHeader.BufferType = (short)wavet;
                waveformDataHeader.BytesPerPoint = (short)Marshal.SizeOf(typeof(float));
                waveformDataHeader.BufferSize = size * waveformDataHeader.BytesPerPoint;
                binHeader.fileSize_ += waveformDataHeader.HeaderSize + waveformDataHeader.BufferSize;

                writeWaveformDataHeader(waveformDataHeader);

                for (int i = 0; i < size; i++)
                {
                    float y = (float)waveform.getVoltage(i, d);
                    binWriter.Write(y);
                }

                d++;
                wavet = waveform.getDatasetType(d);
            }
        }

        public void close()
        {
            binWriter.Seek(0, SeekOrigin.Begin);

            // re-write the header with the proper sizes
            writePBFileHeader(binHeader);

            binWriter.Close();
            binFile.Close();
        }

        public AgilentBinWriter(string file)
        {
            binHeader = new AgilentBinStructures.tPBFileHeader();

            binFile = File.Open(file, FileMode.Create, FileAccess.Write);
            binWriter = new BinaryWriter(binFile);

            binHeader.cookie_ = new char[2];
            binHeader.cookie_[0] = AgilentBinStructures.PB_COOKIE[0];
            binHeader.cookie_[1] = AgilentBinStructures.PB_COOKIE[1];
            binHeader.version_ = new char[2];
            binHeader.version_[0] = AgilentBinStructures.PB_VERSION[0];
            binHeader.version_[1] = AgilentBinStructures.PB_VERSION[1];
            binHeader.numberOfWaveforms_ = 0;
            binHeader.fileSize_ = Marshal.SizeOf(typeof(AgilentBinStructures.tPBFileHeader));

            writePBFileHeader(binHeader);
        }
    }
}
