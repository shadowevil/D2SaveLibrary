using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Quests
{
    public class Act4
    {
        public const int BytePosition = Act1.Length + Act2.Length + Act3.Length;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 16;
        public const int TraveledPosition = 0;
        public const int IntroductionPosition = 2;
        public const int QuestStartPosition = 4;

        private byte[]? actBytes { get; set; } = null;

        public static Act4 ReadActBytes(BitwiseBinaryReader? questMainReader)
        {
            if (questMainReader == null)
                throw new ArgumentNullException(nameof(questMainReader));

            Act4 act = new Act4();
            questMainReader.SetBitPosition(BitPosition);
            act.actBytes = questMainReader.ReadBits(Length*8).ToBytes();
            return act;
        }

        public bool Traveled
        {
            get
            {
                if (actBytes == null)
                    throw new ArgumentNullException(nameof(actBytes));

                byte[] b = new byte[2];
                Array.Copy(actBytes, TraveledPosition, b, 0, 2);

                return Convert.ToBoolean(ByteArrayConverter.ToUInt16(b));
            }
        }

        public bool Introduction
        {
            get
            {
                if (actBytes == null)
                    throw new ArgumentNullException(nameof(actBytes));

                byte[] b = new byte[2];
                Array.Copy(actBytes, IntroductionPosition, b, 0, 2);

                return Convert.ToBoolean(ByteArrayConverter.ToUInt16(b));
            }
        }

        public bool[,] Quests
        {
            get
            {
                if (actBytes == null)
                    throw new ArgumentNullException(nameof(actBytes));

                byte[] b = new byte[12];
                Array.Copy(actBytes, QuestStartPosition, b, 0, 12);

                bool[,] rtn = new bool[6, 16];
                for (int questCount = 0; questCount < 6; questCount++)
                {
                    byte[] b2 = new byte[2];
                    Array.Copy(b, questCount * 2, b2, 0, 2);
                    bool[] bits = ByteArrayConverter.GetBitField(b2);
                    for (int c = 0; c < 16; c++)
                    {
                        rtn[questCount, c] = bits[c];
                    }
                }
                return rtn;
            }
        }
    }
}
