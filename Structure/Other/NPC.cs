using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Player.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
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

        public DifficultyStruct? Normal { get; set; } = null;
        public DifficultyStruct? Nightmare { get; set; } = null;
        public DifficultyStruct? Hell { get; set; } = null;

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

            npc.Normal = new DifficultyStruct()
            {
                warriv_act_ii   = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_II,    (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_II,    (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                charsi          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CHARSI,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CHARSI,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                warriv_act_i    = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_I,     (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_I,     (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                kashya          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_KASHYA,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8), 
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_KASHYA,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                akara           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_AKARA,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_AKARA,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                gheed           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GHEED,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GHEED,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                greiz           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GREIZ,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GREIZ,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                jerhyn          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_JERHYN,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_JERHYN,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                meshif_act_ii   = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_II,    (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_II,    (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                geglash         = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GEGLASH,          (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GEGLASH,          (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                lysnader        = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_LYSNADER,         (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_LYSNADER,         (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                fara            = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_FARA,             (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_FARA,             (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                drogan          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_DROGAN,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_DROGAN,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                alkor           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ALKOR,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ALKOR,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                hratli          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_HRATLI,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_HRATLI,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                ashera          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ASHERA,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ASHERA,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                cain_act_iii    = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_III,     (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_III,     (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                elzix           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ELZIX,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ELZIX,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                malah           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MALAH,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MALAH,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                anya            = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ANYA,             (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ANYA,             (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                natalya         = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NATALYA,          (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NATALYA,          (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                meshif_act_iii  = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_III,   (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_III,   (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                ormus           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ORMUS,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ORMUS,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                cain_act_v      = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_V,       (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_V,       (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                qualkehk        = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_QUALKEHK,         (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_QUALKEHK,         (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8)),
                nihlathak       = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NIHLATHAK,        (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NIHLATHAK,        (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset) * 8))
            };
            npc.Nightmare = new DifficultyStruct()
            {
                warriv_act_ii   = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_II,    (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_II,    (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                charsi          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CHARSI,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CHARSI,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                warriv_act_i    = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_I,     (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_I,     (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                kashya          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_KASHYA,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8), 
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_KASHYA,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                akara           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_AKARA,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_AKARA,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                gheed           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GHEED,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GHEED,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                greiz           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GREIZ,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GREIZ,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                jerhyn          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_JERHYN,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_JERHYN,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                meshif_act_ii   = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_II,    (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_II,    (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                geglash         = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GEGLASH,          (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GEGLASH,          (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                lysnader        = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_LYSNADER,         (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_LYSNADER,         (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                fara            = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_FARA,             (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_FARA,             (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                drogan          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_DROGAN,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_DROGAN,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                alkor           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ALKOR,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ALKOR,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                hratli          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_HRATLI,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_HRATLI,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                ashera          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ASHERA,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ASHERA,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                cain_act_iii    = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_III,     (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_III,     (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                elzix           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ELZIX,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ELZIX,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                malah           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MALAH,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MALAH,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                anya            = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ANYA,             (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ANYA,             (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                natalya         = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NATALYA,          (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NATALYA,          (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                meshif_act_iii  = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_III,   (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_III,   (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                ormus           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ORMUS,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ORMUS,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                cain_act_v      = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_V,       (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_V,       (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                qualkehk        = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_QUALKEHK,         (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_QUALKEHK,         (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8)),
                nihlathak       = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NIHLATHAK,        (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 5) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NIHLATHAK,        (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 5) * 8))
            };

            npc.Hell = new DifficultyStruct()
            {
                warriv_act_ii   = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_II,    (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_II,    (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                charsi          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CHARSI,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CHARSI,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                warriv_act_i    = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_I,     (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_WARRIV_ACT_I,     (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                kashya          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_KASHYA,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8), 
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_KASHYA,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                akara           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_AKARA,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_AKARA,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                gheed           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GHEED,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GHEED,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                greiz           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GREIZ,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GREIZ,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                jerhyn          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_JERHYN,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_JERHYN,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                meshif_act_ii   = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_II,    (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_II,    (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                geglash         = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GEGLASH,          (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_GEGLASH,          (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                lysnader        = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_LYSNADER,         (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_LYSNADER,         (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                fara            = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_FARA,             (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_FARA,             (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                drogan          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_DROGAN,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_DROGAN,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                alkor           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ALKOR,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ALKOR,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                hratli          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_HRATLI,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_HRATLI,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                ashera          = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ASHERA,           (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ASHERA,           (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                cain_act_iii    = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_III,     (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_III,     (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                elzix           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ELZIX,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ELZIX,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                malah           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MALAH,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MALAH,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                anya            = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ANYA,             (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ANYA,             (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                natalya         = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NATALYA,          (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NATALYA,          (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                meshif_act_iii  = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_III,   (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_MESHIF_ACT_III,   (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                ormus           = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ORMUS,            (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_ORMUS,            (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                cain_act_v      = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_V,       (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_CAIN_ACT_V,       (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                qualkehk        = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_QUALKEHK,         (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_QUALKEHK,         (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8)),
                nihlathak       = new NPC(mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NIHLATHAK,        (OtherOffsets.OFFSET_NPC_INTRODUCTION.Offset   + 10) * 8),
                                          mainReader.Read<Bit>(OtherOffsets.OFFSET_NPC_NIHLATHAK,        (OtherOffsets.OFFSET_NPC_CONGRATULATION.Offset + 10) * 8))
            };

            return npc;
        }
    }

    public class DifficultyStruct
    {
        public NPC warriv_act_ii    { get; set; } = new NPC();      // 0  offset
        public NPC charsi           { get; set; } = new NPC();      // 2  offset
        public NPC warriv_act_i     { get; set; } = new NPC();      // 3  offset
        public NPC kashya           { get; set; } = new NPC();      // 4  offset
        public NPC akara            { get; set; } = new NPC();      // 5  offset
        public NPC gheed            { get; set; } = new NPC();      // 6  offset
        public NPC greiz            { get; set; } = new NPC();      // 8  offset
        public NPC jerhyn           { get; set; } = new NPC();      // 9  offset
        public NPC meshif_act_ii    { get; set; } = new NPC();      // 10 offset
        public NPC geglash          { get; set; } = new NPC();      // 11 offset
        public NPC lysnader         { get; set; } = new NPC();      // 12 offset
        public NPC fara             { get; set; } = new NPC();      // 13 offset
        public NPC drogan           { get; set; } = new NPC();      // 14 offset
        public NPC alkor            { get; set; } = new NPC();      // 16 offset
        public NPC hratli           { get; set; } = new NPC();      // 17 offset
        public NPC ashera           { get; set; } = new NPC();      // 18 offset
        public NPC cain_act_iii     { get; set; } = new NPC();      // 21 offset
        public NPC elzix            { get; set; } = new NPC();      // 23 offset
        public NPC malah            { get; set; } = new NPC();      // 24 offset
        public NPC anya             { get; set; } = new NPC();      // 25 offset
        public NPC natalya          { get; set; } = new NPC();      // 27 offset
        public NPC meshif_act_iii   { get; set; } = new NPC();      // 28 offset
        public NPC ormus            { get; set; } = new NPC();      // 31 offset
        public NPC cain_act_v       { get; set; } = new NPC();      // 37 offset
        public NPC qualkehk         { get; set; } = new NPC();      // 38 offset
        public NPC nihlathak        { get; set; } = new NPC();      // 39 offset
    }

    [DebuggerDisplay("{Introduced} : {Congratulated}")]
    public class NPC
    {
        public NPC() { }

        public NPC(bool intro, bool congrats)
        {
            Introduced = intro;
            Congratulated = congrats;
        }

        public bool Introduced { get; set; } = false;
        public bool Congratulated { get; set; } = false;
    }
}
