using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class CharacterStatus
    {
        public const int BytePosition = 36;
        public const int BitPosition = BytePosition * sizeof(long);     // long is 8 bytes such that sizeof(long) returns 8
        public const int Length = 1;

        public static bool[] GetCharacterStatus(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length * 8);
        }
    }
}
