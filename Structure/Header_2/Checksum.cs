using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Checksum
    {
        public const int BytePosition = 12;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static string GetChecksum(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            return $"0x{ByteArrayConverter.ToUInt32(reader.ReadBits(Length * 8).ToBytes()).ToString("X")}";
        }

        public static string CalculateChecksum(byte[] data)
        {
            // Remove the old checksum from the new checksum calculation
            for (int i = 12; i < 16; i++) data[i] = 0x00;
            int checksum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                /* first the current checksum is multiplied by 2
                 * second we check if checksum is less than 0, if it is we place a 1, if it's not place a 0
                 * Third data[i] takes the byte value from the index at i
                 * Finally we add everything together */
                checksum = data[i] + (checksum * 2) + (checksum < 0 ? 1 : 0);
            }

            return $"0x{checksum.ToString("X")}";
        }
    }
}
