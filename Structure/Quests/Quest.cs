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

        public bool Completed {                  get { return (bool)Flags[0]; }  set { Flags[0] = value; } }
        public bool RequirementsMet {            get { return (bool)Flags[1]; }  set { Flags[1] = value; } }
        public bool Given {                      get { return (bool)Flags[2]; }  set { Flags[2] = value; } }
        public bool DrankPotionOfLifeAct3 {      get { return (bool)Flags[6]; }  set { Flags[6] = value; } }
        public bool ReadScrollOfResistanceAct5 { get { return (bool)Flags[8]; }  set { Flags[8] = value; } }
        public bool SecretCowLevelCompleteAct1 { get { return (bool)Flags[11]; } set { Flags[11] = value; } }
        public bool Closed {                     get { return (bool)Flags[12]; } set { Flags[12] = value; } }
        public bool CompletedInCurrentGame {     get { return (bool)Flags[13]; } set { Flags[13] = value; } }

        public bool Write(BitwiseBinaryWriter writer, int byteOffset)
        {
            if (writer.GetBytes().Length != byteOffset)
                return false;

            writer.WriteBits(Flags!);
            return true;
        }
    }
}
