using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class CharacterLevel
    {
        public const int BytePosition = 43;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 1;

        public static byte GetCharacterLevel(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(8).ToByte();
        }
    }
}
