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
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_INTRODUCTION_WARRIV.Offset);
            questDifficulty.Introduction_Warriv = mainReader.ReadBits(QuestOffsets.OFFSET_INTRODUCTION_WARRIV.BitLength).ToUInt16() >= 1;

            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT1_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT1.Offset));

            // Act2
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_TRAVELED_ACT2.Offset);
            questDifficulty.Traveled_Act2 = mainReader.ReadBits(QuestOffsets.OFFSET_TRAVELED_ACT2.BitLength).ToUInt16() >= 1;

            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_INTRODUCTION_JERHYN.Offset);
            questDifficulty.Introduction_Jerhyn = mainReader.ReadBits(QuestOffsets.OFFSET_INTRODUCTION_JERHYN.BitLength).ToUInt16() >= 1;

            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT2_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT2.Offset));

            // Act3
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_TRAVELED_ACT3.Offset);
            questDifficulty.Traveled_Act3 = mainReader.ReadBits(QuestOffsets.OFFSET_TRAVELED_ACT3.BitLength).ToUInt16() >= 1;

            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_INTRODUCTION_HRATLI.Offset);
            questDifficulty.Introduction_Hratli = mainReader.ReadBits(QuestOffsets.OFFSET_INTRODUCTION_HRATLI.BitLength).ToUInt16() >= 1;

            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT3_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT3.Offset));

            // Act4
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_TRAVELED_ACT4.Offset);
            questDifficulty.Traveled_Act4 = mainReader.ReadBits(QuestOffsets.OFFSET_TRAVELED_ACT4.BitLength).ToInt16() >= 1;

            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_INTRODUCTION_TYERAL.Offset);
            questDifficulty.Introduction_Tyeral = mainReader.ReadBits(QuestOffsets.OFFSET_INTRODUCTION_TYERAL.BitLength).ToUInt16() >= 1;

            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT4_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT4.Offset));

            // Act5
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_TRAVELED_ACT5.Offset);
            questDifficulty.Traveled_Act5 = mainReader.ReadBits(QuestOffsets.OFFSET_TRAVELED_ACT5.BitLength).ToInt16() >= 1;

            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_INTRODUCTION_CAIN.Offset);
            questDifficulty.Introduction_Cain = mainReader.ReadBits(QuestOffsets.OFFSET_INTRODUCTION_CAIN.BitLength).ToUInt16() >= 1;

            questDifficulty.Acts.Add(Act.Read(mainReader, QuestOffsets.ACT5_QUEST_COUNT, byteOffset + QuestOffsets.OFFSET_ACT5.Offset));

            // Extra
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_AKARA_STAT_RESET.Offset);
            questDifficulty.Akara_Reset_Stats = mainReader.ReadBits(QuestOffsets.OFFSET_AKARA_STAT_RESET.BitLength).ToUInt16() >= 1;
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_ACT5_COMPLETED.Offset);
            questDifficulty.Act5_Completed = mainReader.ReadBits(QuestOffsets.OFFSET_ACT5_COMPLETED.BitLength).ToByte();

            return questDifficulty;
        }

        public bool Write(BitwiseBinaryWriter writer, OffsetStruct difficultyOffset)
        {
            if (writer.GetBytes().Length != difficultyOffset.Offset)
                return false;

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_INTRODUCTION_WARRIV.Offset)
                return false;
           writer.WriteBits((Convert.ToUInt16(Introduction_Warriv)).ToBits());
            Acts.ElementAt(0).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT1.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT2.Offset)
                return false;
           writer.WriteBits((Convert.ToUInt16(Traveled_Act2)).ToBits());
           writer.WriteBits((Convert.ToUInt16(Introduction_Jerhyn)).ToBits());
            Acts.ElementAt(1).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT2.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT3.Offset)
                return false;
           writer.WriteBits((Convert.ToUInt16(Traveled_Act3)).ToBits());
           writer.WriteBits((Convert.ToUInt16(Introduction_Hratli)).ToBits());
            Acts.ElementAt(2).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT3.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT4.Offset)
                return false;
           writer.WriteBits((Convert.ToUInt16(Traveled_Act4)).ToBits());
           writer.WriteBits((Convert.ToUInt16(Introduction_Tyeral)).ToBits());
            Acts.ElementAt(3).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT4.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_TRAVELED_ACT5.Offset)
                return false;
           writer.WriteBits((Convert.ToUInt16(Traveled_Act5)).ToBits());
           writer.WriteBits((Convert.ToUInt16(Introduction_Cain)).ToBits());
           writer.WriteVoidBits(32);
            Acts.ElementAt(4).Write(writer, difficultyOffset.Offset + QuestOffsets.OFFSET_ACT5.Offset);

            if (writer.GetBytes().Length != difficultyOffset.Offset + QuestOffsets.OFFSET_AKARA_STAT_RESET.Offset)
                return false;
           writer.WriteBits((Convert.ToUInt16(Akara_Reset_Stats)).ToBits());
           writer.WriteBits((Convert.ToUInt16(Act5_Completed)).ToBits());

           writer.WriteVoidBits(10 * 8);

            return true;
        }
    }
}
