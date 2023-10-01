using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class CharacterProgression
    {
        public const int BytePosition = 37;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 1;

        public static bool[] GetCharacterProgression(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length * 8);
        }
    }
}
