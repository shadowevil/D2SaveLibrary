using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.BinaryHandler
{
    public enum Endianness
    {
        LittleEndian,
        BigEndian
    }

    public struct Bit
    {
        public int Value { get; }

        public Bit(int value)
        {
            if (value != 0 && value != 1)
            {
                throw new ArgumentException("Bit value must be 0 or 1");
            }
            Value = value;
        }

        public Bit(bool value)
        {
            if (value == true) Value = 1; else Value = 0;
        }

        public static implicit operator Bit(int value)
        {
            return new Bit(value);
        }

        public static bool operator ==(Bit b, int value)
        {
            return b.Value == value;
        }

        public static bool operator !=(Bit b, int value)
        {
            return b.Value != value;
        }

        public static int operator <<(Bit b, int value)
        {
            return b.Value << value;
        }

        public static int operator >>(Bit b, int value)
        {
            return b.Value >> value;
        }

        public static implicit operator Bit(bool value)
        {
            return new Bit(value);
        }

        public static explicit operator int(Bit b)
        {
            return b.Value;
        }

        public static explicit operator bool(Bit b)
        {
            return b.Value == 1;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Bit bit)
            {
                return Value == bit.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public static class ByteArrayExtensions
    {
        public static Bit[] ToBits(this byte[] bytes)
        {
            int totalBits = bytes.Length * 8; // Total number of bits in the byte array
            List<Bit> bitArray = new List<Bit>(); // Array to hold the Bit structs

            for (int i = 0; i < bytes.Length; i++)
            {
                byte currentByte = bytes[i];
                for (int j = 0; j < 8; j++) // Loop through each bit in the byte
                {
                    int bitPosition;
                    //bitPosition = (bytes.Length - i - 1) * 8 + (7 - j); // Big-endian bit position
                    bitPosition = i * 8 + j; // Little-endian bit position

                    int bitValue = (currentByte >> j) & 1; // Extract the bit value
                    bitArray.Add(bitValue); // Initialize the Bit struct and store it
                }
            }

            return bitArray.ToArray();
        }
    }

    public static class BitExtensions
    {
        public static Bit[] ToBits(this string value, Endianness endianness = Endianness.LittleEndian)
        {
            List<Bit> bits = new List<Bit>();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            foreach (byte b in bytes)
            {
                bits.AddRange(b.ToBits(8, endianness));
            }
            return bits.ToArray();
        }

        public static Bit[] ToBits(this UInt16 value, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return ConvertToBits(value, numBits, endianness);
        }

        public static Bit[] ToBits(this UInt32 value, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return ConvertToBits(value, numBits, endianness);
        }

        public static Bit[] ToBits(this Int32 value, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return ConvertToBits((UInt32)value, numBits, endianness);
        }

        public static Bit[] ToBits(this Int16 value, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return ConvertToBits((UInt16)value, numBits, endianness);
        }

        public static Bit[] ToBits(this byte value, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return ConvertToBits(value, numBits, endianness);
        }

        public static Bit[] ToBits(this char value, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return ConvertToBits(value, numBits, endianness);
        }

        private static Bit[] ConvertToBits(UInt32 value, uint bitCount, Endianness endianness = Endianness.LittleEndian)
        {
            Bit[] bits = new Bit[bitCount];
            for (int i = 0; i < bitCount; i++)
            {
                int bitIndex = (endianness == Endianness.LittleEndian) ? i : ((int)bitCount - 1 - i);
                int bitValue = (int)((value >> i) & 1);
                bits[bitIndex] = new Bit(bitValue);
            }
            return bits;
        }
    }

    public static class BitArrayExtensions
    {
        public static T ToType<T>(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)bits.ToString(numBits, endianness);
            }
            else if (typeof(T) == typeof(UInt16))
            {
                return (T)(object)bits.ToUInt16(numBits, endianness);
            }
            else if (typeof(T) == typeof(UInt32))
            {
                return (T)(object)bits.ToUInt32(numBits, endianness);
            }
            else if (typeof(T) == typeof(Int32))
            {
                return (T)(object)bits.ToInt32(numBits, endianness);
            }
            else if (typeof(T) == typeof(Int16))
            {
                return (T)(object)bits.ToInt16(numBits, endianness);
            }
            else if (typeof(T) == typeof(byte) && numBits <= 8)
            {
                return (T)(object)(byte)bits.ToUInt16(numBits, endianness);
            }
            else
            {
                throw new ArgumentException($"Unsupported type {typeof(T)}");
            }
        }

        public static Bit[] ReverseByteOrder(Bit[] bits)
        {
            Bit[] reversedBits = new Bit[bits.Length];
            for (int byteIdx = 0; byteIdx < bits.Length / 8; byteIdx++)
            {
                for (int bitIdx = 0; bitIdx < 8; bitIdx++)
                {
                    int from = byteIdx * 8 + bitIdx;
                    int to = (bits.Length - 8) - byteIdx * 8 + bitIdx;
                    reversedBits[to] = bits[from];
                }
            }
            return reversedBits;
        }

        public static byte ToByte(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            byte result = 0;
            for(int i=0;i<bits.Length;i++)
            {
                result |= (byte)(bits[i] << i);
            }
            return result;
        }

        public static byte[] ToBytes(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            List<Bit> _bits = bits.ToList();
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < numBits / 8; i++)
            {
                List<Bit> bitA = new List<Bit>();
                for (int x = 0; x < 8; x++)
                {
                    bitA.Add(_bits[x]);
                }
                _bits.RemoveRange(0, 8);
                bytes.Add(bitA.ToArray().ToType<byte>(8, endianness));
            }
            return bytes.ToArray();
        }

        // For UInt16
        public static UInt16 ToUInt16(this Bit[] bits, uint numBits, Endianness endianness)
        {
            if (numBits > 16 || numBits > bits.Length)
            {
                throw new ArgumentException("Invalid bit count for UInt16");
            }

            UInt16 result = 0;
            // Reverse the order of bytes (but not bits within each byte)
            if (endianness == Endianness.LittleEndian) bits = ReverseByteOrder(bits);

            for (int i = 0; i < bits.Length; i++)
            {
                result |= (ushort)(bits[i] << i);
            }
            return result;
        }

        // For UInt32
        public static UInt32 ToUInt32(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            if (numBits > 32 || numBits > bits.Length)
            {
                throw new ArgumentException("Invalid bit count for UInt32");
            }

            UInt32 result = 0;
            // Reverse the order of bytes (but not bits within each byte)
            if (endianness == Endianness.LittleEndian) bits = ReverseByteOrder(bits);

            for (int i = 0; i < bits.Length; i++)
            {
                result |= (uint)(bits[i] << i);
            }
            return result;
        }

        // For Int32
        public static Int32 ToInt32(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            if (numBits > 32 || numBits > bits.Length)
            {
                throw new ArgumentException("Invalid bit count for Int32");
            }

            Int32 result = 0;
            // Reverse the order of bytes (but not bits within each byte)
            if (endianness == Endianness.LittleEndian) bits = ReverseByteOrder(bits);

            for (int i = 0; i < bits.Length; i++)
            {
                result |= (int)(bits[i] << i);
            }
            return result;
        }

        public static char ToChar(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return (char)(bits.ToUInt16(numBits, endianness));
        }

        // For Int16
        public static Int16 ToInt16(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            if (numBits > 16 || numBits > bits.Length)
            {
                throw new ArgumentException("Invalid bit count for Int16");
            }

            Int16 result = 0;
            // Reverse the order of bytes (but not bits within each byte)
            if (endianness == Endianness.LittleEndian) bits = ReverseByteOrder(bits);

            for (int i = 0; i < bits.Length; i++)
            {
                result |= (short)(bits[i] << i);
            }
            return result;
        }

        public static char[] ToCharArray(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            return bits.ToString(numBits, endianness).ToCharArray();
        }

        // For string
        public static string ToString(this Bit[] bits, uint numBits, Endianness endianness = Endianness.LittleEndian)
        {
            if (numBits > bits.Length)
            {
                throw new ArgumentException("Invalid bit count for string conversion");
            }

            StringBuilder sb = new StringBuilder();
            byte[] byteArray = new byte[bits.Length / 8];
            for (int i = 0; i < byteArray.Length; i++)
            {
                Bit[] byteBits = new Bit[8];
                Array.Copy(bits, i * 8, byteBits, 0, 8);
                sb.Append((char)byteBits.ToInt16((uint)byteBits.Length, Endianness.LittleEndian));
            }

            string str = sb.ToString();

            return sb.ToString();
        }

        public static string ToStringRepresentation(this Bit[] bits)
        {
            string result = "";
            foreach (Bit b in bits)
            {
                result += b.ToString();
            }
            return result;
        }
    }
}