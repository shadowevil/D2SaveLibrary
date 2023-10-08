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
        private List<byte> buffer = new List<byte>();

        public byte[] GetBytes() => buffer.ToArray();
        public int FlushCount => bits.Count;

        public BitwiseBinaryWriter()
        {

        }

        public void WriteBit(Bit bit, bool isLittleEndian = false)
        {
            bits.Add(bit);
            FlushIfNeeded(isLittleEndian);
        }

        public void WriteBits(IEnumerable<Bit> bits, bool isLittleEndian = false)
        {
            this.bits.AddRange(bits);
            FlushIfNeeded(isLittleEndian);
        }

        public void WriteBits(IEnumerable<Bit> bits, int n, bool isLittleEndian = false)
        {
            // Take only 'n' bits from the input, starting from the most significant if big-endian
            // or least significant if little-endian
            IEnumerable<Bit> selectedBits = isLittleEndian ?
                bits.TakeLast(n) :
                bits.Take(n);

            this.bits.AddRange(selectedBits);
            FlushIfNeeded(isLittleEndian);
        }

        public Bit[] GetNumBits(IEnumerable<Bit> bits, int n, bool isLittleEndian = false)
        {
            IEnumerable<Bit> selectedBits = isLittleEndian ?
                bits.TakeLast(n) :
                bits.Take(n);

            return selectedBits.ToArray();
        }

        private void FlushIfNeeded(bool isLittleEndian = false)
        {
            while (bits.Count >= 8)
            {
                byte byteValue = ConvertBitsToByte(bits.GetRange(0, 8), isLittleEndian);
                buffer.Add(byteValue);
                bits.RemoveRange(0, 8);
            }
        }

        private byte ConvertBitsToByte(List<Bit> bits, bool isLittleEndian = false)
        {
            byte value = 0;
            for (int i = 0; i < 8; i++)
            {
                int index = isLittleEndian ? i : (7 - i);
                if (bits[index].Value == 1)
                {
                    value |= (byte)(1 << (7 - i));
                }
            }
            return value;
        }

        public void Flush(bool isLittleEndian = false)
        {
            if (bits.Count > 0)
            {
                // Fill remaining bits with zeros and write
                while (bits.Count < 8)
                {
                    bits.Add(new Bit(0));
                }
                byte byteValue = ConvertBitsToByte(bits, isLittleEndian);
                buffer.Add(byteValue);
                bits.Clear();
            }
        }

        public byte[] ToArray()
        {
            Flush();
            return buffer.ToArray();
        }

        public List<byte> ToList()
        {
            Flush();
            return new List<byte>(buffer);
        }

        public void WriteVoidBits(int numBits)
        {
            for(int i=0;i<numBits;i++)
            {
                bits.Add(new Bit(0));
                FlushIfNeeded();
            }
        }
    }

    public static class BitExtensions
    {
        public static Bit[] ToBits(this uint value, bool isLittleEndian = true)
        {
            Bit[] bits = new Bit[32];
            for (int i = 0; i < 32; i++)
            {
                int index = isLittleEndian ? i : (31 - i);
                bits[index] = new Bit((int)(value >> i) & 1);
            }
            return bits;
        }

        public static Bit[] ToBits(this int value, bool isLittleEndian = true)
        {
            Bit[] bits = new Bit[32];
            for (int i = 0; i < 32; i++)
            {
                int index = isLittleEndian ? i : (31 - i);
                bits[index] = new Bit((value >> i) & 1);
            }
            return bits;
        }

        public static Bit[] ToBits(this ushort value, bool isLittleEndian = true)
        {
            Bit[] bits = new Bit[16];
            for (int i = 0; i < 16; i++)
            {
                int index = isLittleEndian ? i : (15 - i);
                bits[index] = new Bit((value >> i) & 1);
            }
            return bits;
        }

        public static Bit[] ToBits(this short value, bool isLittleEndian = true)
        {
            Bit[] bits = new Bit[16];
            for (int i = 0; i < 16; i++)
            {
                int index = isLittleEndian ? i : (15 - i);
                bits[index] = new Bit((value >> i) & 1);
            }
            return bits;
        }

        public static Bit[] ToBits(this byte value, bool isLittleEndian = true)
        {
            Bit[] bits = new Bit[8];
            for (int i = 0; i < 8; i++)
            {
                int index = isLittleEndian ? i : (7 - i);
                bits[index] = new Bit((value >> i) & 1);
            }
            return bits;
        }

        public static Bit[] ToBits(this char value, bool isLittleEndian = true)
        {
            return ((byte)value).ToBits(isLittleEndian);
        }

        public static Bit[] ToBits(this string value, bool isLittleEndian = true)
        {
            List<Bit> bits = new List<Bit>();
            foreach (char c in value)
            {
                bits.AddRange(c.ToBits(isLittleEndian));
            }
            return bits.ToArray();
        }

        public static Bit[] ToBits(this byte[] value, bool isLittleEndian = true)
        {
            List<Bit> bits = new List<Bit>();
            foreach (byte b in value)
            {
                bits.AddRange(b.ToBits(isLittleEndian));
            }
            return bits.ToArray();
        }
    }
}
