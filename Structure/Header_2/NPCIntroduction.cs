using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class NPCIntroduction
    {
        public const int BytePosition = 713;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 52;

        public static byte[]? npcIntroductionBytes { get; set; } = null;

        public static byte[] GetNPCIntroduction(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            byte[] header = reader.ReadBits(32).ToBytes();
            if (header[0] != 0x01 && header[1] != 0x77)
                throw new Exception("NPC Introduction header not found!");

            reader.SetBitPosition(BitPosition);
            npcIntroductionBytes = reader.ReadBits(Length*8).ToBytes();

            return npcIntroductionBytes;
        }
    }
}
