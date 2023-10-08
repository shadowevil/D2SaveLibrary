using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Quests
{
    public class Act
    {
        private int QuestCount = 0;
        public HashSet<Quest> Quests = new HashSet<Quest>();

        public Act(int questCount) {
            QuestCount = questCount;
        }

        public static Act Read(BitwiseBinaryReader mainReader, int questCount, int byteOffset)
        {
            Act act = new Act(questCount);

            if (act.QuestCount < 1) return act;
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST1.Offset);
            act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST1.BitLength)));

            if (act.QuestCount < 2) return act;
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST2.Offset);
            act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST2.BitLength)));

            if (act.QuestCount < 3) return act;
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST3.Offset);
            act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST3.BitLength)));

            if (act.QuestCount < 4)
            {
                mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST4.Offset);
                act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST4.BitLength)));
                mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST5.Offset);
                act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST5.BitLength)));
                mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST6.Offset);
                act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST6.BitLength)));
                return act;
            }
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST4.Offset);
            act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST4.BitLength)));

            if (act.QuestCount < 5) return act;
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST5.Offset);
            act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST5.BitLength)));

            if (act.QuestCount < 6) return act;
            mainReader.SetBytePosition(byteOffset + QuestOffsets.OFFSET_QUEST6.Offset);
            act.Quests.Add(new Quest(mainReader.ReadBits(QuestOffsets.OFFSET_QUEST6.BitLength)));

            return act;
        }

        public bool Write(BitwiseBinaryWriter writer, int byteOffset)
        {
            if (writer.GetBytes().Length != byteOffset + QuestOffsets.OFFSET_QUEST1.Offset)
                return false;
            Quests.ElementAt(0).Write(writer, byteOffset + QuestOffsets.OFFSET_QUEST1.Offset);

            if (writer.GetBytes().Length != byteOffset + QuestOffsets.OFFSET_QUEST2.Offset)
                return false;
            Quests.ElementAt(1).Write(writer, byteOffset + QuestOffsets.OFFSET_QUEST2.Offset);

            if (writer.GetBytes().Length != byteOffset + QuestOffsets.OFFSET_QUEST3.Offset)
                return false;
            Quests.ElementAt(2).Write(writer, byteOffset + QuestOffsets.OFFSET_QUEST3.Offset);

            if (writer.GetBytes().Length != byteOffset + QuestOffsets.OFFSET_QUEST4.Offset)
                return false;
            Quests.ElementAt(3).Write(writer, byteOffset + QuestOffsets.OFFSET_QUEST4.Offset);

            if (writer.GetBytes().Length != byteOffset + QuestOffsets.OFFSET_QUEST5.Offset)
                return false;
            Quests.ElementAt(4).Write(writer, byteOffset + QuestOffsets.OFFSET_QUEST5.Offset);

            if (writer.GetBytes().Length != byteOffset + QuestOffsets.OFFSET_QUEST6.Offset)
                return false;
            Quests.ElementAt(5).Write(writer, byteOffset + QuestOffsets.OFFSET_QUEST6.Offset);

            return true;
        }
    }
}
