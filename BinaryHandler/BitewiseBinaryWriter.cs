using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.BinaryHandler
{
    public class BitwiseBinaryWriter
    {
        private List<Bit> bits = new List<Bit>();
        private List<byte> bytes = new List<byte>();
        public byte[] GetBytes() => bytes.ToArray();
        public Bit[] GetBits() => bits.ToArray();
        public int d_bits { get => ((bytes.Count() * 8) + bits.Count());  }

        public void Align() { while (bits.Count % 8 > 0) WriteVoidBits(1); }

        public void WriteVoidBits(uint numBits)
        {
            for (int i = 0; i < numBits; i++)
            {
                WriteBit(0);
            }
            FlushBits(Endianness.LittleEndian);
        }

        public void WriteBit(Bit bit)
        {
            bits.Add(bit);
        }

        public void WriteBits(Bit[] bits, Endianness endianness = Endianness.LittleEndian)
        {
            for (int i = 0; i < bits.Length; i++)
            {
                WriteBit(bits[i]);
            }
            FlushBits(endianness);
        }

        public void FlushBits(Endianness endianness)
        {
            List<byte> lbytes = new List<byte>();
            int i = 0;
            for (; i < bits.Count - (bits.Count % 8); i += 8)
            {
                byte b = 0;
                for (int j = 0; j < 8; j++)
                {
                    int bitIndex = (endianness == Endianness.LittleEndian) ? j : (7 - j);
                    if (bits[i + j].Value == 1)
                    {
                        b |= (byte)(1 << bitIndex);
                    }
                }
                lbytes.Add(b);
            }

            // Remove the bits that have been converted to bytes
            bits.RemoveRange(0, i);

            // Add the new bytes to the existing byte list
            bytes.AddRange(lbytes);
        }
    }
}
