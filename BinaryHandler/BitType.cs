using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.BinaryHandler
{
    [DebuggerDisplay("{Value}")]
    public struct Bit
    {
        private int _value;

        public Bit(int value)
        {
            if (value == 0 || value == 1)
            {
                _value = value;
            }
            else
            {
                throw new ArgumentException("Bit can only be 1 or 0.");
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (value == 0 || value == 1)
                {
                    _value = value;
                }
                else
                {
                    throw new ArgumentException("Bit can only be 1 or 0.");
                }
            }
        }

        public static implicit operator bool(Bit b) => (b.Value != 0);

        public int ToInt() => (int)_value;
        public uint ToUInt() => (uint)_value;
        public long ToLong() => (long)_value;
        public ulong ToUlong() => (ulong)Value;
        public short ToShort() => (short)_value;
        public ushort ToUshort() => (ushort)Value;
        public static explicit operator Bit(int value) => new Bit(value);

        public static Bit[] ConvertByteArrayToBitArray(byte[] byteArray)
        {
            int totalBits = byteArray.Length * 8; // Total number of bits in the byte array
            Bit[] bitArray = new Bit[totalBits]; // Array to hold the Bit structs

            for (int i = 0; i < byteArray.Length; i++)
            {
                byte currentByte = byteArray[i];
                for (int j = 0; j < 8; j++) // Loop through each bit in the byte
                {
                    int bitPosition = i * 8 + j; // Calculate the position in the Bit array
                    int bitValue = (currentByte >> j) & 1; // Extract the bit value using little-endian order
                    bitArray[bitPosition] = new Bit(bitValue); // Initialize the Bit struct and store it
                }
            }

            return bitArray;
        }
    }

    public static class BitArrayExtensions
    {
        private static Bit[] ReverseByteOrder(Bit[] bits)
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


        public static int ToInt32(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 32)
                throw new ArgumentException("Too many bits for UInt32.");

            int result = 0;

            // Reverse the order of bytes (but not bits within each byte)
            if (!littleEndian) bits = ReverseByteOrder(bits);

            for (int i = 0; i < bits.Length; i++)
            {
                result |= bits[i].ToInt() << i;
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<Int32>(bits, result);
            }

            return result;
        }

        public static uint ToUInt32(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 32)
                throw new ArgumentException("Too many bits for UInt32.");

            uint result = 0;

            // Reverse the order of bytes (but not bits within each byte)
            if (!littleEndian) bits = ReverseByteOrder(bits);

            for (int i = 0; i < bits.Length; i++)
            {
                result |= bits[i].ToUInt() << i;
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<UInt32>(bits, result);
            }

            return result;
        }

        public static short ToInt16(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 16)
                throw new ArgumentException("Too many bits for Int16.");

            if (!littleEndian) bits = ReverseByteOrder(bits);

            short result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result |= (short)(bits[i].ToInt() << i);
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<Int16>(bits, result);
            }

            return result;
        }

        public static ushort ToUInt16(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 16)
                throw new ArgumentException("Too many bits for UInt16.");

            if (!littleEndian) bits = ReverseByteOrder(bits);

            ushort result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result |= (ushort)(bits[i].ToInt() << i);
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<UInt32>(bits, result);
            }

            return result;
        }

        public static long ToInt64(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 64)
                throw new ArgumentException("Too many bits for Int64.");

            if (!littleEndian) bits = ReverseByteOrder(bits);

            long result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result |= (long)(bits[i].ToUlong() << i);
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<Int64>(bits, result);
            }

            return result;
        }

        public static ulong ToUInt64(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 64)
                throw new ArgumentException("Too many bits for UInt64.");

            if (!littleEndian) bits = ReverseByteOrder(bits);

            ulong result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result |= (ulong)bits[i].ToUlong() << i;
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<UInt64>(bits, result);
            }

            return result;
        }

        public static string ToStr(this Bit[] bits, bool littleEndian = true)
        {
            byte[] byteArray = new byte[bits.Length / 8];
            for (int i = 0; i < byteArray.Length; i++)
            {
                Bit[] byteBits = new Bit[8];
                Array.Copy(bits, i * 8, byteBits, 0, 8);
                byteArray[i] = (byte)byteBits.ToInt32(littleEndian);
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<string>(bits, Encoding.UTF8.GetString(byteArray));
            }

            return Encoding.UTF8.GetString(byteArray);
        }

        public static byte ToByte(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 8)
                throw new ArgumentException("Incorrect number of bits for byte.");

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<byte>(bits, (byte)bits.ToInt16(littleEndian));
            }

            return (byte)bits.ToInt16(littleEndian);
        }

        public static byte[] ToBytes(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length % 8 != 0)
                throw new ArgumentException("Number of bits must be a multiple of 8.");

            int numBytes = bits.Length / 8;
            byte[] byteArray = new byte[numBytes];

            for (int i = 0; i < numBytes; i++)
            {
                Bit[] byteBits = new Bit[8];
                Array.Copy(bits, i * 8, byteBits, 0, 8);
                byteArray[i] = byteBits.ToByte(littleEndian);
            }

            if (Debugger.IsAttached)
            {
                string bytes = "";
                foreach (byte bite in byteArray)
                {
                    bytes += bite.ToString("X2");
                }
                Debugging.WriteBitPositionMessage<string>(bits, bytes);
            }

            return byteArray;
        }

        public static char ToChar(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length > 8)
                throw new ArgumentException("Incorrect number of bits for char.");

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<char>(bits, (char)bits.ToInt16(littleEndian));
            }

            return (char)bits.ToInt16(littleEndian);
        }

        public static char[] ToCharArray(this Bit[] bits, bool littleEndian = true)
        {
            if (bits.Length % 8 != 0)
                throw new ArgumentException("Bit array length must be a multiple of 8 for char array conversion.");

            char[] chars = new char[bits.Length / 8];
            for (int i = 0; i < chars.Length; i++)
            {
                Bit[] charBits = new Bit[8];
                Array.Copy(bits, i * 8, charBits, 0, 8);
                chars[i] = charBits.ToChar(littleEndian);
            }

            if (Debugger.IsAttached)
            {
                Debugging.WriteBitPositionMessage<string>(bits, new string(chars));
            }

            return chars;
        }

        public static bool[] ConvertBitArrayToBoolArray(this Bit[] bits)
        {
            bool[] result = new bool[bits.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToBoolean(bits[i]);
            }
            return result;
        }

        public static string ToStringRepresentation(this Bit[] bits)
        {
            string result = "";
            foreach(Bit b in bits)
            {
                result += b.ToInt().ToString();
            }
            return result;
        }
    }
}
