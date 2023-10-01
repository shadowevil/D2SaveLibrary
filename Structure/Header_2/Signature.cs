using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Signature
    {
        public const int BytePosition = 0;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 4;

        public static string GetSignature(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            // BitConverter.ToUInt32() is a little endian bit converter, the ToString("X8")
            // specifically takes 4 bytes (usually 0xXX for each byte XX being the byte itself)
            // which makes 8 total characters, thus X8 says there should be 8 available spaces, 
            // otherwise it will be left-padded with zeros.
            return $"0x{BitConverter.ToUInt32(reader.ReadBits(Length*8).ToBytes(), 0).ToString("X8")}";
        }
    }
}
