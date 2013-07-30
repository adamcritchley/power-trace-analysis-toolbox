using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FpgaTraceAnalysis
{
    class AgilentBinStructures
    {
        //*****************************************************************************
        //
        //  Description: This file is broken into three sections
        //    Section 1: Data Structures to describe Infiniium Public Waveform File
        //    Section 2: Functions to correctly read .bin files
        //    Section 3: Functions to convert a .bin file to .csv file
        //*****************************************************************************
        //
        //  Description: Structures and Enumerations to describe Infiniium
        //               Public Waveform File - using these structures assumes
        //               a 32-Bit x86 Compiler
        //
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct tPBFileHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] cookie_;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] version_;
            public int fileSize_;
            public int numberOfWaveforms_;
        }

        public static char[] PB_COOKIE = { 'A', 'G' };
        public static char[] PB_VERSION = { '1', '0' };
        public const int DATE_TIME_STRING_LENGTH = (16);
        public const int FRAME_STRING_LENGTH = (24);
        public const int SIGNAL_STRING_LENGTH = (16);

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct tPBWaveformHeader
        {
            public int HeaderSize;
            public int WaveformType;
            public int NWaveformBuffers;
            public int Points;
            public int Count;
            public float XDisplayRange;
            public double XDisplayOrigin;
            public double XIncrement;
            public double XOrigin;
            public int XUnits;
            public int YUnits;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DATE_TIME_STRING_LENGTH)]
            public char[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DATE_TIME_STRING_LENGTH)]
            public char[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FRAME_STRING_LENGTH)]
            public char[] Frame;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SIGNAL_STRING_LENGTH)]
            public char[] WaveformLabel;
            public double TimeTag;
            public uint SegmentIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct tPBWaveformDataHeader
        {
            public int HeaderSize;
            public short BufferType;
            public short BytesPerPoint;
            public int BufferSize;
        }
    }
}
