using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class CreatedUnixTimestamp
    {
        public const int BytePosition = 48;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetCreatedUnixTimestamp(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length * 8).ToUInt32();
        }
    }
}
