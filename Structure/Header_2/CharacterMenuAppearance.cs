using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class CharacterMenuAppearance
    {
        public const int BytePosition = 126;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 32;

        public static byte[] GetCharacterMenuAppearance(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length*8).ToBytes();
        }
    }

    public static class D2RCharacterMenuApperance
    {
        public const int BytePosition = 219;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 48;

        public static byte[] GetD2RCharacterMenuAppearance(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return reader.ReadBits(Length*8).ToBytes();
        }
    }
}
