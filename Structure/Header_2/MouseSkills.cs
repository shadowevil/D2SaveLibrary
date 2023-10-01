using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class LeftMouseSkill
    {
        public const int BytePosition = 120;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetLeftMouseSkill(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length * 8).ToUInt32();
        }
    }

    public static class RightMouseSkill
    {
        public const int BytePosition = 124;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetRightMouseSkill(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length * 8).ToUInt32();
        }
    }


    public static class SwitchLeftMouseSkill
    {
        public const int BytePosition = 128;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetSwitchLeftMouseSkill(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length*8).ToUInt32();
        }
    }

    public static class SwitchRightMouseSkill
    {
        public const int BytePosition = 132;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetSwitchRightMouseSkill(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length*8).ToUInt32();
        }
    }
}
