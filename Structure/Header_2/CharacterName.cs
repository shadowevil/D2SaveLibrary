using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class CharacterName
    {
        public const int BytePosition = 267;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 16;

        public static string GetCharacterName(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<Length;i++)
            {
                if (reader.PeekBits(8).ToChar() == '\0') break;
                sb.Append(reader.ReadBits(8).ToChar());
            }
            return sb.ToString();
        }
    }
}
