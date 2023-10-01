using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Header.Waypoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Quests
{
    public class DifficultyQuest
    {
        private readonly int BytePosition = 0;
        private readonly int BitPosition = 0;
        private const int Length = 96;

        private BitwiseBinaryReader? questReader { get; set; } = null;
        public byte[]? questData => questReader == null ? null : questReader.bitArray.ToBytes();

        public Act1? act1 { get; set; } = null;
        public Act2? act2 { get; set; } = null;
        public Act3? act3 { get; set; } = null;
        public Act4? act4 { get; set; } = null;
        public Act5? act5 { get; set; } = null;

        public DifficultyQuest(int offset = -1)
        {
            if (offset > 0)
            {
                BytePosition = offset;
                BitPosition = BytePosition * sizeof(long);
            }
        }

        public void ReadQuestBytes(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            questReader = new BitwiseBinaryReader(reader.ReadBits(Length*8));

            act1 = Act1.ReadActBytes(questReader);
            act2 = Act2.ReadActBytes(questReader);
            act3 = Act3.ReadActBytes(questReader);
            act4 = Act4.ReadActBytes(questReader);
            act5 = Act5.ReadActBytes(questReader);
        }
    }
}
