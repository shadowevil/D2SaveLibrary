using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Quests
{
    public class QuestMain
    {
        public static QuestMain? instance { get; private set; } = null;
        private const int BytePosition = 335;
        private const int BitPosition = BytePosition * sizeof(long);
        private const int Length = 298;

        private const int NormalOffset = 0 + QuestHeader.Length;
        private const int NightmareOffset = 96 + QuestHeader.Length;
        private const int HellOffset = 192 + QuestHeader.Length;

        private BitwiseBinaryReader? questReader { get; set; } = null;

        public DifficultyQuest? Normal { get; set; } = null;
        public DifficultyQuest? Nightmare { get; set; } = null;
        public DifficultyQuest? Hell { get; set; } = null;

        public static QuestMain ReadQuestBytes(BitwiseBinaryReader reader)
        {
            instance = new QuestMain();
            reader.SetBitPosition(BitPosition);
            instance.questReader = new BitwiseBinaryReader(reader.ReadBits(Length * 8));

            instance.Normal = new DifficultyQuest(NormalOffset);
            instance.Normal.ReadQuestBytes(instance.questReader);

            instance.Nightmare = new DifficultyQuest(NightmareOffset);
            instance.Nightmare.ReadQuestBytes(instance.questReader);

            instance.Hell = new DifficultyQuest(HellOffset);
            instance.Hell.ReadQuestBytes(instance.questReader);

            return instance;
        }

        public string Descriptor
        {
            get
            {
                if (questReader == null)
                    throw new ArgumentNullException(nameof(questReader));

                return QuestHeader.GetDescriptor(questReader);
            }
        }

        public UInt32 Version
        {
            get
            {
                if (questReader == null)
                    throw new ArgumentNullException(nameof(questReader));

                return QuestHeader.GetVersion(questReader);
            }
        }

        public UInt16 Size
        {
            get
            {
                if (questReader == null)
                    throw new ArgumentNullException(nameof(questReader));

                return QuestHeader.GetSize(questReader);
            }
        }
    }
}
