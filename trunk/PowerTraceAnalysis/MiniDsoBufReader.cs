using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FpgaTraceAnalysis
{
    class MiniDsoBufReader : DsoReader
    {
        private FileStream bufFile;
        private BinaryReader bufReader;

        public class dsoOptions
        {
            public dsoOptions()
            {
                inputRange = 1.024;
                bitDepth = 8;
                loadTrack1 = true;
                loadTrack2 = false;
                loadTrack3 = false;
                loadTrack4 = false;
                sampleTime = 0.1;
            }

            public double inputRange;
            public int bitDepth;
            public bool loadTrack1;
            public bool loadTrack2;
            public bool loadTrack3;
            public bool loadTrack4;
            public double sampleTime;
        }

        public class MiniDsoWaveform : DsoWaveform
        {
            private dsoOptions waveOpts;
            private int waveSize;

            private double[] t1Voltage = null;
            private double[] t2Voltage = null;
            private double[] t3Voltage = null;
            private double[] t4Voltage = null;
            private double ka1;
            private double ka2;
            private double kb1;
            private double kb2;

            public MiniDsoWaveform(dsoOptions opts)
            {
                waveOpts = opts;
            }

            public override ePBWaveformType getWaveformType()
            {
                return ePBWaveformType.PB_NORMAL;
            }

            public override double getTime(int index)
            {
                return ((double)index * waveOpts.sampleTime);
            }

            public override ePBDataType getDatasetType(int dataset)
            {
                if (waveOpts.loadTrack1 && dataset == 0)
                {
                    return ePBDataType.PB_DATA_NORMAL;
                }
                if (waveOpts.loadTrack2 && dataset == 1)
                {
                    return ePBDataType.PB_DATA_NORMAL;
                }
                if (waveOpts.loadTrack3 && dataset == 2)
                {
                    return ePBDataType.PB_DATA_NORMAL;
                }
                if (waveOpts.loadTrack4 && dataset == 3)
                {
                    return ePBDataType.PB_DATA_NORMAL;
                }

                return ePBDataType.PB_DATA_UNKNOWN;
            }

            public override double getVoltage(int index, int dataset)
            {
                double realVoltage = 0.0;

                if (dataset == 0)
                {
                    double voltage = t1Voltage[index];
                    double countVoltage = ((ka2 * voltage) + 512) / 1024 + ka1;
                    realVoltage = countVoltage * (waveOpts.inputRange / (Math.Pow(2, waveOpts.bitDepth) - 1));
                }
                else
                    if (dataset == 1)
                    {
                        double voltage = t2Voltage[index];
                        double countVoltage = ((kb2 * voltage) + 512) / 1024 + kb1;
                        realVoltage = countVoltage * (waveOpts.inputRange / (Math.Pow(2, waveOpts.bitDepth) - 1));
                    }
                    else
                        if (dataset == 2)
                        {
                            realVoltage = t3Voltage[index];
                        }
                        else
                            if (dataset == 3)
                            {
                                realVoltage = t4Voltage[index];
                            }

                return realVoltage;
            }

            public override int getCount(int dataset)
            {
                return waveSize;
            }

            public void setValue(int index, int dataset, double voltage)
            {
                if (dataset == 0)
                {
                    t1Voltage[index] = voltage;
                }
                else
                    if (dataset == 1)
                    {
                        t2Voltage[index] = voltage;
                    }
                    else
                        if (dataset == 2)
                        {
                            t3Voltage[index] = voltage;
                        }
                        else
                            if (dataset == 3)
                            {
                                t4Voltage[index] = voltage;
                            }
            }

            public void setCount(int size)
            {
                waveSize = size;
                if (waveOpts.loadTrack1)
                {
                    t1Voltage = new double[size];
                }
                if (waveOpts.loadTrack2)
                {
                    t2Voltage = new double[size];
                }
                if (waveOpts.loadTrack3)
                {
                    t3Voltage = new double[size];
                }
                if (waveOpts.loadTrack4)
                {
                    t4Voltage = new double[size];
                }
            }

            public void setKaRanges(double ka1, double ka2)
            {
                this.ka1 = ka1;
                this.ka2 = ka2;
            }

            public void setKbRanges(double kb1, double kb2)
            {
                this.kb1 = kb1;
                this.kb2 = kb2;
            }
        }

        public MiniDsoBufReader(string file, dsoOptions opts)
            : base()
        {
            bufFile = File.Open(file, FileMode.Open, FileAccess.Read);
            bufReader = new BinaryReader(bufFile);

            MiniDsoWaveform waveform = new MiniDsoWaveform(opts);

            waveform.setCount(4096);

            // Read the voltage counts...
            for (int i = 0; i < 4096; i++)
            {
                double v = bufReader.ReadByte();
                if (opts.loadTrack1 == true)
                {
                    waveform.setValue(i, 0, v);
                }
                v = bufReader.ReadByte();
                if (opts.loadTrack2 == true)
                {
                    waveform.setValue(i, 1, v);
                }
                v = bufReader.ReadByte();
                if (opts.loadTrack3 == true)
                {
                    waveform.setValue(i, 2, v);
                }
                v = bufReader.ReadByte();
                if (opts.loadTrack4 == true)
                {
                    waveform.setValue(i, 3, v);
                }
            }

            // Skips the tables...
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    bufReader.ReadUInt16();
                }
            }

            bufReader.ReadUInt16();
            bufReader.ReadUInt16();

            // Read the K ranges...
            waveform.setKaRanges(bufReader.ReadUInt16(), bufReader.ReadUInt16());
            waveform.setKbRanges(bufReader.ReadUInt16(), bufReader.ReadUInt16());

            waveforms.Add(waveform);

            bufReader.Close();
            bufFile.Close();
        }
    }
}
