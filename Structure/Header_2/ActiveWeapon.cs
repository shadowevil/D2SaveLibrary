using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class ActiveWeapon
    {
        public const int BytePosition = 16;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static UInt32 GetActiveWeapon(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return ByteArrayConverter.ToUInt32(reader.ReadBits(Length * 8).ToBytes());
        }
    }
}
