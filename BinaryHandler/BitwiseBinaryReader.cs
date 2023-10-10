using D2SLib2.Structure;
using D2SLib2.Structure.Player.Item;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.BinaryHandler
{
    public class BitwiseBinaryReader
    {
        public static BitwiseBinaryReader? instance = null;
        public static BitwiseBinaryReader? subinstance = null;
        public Bit[] BitArray { get; private set; } = new Bit[1];
        public byte[]? ByteArray { get; private set; } = null;
        public int bitPosition { get; private set; } = 0;
        public int bytePosition => bitPosition / 8;

        public BitwiseBinaryReader(string filename)
        {
            ByteArray = File.ReadAllBytes(filename);
            BitArray = ByteArray.ToBits().ToArray();
            instance = this;
        }

        public BitwiseBinaryReader(Bit[] bitArray)
        {
            BitArray = bitArray;
            ByteArray = bitArray.ToBytes((uint)bitArray.Length, Endianness.BigEndian);
            subinstance = this;
        }

        public void SetBytePosition(int bytePosition) => bitPosition = bytePosition * 8;
        public void SetBitPosition(int _bitPosition) => bitPosition = _bitPosition;
        public void SkipBytes(int bytes) => bitPosition += bytes * 8;
        public void SkipBits(int bits) => bitPosition += bits;

        // This rounds to the nearest 8th byte
        public void Align() => bitPosition = (bitPosition + 7) & ~7; 

        public Bit ReadBit()
        {
            if (bitPosition >= BitArray.Length)
                throw new ArgumentException("Reached the end of the bit array.");

            return BitArray[bitPosition++];
        }

        public Bit PeekBit()
        {
            if (bitPosition >= BitArray.Length)
                throw new ArgumentException("Reached the end of the bit array.");

            return BitArray[bitPosition];
        }

        public Bit[] ReadBits(int numBits)
        {
            if (bitPosition + numBits > BitArray.Length)
            {
                throw new ArgumentException("Not enough bits to read.");
            }

            Bit[] result = new Bit[numBits];

            for (int i = 0; i < numBits; i++)
            {
                result[i] = BitArray[bitPosition + i];
            }

            bitPosition += numBits; // Move by the number of bits read
            return result;
        }

        public Bit[] PeekBits(int numBits)
        {
            if (bitPosition + numBits > BitArray.Length)
            {
                throw new ArgumentException("Not enough bits to read.");
            }

            Bit[] result = new Bit[numBits];

            for (int i = 0; i < numBits; i++)
            {
                result[i] = BitArray[bitPosition + i];
            }

            return result;
        }

        public T? ReadItemBits<T>(ItemOffsetStruct iStruct, int offset = 0, Endianness endianness = Endianness.BigEndian)
        {
            return Read<T>(iStruct.BitOffset + Inventory.InventoryOffset + offset, iStruct.BitLength, endianness, true);
        }

        public T? PeekItemBits<T>(ItemOffsetStruct iStruct, int offset = 0, Endianness endianness = Endianness.BigEndian)
        {
            return Peek<T>(iStruct.BitOffset + Inventory.InventoryOffset + offset, iStruct.BitLength, endianness);
        }

        public T? Peek<T>(OffsetStruct oStruct, int offset = 0, Endianness endianness = Endianness.BigEndian)
        {
            return Peek<T>(oStruct.BitOffset + Inventory.InventoryOffset + offset, oStruct.BitLength, endianness);
        }

        public T? Read<T>(OffsetStruct oStruct, int bitOffset = 0, Endianness endianness = Endianness.BigEndian)
        {
            if (!oStruct.isUsingBitOffset)
            {
                return Read<T>((oStruct.Offset * 8) + bitOffset, oStruct.BitLength, endianness);
            } else
            {
                return Read<T>(oStruct.BitOffset + bitOffset, oStruct.BitLength, endianness);
            }
        }

        public T? ReadSkipPositioning<T>(OffsetStruct oStruct, int bitOffset = 0, Endianness endianness = Endianness.BigEndian)
        {
            if (!oStruct.isUsingBitOffset)
            {
                return Read<T>((oStruct.Offset * 8) + bitOffset, oStruct.BitLength, endianness, true);
            }
            else
            {
                return Read<T>(oStruct.BitOffset + bitOffset, oStruct.BitLength, endianness, true);
            }
        }


        private T? Peek<T>(int offset, int bitLength, Endianness endianness)
        {
            if (bitLength <= 0) return default;

            if (typeof(T) == typeof(Bit) && bitLength == 1)
                return (T)(object)PeekBit();

            if (typeof(T) == typeof(Bit[]) && bitLength > 8)
                return (T)(object)PeekBits(bitLength);

            if (typeof(T) == typeof(byte) && bitLength <= 8)
                return (T)(object)(byte)PeekBits(bitLength).ToUInt16((uint)bitLength, endianness);

            if (typeof(T) == typeof(byte[]) && bitLength % 8 == 0)
                return (T)(object)PeekBits(bitLength).ToBytes((uint)bitLength, endianness);

            if (typeof(T) == typeof(char) && bitLength <= 8)
                return (T)(object)PeekBits(bitLength).ToChar((uint)bitLength, endianness);

            if (typeof(T) == typeof(char[]) && bitLength > 8 && bitLength % 8 == 0)
                return (T)(object)PeekBits(bitLength).ToCharArray((uint)bitLength, endianness);

            if (typeof(T) == typeof(string) && bitLength % 8 == 0)
                return (T)(object)PeekBits(bitLength).ToString((uint)bitLength, endianness);

            if (typeof(T) == typeof(UInt16) && bitLength <= 16)
                return (T)(object)PeekBits(bitLength).ToUInt16((uint)bitLength, endianness);

            if (typeof(T) == typeof(UInt32) && bitLength <= 32)
                return (T)(object)PeekBits(bitLength).ToUInt32((uint)bitLength, endianness);

            return default;
        }

        private T? Read<T>(int bitOffset, int bitLength, Endianness endianness, bool skipBitPositioning = false)
        {
            int prevBitPosition = bitPosition;
            if (!skipBitPositioning)
            {
                SetBitPosition(bitOffset);
            }

            if (bitLength <= 0) return default;

            if (typeof(T) == typeof(Bit) && bitLength == 1)
                return (T)(object)ReadBit();

            if (typeof(T) == typeof(Bit[]) && bitLength >= 8)
                return (T)(object)ReadBits(bitLength);

            if (typeof(T) == typeof(byte) && bitLength <= 8)
                return (T)(object)ReadBits(bitLength).ToByte((uint)bitLength, endianness);

            if (typeof(T) == typeof(byte[]) && bitLength % 8 == 0)
                return (T)(object)ReadBits(bitLength).ToBytes((uint)bitLength, endianness);

            if (typeof(T) == typeof(char) && bitLength <= 8)
                return (T)(object)ReadBits(bitLength).ToChar((uint)bitLength, endianness);

            if (typeof(T) == typeof(char[]) && bitLength > 8 && bitLength % 8 == 0)
                return (T)(object)ReadBits(bitLength).ToCharArray((uint)bitLength, endianness);

            if (typeof(T) == typeof(string) && bitLength % 8 == 0)
                return (T)(object)ReadBits(bitLength).ToString((uint)bitLength, endianness);

            if (typeof(T) == typeof(UInt16) && bitLength <= 16)
                return (T)(object)ReadBits(bitLength).ToUInt16((uint)bitLength, endianness);

            if (typeof(T) == typeof(UInt32) && bitLength <= 32)
                return (T)(object)ReadBits(bitLength).ToUInt32((uint)bitLength, endianness);

            if (!skipBitPositioning)
            {
                SetBitPosition(prevBitPosition);
            }

            return default;
        }
    }
}
