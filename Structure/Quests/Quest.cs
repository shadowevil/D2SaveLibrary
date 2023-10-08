using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Quests
{
    public class Quest
    {
        private Bit[] Flags = new Bit[16];

        public Quest(Bit[] flags)
        {
            Flags = flags;
        }

        public bool Completed
        {
            get
            {
                return Flags[0];
            }
            set
            {
                Flags[0] = (Bit)(value ? 1 : 0);
            }
        }

        public bool RequirementsMet
        {
            get
            {
                return Flags[1];
            }
            set
            {
                Flags[1] = (Bit)(value ? 1 : 0);
            }
        }

        public bool Given
        {
            get
            {
                return Flags[2];
            }
            set
            {
                Flags[2] = (Bit)(value ? 1 : 0);
            }
        }

        public bool DrankPotionOfLifeAct3
        {
            get
            {
                return Flags[6];
            }
            set
            {
                Flags[6] = (Bit)(value ? 1 : 0);
            }
        }

        public bool ReadScrollOfResistanceAct5
        {
            get
            {
                return Flags[8];
            }
            set
            {
                Flags[8] = (Bit)(value ? 1 : 0);
            }
        }

        public bool SecretCowLevelCompleteAct1
        {
            get
            {
                return Flags[11];
            }
            set
            {
                Flags[11] = (Bit)(value ? 1 : 0);
            }
        }

        public bool Closed
        {
            get
            {
                return Flags[12];
            }
            set
            {
                Flags[12] = (Bit)(value ? 1 : 0);
            }
        }

        public bool CompletedInCurrentGame
        {
            get
            {
                return Flags[13];
            }
            set
            {
                Flags[13] = (Bit)(value ? 1 : 0);
            }
        }

        public bool Write(BitwiseBinaryWriter writer, int byteOffset)
        {
            if (writer.GetBytes().Length != byteOffset)
                return false;

           writer.WriteBits(Flags.ToBytes().ToBits());
            return true;
        }
    }
}
