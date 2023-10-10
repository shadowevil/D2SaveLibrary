using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestClass = D2SLib2.Structure.Quests.Quest;

namespace D2SLib2.Structure.Quests
{
    public class QuestDifficulty
    {
        public HashSet<Act> Acts = new HashSet<Act>();

        public bool Introduction_Warriv = false;
        public bool Traveled_Act2 = false;
        public bool Introduction_Jerhyn = false;
        public bool Traveled_Act3 = false;
        public bool Introduction_Hratli = false;
        public bool Traveled_Act4 = false;
        public bool Introduction_Tyeral = false;
        public bool Traveled_Act5 = false;
        public bool Introduction_Cain = false;
        public bool Akara_Reset_Stats = false;
        public byte Act5_Completed = 0x00;

        public QuestDifficulty() { }

        public static QuestDifficulty Read(BitwiseBinaryReader mainReader, int byteOffset)
        {
            QuestDifficulty questDifficulty = new QuestDifficulty();

            // Act1
            questDifficulty.Introduction_Warriv = mainReader.Read<UInt16>(QuestOffsets.OFFSET_INTRODUCTION_WARRIV, byteOffset * 8) >= 1;
            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT1_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT1.Offset));

            // Act2
            questDifficulty.Traveled_Act2 = mainReader.Read<UInt16>(QuestOffsets.OFFSET_TRAVELED_ACT2, byteOffset * 8) >= 1;
            questDifficulty.Introduction_Jerhyn = mainReader.Read<UInt16>(QuestOffsets.OFFSET_INTRODUCTION_JERHYN, byteOffset * 8) >= 1;
            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT2_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT2.Offset));

            // Act3
            questDifficulty.Traveled_Act3 = mainReader.Read<UInt16>(QuestOffsets.OFFSET_TRAVELED_ACT3, byteOffset * 8) >= 1;
            questDifficulty.Introduction_Hratli = mainReader.Read<UInt16>(QuestOffsets.OFFSET_INTRODUCTION_HRATLI, byteOffset * 8) >= 1;
            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT3_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT3.Offset));

            // Act4
            questDifficulty.Traveled_Act4 = mainReader.Read<UInt16>(QuestOffsets.OFFSET_TRAVELED_ACT4, byteOffset * 8) >= 1;
            questDifficulty.Introduction_Tyeral = mainReader.Read<UInt16>(QuestOffsets.OFFSET_INTRODUCTION_TYERAL, byteOffset * 8) >= 1;
            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT4_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT4.Offset));

            // Act5
            questDifficulty.Traveled_Act5 = mainReader.Read<UInt16>(QuestOffsets.OFFSET_TRAVELED_ACT5, byteOffset * 8) >= 1;
            questDifficulty.Introduction_Cain = mainReader.Read<UInt16>(QuestOffsets.OFFSET_INTRODUCTION_CAIN, byteOffset * 8) >= 1;
            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT5_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT5.Offset));

            // Extra
            questDifficulty.Akara_Reset_Stats = mainReader.Read<UInt16>(QuestOffsets.OFFSET_AKARA_STAT_RESET, byteOffset * 8) >= 1;
            questDifficulty.Act5_Completed = mainReader.Read<byte>(QuestOffsets.OFFSET_ACT5_COMPLETED, byteOffset * 8);

            return questDifficulty;
        }

        public bool Write(BitwiseBinaryWriter writer, OffsetStruct difficultyOffset)
        {
            if (writer.GetBytes().Length != difficultyOffset.Offset)
                return false;

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_INTRODUCTION_WARRIV.Offset)
                return false;
            writer.WriteBits((Convert.ToUInt16(Introduction_Warriv)).ToBits((uint)QuestOffsets.OFFSET_INTRODUCTION_WARRIV.BitLength));
            Acts.ElementAt(0).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT1.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT2.Offset)
                return false;
            writer.WriteBits((Convert.ToUInt16(Traveled_Act2)).ToBits((uint)QuestOffsets.OFFSET_TRAVELED_ACT2.BitLength));
            writer.WriteBits((Convert.ToUInt16(Introduction_Jerhyn)).ToBits((uint)QuestOffsets.OFFSET_INTRODUCTION_JERHYN.BitLength));
            Acts.ElementAt(1).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT2.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT3.Offset)
                return false;
            writer.WriteBits((Convert.ToUInt16(Traveled_Act3)).ToBits((uint)QuestOffsets.OFFSET_TRAVELED_ACT3.BitLength));
            writer.WriteBits((Convert.ToUInt16(Introduction_Hratli)).ToBits((uint)QuestOffsets.OFFSET_INTRODUCTION_HRATLI.BitLength));
            Acts.ElementAt(2).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT3.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT4.Offset)
                return false;
            writer.WriteBits((Convert.ToUInt16(Traveled_Act4)).ToBits((uint)QuestOffsets.OFFSET_TRAVELED_ACT4.BitLength));
            writer.WriteBits((Convert.ToUInt16(Introduction_Tyeral)).ToBits((uint)QuestOffsets.OFFSET_INTRODUCTION_TYERAL.BitLength));
            Acts.ElementAt(3).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT4.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT5.Offset)
                return false;
            writer.WriteBits((Convert.ToUInt16(Traveled_Act5)).ToBits((uint)QuestOffsets.OFFSET_TRAVELED_ACT5.BitLength));
            writer.WriteBits((Convert.ToUInt16(Introduction_Cain)).ToBits((uint)QuestOffsets.OFFSET_INTRODUCTION_CAIN.BitLength));
            writer.WriteVoidBits(32);
            Acts.ElementAt(4).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT5.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_AKARA_STAT_RESET.Offset)
                return false;
            writer.WriteBits((Convert.ToUInt16(Akara_Reset_Stats)).ToBits((uint)QuestOffsets.OFFSET_AKARA_STAT_RESET.BitLength));
            writer.WriteBits((Convert.ToUInt16(Act5_Completed)).ToBits((uint)QuestOffsets.OFFSET_ACT5_COMPLETED.BitLength));

            writer.WriteVoidBits(12 * 8);

            return true;
        }
    }
}
