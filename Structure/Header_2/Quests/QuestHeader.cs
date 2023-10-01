using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Quests
{
    public static class QuestHeader
    {
        // We will be only setting the bit position
        // relative to the quest section nothing else
        // Length is 10 but not actually used
        public const int Length = 10;

        public static string GetDescriptor(BitwiseBinaryReader questReader)
        {
            questReader.SetBitPosition(0);
            return questReader.ReadBits(32).To_String();
        }

        public static UInt32 GetVersion(BitwiseBinaryReader questReader)
        {
            questReader.SetBitPosition(32);
            return questReader.ReadBits(32).ToUInt32();
        }

        public static UInt16 GetSize(BitwiseBinaryReader questReader)
        {
            questReader.SetBitPosition(64);
            return questReader.ReadBits(16).ToUInt16();
        }
    }
}
