using D2SLib2.BinaryHandler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Version
    {
        public const int BytePosition = 4;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetVersion(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length*8).ToUInt32();
        }
    }

    public enum FileVersion
    {
        V00_To_06 = 71,
        V107_To_V1_08 = 87,
        V108 = 89,
        V109 = 92,
        V110_To_114d = 96,
        D2R_V10x_To_11x = 97,
        D2R_V12x_To_V13x = 98,
        D2R_V14x_LATEST = 99
    }
}
