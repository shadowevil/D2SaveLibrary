using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.BinaryHandler
{
    public static class ByteArrayConverter
    {
        public static int ToInt32(byte[] bytes)
        {
            if (bytes.Length < 4)
                throw new ArgumentException("Array too short");

            return BitConverter.ToInt32(bytes, 0);
        }

        public static uint ToUInt32(byte[] bytes)
        {
            if (bytes.Length < 4)
                throw new ArgumentException("Array too short");

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static short ToInt16(byte[] bytes)
        {
            if (bytes.Length < 2)
                throw new ArgumentException("Array too short");

            return BitConverter.ToInt16(bytes, 0);
        }

        public static ushort ToUInt16(byte[] bytes)
        {
            if (bytes.Length < 2)
                throw new ArgumentException("Array too short");

            return BitConverter.ToUInt16(bytes, 0);
        }

        public static string ToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static bool[] GetBitField(byte b)
        {
            bool[] bits = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                bits[i] = (b & (1 << i)) != 0;
            }
            return bits;
        }


        public static bool[] GetBitField(byte[] b)
        {
            bool[] bits = new bool[b.Length * 8];
            for (int byteIndex = 0; byteIndex < b.Length; byteIndex++)
            {
                for (int bitIndex = 0; bitIndex < 8; bitIndex++)
                {
                    int i = byteIndex * 8 + bitIndex;
                    bits[i] = (b[byteIndex] & (1 << bitIndex)) != 0;
                }
            }
            return bits;
        }

        public static byte[] BitsToBytes(bool[] bits)
        {
            int numBytes = (bits.Length + 7) / 8;
            byte[] bytes = new byte[numBytes];

            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    bytes[i / 8] |= (byte)(1 << (i % 8));
                }
            }

            return bytes;
        }
    }
}
