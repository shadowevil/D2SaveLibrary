using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Checksum
    {
        public static string CalculateChecksum(byte[] data)
        {
            return $"0x{BinaryPrimitives.ReadInt32LittleEndian(ComputeChecksum(data)).ToString("X8")}";
        }

        public static byte[] ComputeChecksum(byte[] data)
        {
            byte[] copiedByteArray = new byte[data.Length];
            Array.Copy(data, copiedByteArray, data.Length);

            // Remove the old checksum from the new checksum calculation
            for (int i = 12; i < 16; i++) copiedByteArray[i] = 0x00;
            int checksum = 0;
            for (int i = 0; i < copiedByteArray.Length; i++)
            {
                /* first the current checksum is multiplied by 2
                 * second we check if checksum is less than 0, if it is we place a 1, if it's not place a 0
                 * Third data[i] takes the byte value from the index at i
                 * Finally we add everything together */
                checksum = copiedByteArray[i] + (checksum * 2) + (checksum < 0 ? 1 : 0);
            }
            byte[] checksumbytes = new byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(checksumbytes, checksum);
            return checksumbytes;
        }

        public static void WriteChecksumBytes(ref byte[] data, byte[] checksumBytes)
        {
            for(int i = 12; i < 16;i++)
            {
                data[i] = checksumBytes[i - 12];
            }
        }
    }
}
