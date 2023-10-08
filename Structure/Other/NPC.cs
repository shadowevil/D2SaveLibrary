using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Player.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Other
{
    public class NPCIntroduction
    {
        private readonly byte[] Marker = new byte[2] { 0x01, 0x77 };
        public UInt16 Size = UInt16.MaxValue;

        public DifficultyStruct? NormalIntro { get; set; } = null;
        public DifficultyStruct? NightmareIntro { get; set; } = null;
        public DifficultyStruct? HellIntro { get; set; } = null;
        public DifficultyStruct? NormalCongrats { get; set; } = null;
        public DifficultyStruct? NightmareCongrats { get; set; } = null;
        public DifficultyStruct? HellCongrats { get; set; } = null;

        public NPCIntroduction() { }

        public static NPCIntroduction Read(BitwiseBinaryReader mainReader)
        {
            NPCIntroduction npc = new NPCIntroduction();

            byte[]? markerCheck = mainReader.Read<byte[]>(OtherOffsets.OFFSET_NPC_MARKER);
            Logger.WriteSection(mainReader, OtherOffsets.OFFSET_NPC_MARKER.BitLength, $"NPC Intro/Congrats Marker: 0x{markerCheck?[0].ToString("X2")} 0x{markerCheck?[1].ToString("X2")}");
            if (npc.Marker[0] != markerCheck![0] && npc.Marker[1] != markerCheck![1])
                throw new OffsetException("Unable to verify NPC Marker offset, corrupt save?");

            npc.Size = mainReader.Read<UInt16>(OtherOffsets.OFFSET_NPC_SIZE);
            Logger.WriteSection(mainReader, OtherOffsets.OFFSET_NPC_SIZE.BitLength, $"NPC Offset Length: {npc.Size}");

            npc.NormalIntro = new DifficultyStruct(mainReader, OtherOffsets.OFFSET_NPC_INTRO_NORMAL);
            npc.NightmareIntro = new DifficultyStruct(mainReader, OtherOffsets.OFFSET_NPC_INTRO_NIGHTMARE);
            npc.HellIntro = new DifficultyStruct(mainReader, OtherOffsets.OFFSET_NPC_INTRO_HELL);

            npc.NormalCongrats = new DifficultyStruct(mainReader, OtherOffsets.OFFSET_NPC_CONGRATS_NORMAL);
            npc.NightmareCongrats = new DifficultyStruct(mainReader, OtherOffsets.OFFSET_NPC_CONGRATS_NIGHTMARE);
            npc.HellCongrats = new DifficultyStruct(mainReader, OtherOffsets.OFFSET_NPC_CONGRATS_HELL);

            return npc;
        }

        public bool Write(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != OtherOffsets.OFFSET_NPC_MARKER.Offset)
                return false;
           writer.WriteBits(Marker.ToBits());
           writer.WriteBits(Size.ToBits());

            NormalIntro!.Write(writer);
            NightmareIntro!.Write(writer);
            HellIntro!.Write(writer);
            NormalCongrats!.Write(writer);
            NightmareCongrats!.Write(writer);
            HellCongrats!.Write(writer);

            return true;
        }
    }

    public class DifficultyStruct
    {
        private Bit[] Flags = new Bit[64];

        public bool warriv_act_ii    { get => Flags[0];  set => Flags[0]  = new Bit(value); }     // 0  offset
        public bool charsi           { get => Flags[2];  set => Flags[2]  = new Bit(value); }     // 2  offset
        public bool warriv_act_i     { get => Flags[3];  set => Flags[3]  = new Bit(value); }     // 3  offset
        public bool kashya           { get => Flags[4];  set => Flags[4]  = new Bit(value); }     // 4  offset
        public bool akara            { get => Flags[5];  set => Flags[5]  = new Bit(value); }     // 5  offset
        public bool gheed            { get => Flags[6];  set => Flags[6]  = new Bit(value); }     // 6  offset
        public bool greiz            { get => Flags[8];  set => Flags[8]  = new Bit(value); }     // 8  offset
        public bool jerhyn           { get => Flags[9];  set => Flags[9]  = new Bit(value); }     // 9  offset
        public bool meshif_act_ii    { get => Flags[10]; set => Flags[10] = new Bit(value); }     // 10 offset
        public bool geglash          { get => Flags[11]; set => Flags[11] = new Bit(value); }     // 11 offset
        public bool lysander         { get => Flags[12]; set => Flags[12] = new Bit(value); }     // 12 offset
        public bool fara             { get => Flags[13]; set => Flags[13] = new Bit(value); }     // 13 offset
        public bool drogan           { get => Flags[14]; set => Flags[14] = new Bit(value); }     // 14 offset
        public bool alkor            { get => Flags[16]; set => Flags[16] = new Bit(value); }     // 16 offset
        public bool hratli           { get => Flags[17]; set => Flags[17] = new Bit(value); }     // 17 offset
        public bool ashera           { get => Flags[18]; set => Flags[18] = new Bit(value); }     // 18 offset
        public bool cain_act_iii     { get => Flags[21]; set => Flags[21] = new Bit(value); }     // 21 offset
        public bool elzix            { get => Flags[23]; set => Flags[23] = new Bit(value); }     // 23 offset
        public bool malah            { get => Flags[24]; set => Flags[24] = new Bit(value); }     // 24 offset
        public bool anya             { get => Flags[25]; set => Flags[25] = new Bit(value); }     // 25 offset
        public bool natalya          { get => Flags[27]; set => Flags[27] = new Bit(value); }     // 27 offset
        public bool meshif_act_iii   { get => Flags[28]; set => Flags[28] = new Bit(value); }     // 28 offset
        public bool ormus            { get => Flags[31]; set => Flags[31] = new Bit(value); }     // 31 offset
        public bool cain_act_v       { get => Flags[37]; set => Flags[37] = new Bit(value); }     // 37 offset
        public bool qualkehk         { get => Flags[38]; set => Flags[38] = new Bit(value); }     // 38 offset
        public bool nihlathak        { get => Flags[39]; set => Flags[39] = new Bit(value); }     // 39 offset

        public DifficultyStruct(BitwiseBinaryReader mainReader, OffsetStruct offset)
        {
            if (mainReader.bytePosition != offset.Offset)
                throw new Exception("Byte position not correct for NPC Introduction or Congratulations difficulty");
            Flags = mainReader.Read<Bit[]>(offset)
                ?? throw new Exception("Unable to read NPC offset struct. Corrupt save?");
        }

        public bool Write(BitwiseBinaryWriter writer)
        {
           writer.WriteBits(Flags.ToBytes().ToBits());
            return true;
        }
    }
}
