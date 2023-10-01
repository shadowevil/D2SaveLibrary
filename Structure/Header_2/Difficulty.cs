using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Difficulty
    {
        public const int BytePosition = 168;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 3;

        public static bool[,] GetDifficulty(BitwiseBinaryReader reader)
        {
            bool[,] difficulties = new bool[3,8];
            reader.SetBitPosition(BitPosition);
            int dif = 0;
            foreach(byte b in reader.ReadBits(Length*8).ToBytes())
            {
                bool[] bits = ByteArrayConverter.GetBitField(b);
                for (int col = 0; col < 8; col++)
                {
                    difficulties[dif,col] = bits[col];
                }
                dif++;
            }
            // This returns a 2d array which is a 3 rows by 8 columns
            // This is a layout for difficulty which is a bitfield
            /*
                  7	           6	       5	       4	       3	     2, 1, 0
                Active?	    Unknown	    Unknown	    Unknown	    Unknown	    Act (0-4)
             * */
            return difficulties;
        }
    }
}
