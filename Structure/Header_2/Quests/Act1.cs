using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Quests
{
    public class Act1
    {
        public const int BytePosition = 0;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 14;

        private byte[]? actBytes { get; set; } = null;

        public static Act1 ReadActBytes(BitwiseBinaryReader? questMainReader)
        {
            if (questMainReader == null)
                throw new ArgumentNullException(nameof(questMainReader));

            Act1 act = new Act1();
            questMainReader.SetBitPosition(BitPosition);
            act.actBytes = questMainReader.ReadBits(Length * 8).ToBytes();
            return act;
        }

        public bool Introduction
        {
            get
            {
                if(actBytes == null)
                    throw new ArgumentNullException(nameof(actBytes));

                byte[] b = new byte[2];
                Array.Copy(actBytes, 0, b, 0, 2);

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
                Array.Copy(actBytes, 2, b, 0, 12);

                bool[,] rtn = new bool[6, 16];
                for(int questCount = 0; questCount < 6; questCount++)
                {
                    byte[] b2 = new byte[2];
                    Array.Copy(b, questCount * 2, b2, 0, 2);
                    bool[] bits = ByteArrayConverter.GetBitField(b2);
                    for(int c = 0;c < 16;c++)
                    {
                        rtn[questCount, c] = bits[c];
                    }
                }
                return rtn;
            }
        }
    }
}
