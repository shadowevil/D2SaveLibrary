using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Other
{
    public class NPC
    {
        private readonly byte[] Marker = new byte[2] { 0x01, 0x77 };
        public UInt16 Size = UInt16.MaxValue;
        public byte[] NormalIntroductionBytes { get; set; } = new byte[48];
        public byte[] NightmareIntroductionBytes { get; set; } = new byte[48];
        public byte[] HellIntroductionBytes { get; set; } = new byte[48];

        // Might not even be used?
        public byte[] NormalCongratulationBytes { get; set; } = new byte[48];
        public byte[] NightmareCongratulationBytes { get; set; } = new byte[48];
        public byte[] HellCongratulationBytes { get; set; } = new byte[48];

        public NPC() { }

#warning NPC structure does not seem right, notes indicate NPC structure is 52 bytes then multiplied by 3 for each difficulty. However attributes start at 765. 765-713 = 52 thus you cannot repeat multiple times. What even.
        public static NPC Read(BitwiseBinaryReader mainReader)
        {
            NPC npc = new NPC();

            mainReader.SetBytePosition(OtherOffsets.OFFSET_NPC_MARKER.Offset);
            byte[] markerCheck = mainReader.ReadBytes(OtherOffsets.OFFSET_NPC_MARKER.ByteLength);
            if (npc.Marker[0] != markerCheck[0] && npc.Marker[1] != markerCheck[1])
                throw new OffsetException("Unable to verify NPC Marker offset, corrupt save?");

            mainReader.SetBytePosition(OtherOffsets.OFFSET_NPC_SIZE.Offset);
            npc.Size = mainReader.ReadBits(OtherOffsets.OFFSET_NPC_SIZE.BitLength).ToUInt16();

            // This doesn't seem right at all what so ever

            mainReader.SetBytePosition(OtherOffsets.OFFSET_NPC_NORMAL_INTRODUCTION.Offset);
            npc.NormalIntroductionBytes = mainReader.ReadBits(OtherOffsets.OFFSET_NPC_NORMAL_INTRODUCTION.BitLength).ToBytes();

            //mainReader.SetBytePosition(OtherOffsets.OFFSET_NPC_NIGHTMARE_INTRODUCTION.Offset);
            //npc.NightmareIntroductionBytes = mainReader.ReadBits(OtherOffsets.OFFSET_NPC_NIGHTMARE_INTRODUCTION.BitLength).ToBytes();

            //mainReader.SetBytePosition(OtherOffsets.OFFSET_NPC_HELL_INTRODUCTION.Offset);
            //npc.HellIntroductionBytes = mainReader.ReadBits(OtherOffsets.OFFSET_NPC_HELL_INTRODUCTION.BitLength).ToBytes();

            return npc;
        }
    }
}
