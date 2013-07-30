using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FpgaTraceAnalysis
{
    class DsoReader
    {
        public enum ePBWaveformType
        {
            PB_UNKNOWN,
            PB_NORMAL,
            PB_PEAK_DETECT,
            PB_AVERAGE,
            PB_HORZ_HISTOGRAM,
            PB_VERT_HISTOGRAM,
            PB_LOGIC
        }

        public enum ePBDataType
        {
            PB_DATA_UNKNOWN,
            PB_DATA_NORMAL,
            PB_DATA_MAX,
            PB_DATA_MIN,
            PB_DATA_TIME,
            PB_DATA_COUNTS,
            PB_DATA_LOGIC
        }

        public abstract class DsoWaveform
        {
            protected string log;

            protected DsoWaveform()
            {
                log = "No error";
            }

            public abstract ePBWaveformType getWaveformType();
            public abstract ePBDataType getDatasetType(int dataset);
            public abstract double getVoltage(int index, int dataset);
            public abstract int getCount(int dataset);
            public abstract double getTime(int index);
        }

        public List<DsoWaveform> waveforms = null;

        public DsoReader()
        {
            waveforms = new List<DsoWaveform>();
        }
    }
}
