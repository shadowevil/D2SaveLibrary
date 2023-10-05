﻿using D2SLib2.Structure;
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
            BitArray = Bit.ConvertByteArrayToBitArray(ByteArray);
            instance = this;
        }

        public BitwiseBinaryReader(Bit[] bitArray)
        {
            BitArray = bitArray;
            ByteArray = bitArray.ToBytes();
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

        public byte ReadByte(bool littleEndian = true)
        {
            Bit[] bits = ReadBits(8);

            return (byte)bits.ToUInt32(littleEndian);
        }

        public byte[] ReadBytes(int numBytes, bool littleEndian = true)
        {
            byte[] result = new byte[numBytes];
            for (int i = 0; i < numBytes; i++)
            {
                result[i] = ReadByte(littleEndian);
            }

            return result;
        }

        public UInt16 ReadUInt16(int numBits = 16, bool littleEndian = true)
        {
            Bit[] bits = ReadBits(numBits);
            return bits.ToUInt16(littleEndian);
        }

        public Int16 ReadInt16(int numBits = 16, bool littleEndian = true)
        {
            Bit[] bits = ReadBits(numBits);
            return bits.ToInt16(littleEndian);
        }

        public Int32 ReadInt32(int numBits = 32, bool littleEndian = true)
        {
            Bit[] bits = ReadBits(numBits);
            return bits.ToInt32(littleEndian);
        }

        public UInt32 ReadUInt32(int numBits = 32, bool littleEndian = true)
        {
            Bit[] bits = ReadBits(numBits);
            return bits.ToUInt32(littleEndian);
        }

        public Int64 ReadInt64(int numBits = 64, bool littleEndian = true)
        {
            Bit[] bits = ReadBits(numBits);
            return bits.ToInt64(littleEndian);
        }

        public UInt64 ReadUInt64(int numBits = 64, bool littleEndian = true)
        {
            Bit[] bits = ReadBits(numBits);
            return bits.ToUInt64(littleEndian);
        }

        public string ReadString(int numBytes, bool littleEndian = true)
        {
            byte[] bytes = ReadBytes(numBytes, littleEndian);
            return Encoding.UTF8.GetString(bytes);
        }

        public char ReadChar(bool littleEndian = true)
        {
            byte[] bytes = ReadBytes(1, littleEndian);
            return BitConverter.ToChar(bytes, 0);
        }

        public char[] ReadChars(int numChars, bool littleEndian = true)
        {
            char[] result = new char[numChars];
            for (int i = 0; i < numChars; i++)
            {
                result[i] = ReadChar(littleEndian);
            }
            return result;
        }

        public T? Read<T>(ItemOffsetStruct iStruct, int offset = 0, bool littleEndian = true)
        {
            return Read<T>(iStruct.BitOffset + Inventory.InventoryOffset + offset, iStruct.BitLength, littleEndian);
        }

        public T? ReadItemBits<T>(ItemOffsetStruct iStruct, int offset = 0, bool littleEndian = true)
        {
            return Read<T>(iStruct.BitOffset + Inventory.InventoryOffset + offset, iStruct.BitLength, littleEndian, true);
        }

        public T? PeekItemBits<T>(ItemOffsetStruct iStruct, int offset = 0, bool littleEndian = true)
        {
            return Peek<T>(iStruct.BitOffset + Inventory.InventoryOffset + offset, iStruct.BitLength, littleEndian);
        }

        public T? Read<T>(OffsetStruct oStruct, int bitOffset = 0, bool littleEndian = true)
        {
            return Read<T>((oStruct.Offset * 8) + bitOffset, oStruct.BitLength, littleEndian);
        }

        public T? ReadSkipPositioning<T>(OffsetStruct oStruct, int bitOffset = 0, bool littleEndian = true)
        {
            return Read<T>((oStruct.Offset * 8) + bitOffset, oStruct.BitLength, littleEndian, true);
        }


        private T? Peek<T>(int offset, int bitLength, bool littleEndian)
        {
            if (bitLength <= 0) return default;

            if (typeof(T) == typeof(Bit) && bitLength == 1)
                return (T)(object)PeekBit();

            if (typeof(T) == typeof(Bit[]) && bitLength > 8)
                return (T)(object)PeekBits(bitLength);

            if (typeof(T) == typeof(byte) && bitLength <= 8)
                return (T)(object)PeekBits(bitLength).ToByte(littleEndian);

            if (typeof(T) == typeof(byte[]) && bitLength % 8 == 0)
                return (T)(object)PeekBits(bitLength).ToBytes(littleEndian);

            if (typeof(T) == typeof(char) && bitLength <= 8)
                return (T)(object)PeekBits(bitLength).ToChar(littleEndian);

            if (typeof(T) == typeof(char[]) && bitLength > 8 && bitLength % 8 == 0)
                return (T)(object)PeekBits(bitLength).ToCharArray(littleEndian);

            if (typeof(T) == typeof(string) && bitLength % 8 == 0)
                return (T)(object)PeekBits(bitLength).ToStr(littleEndian);

            if (typeof(T) == typeof(UInt16) && bitLength <= 16)
                return (T)(object)PeekBits(bitLength).ToUInt16(littleEndian);

            if (typeof(T) == typeof(UInt32) && bitLength <= 32)
                return (T)(object)PeekBits(bitLength).ToUInt32(littleEndian);

            if (typeof(T) == typeof(UInt64) && bitLength <= 64)
                return (T)(object)PeekBits(bitLength).ToUInt64(littleEndian);

            return default;
        }

        private T? Read<T>(int bitOffset, int bitLength, bool littleEndian, bool skipBitPositioning = false)
        {
            int prevBitPosition = bitPosition;
            if (!skipBitPositioning)
            {
                SetBitPosition(bitOffset);
            }

            var newbits = PeekBits(bitLength + 8);

            if (bitLength <= 0) return default;

            if (typeof(T) == typeof(Bit) && bitLength == 1)
                return (T)(object)ReadBit();

            if (typeof(T) == typeof(Bit[]) && bitLength >= 8)
                return (T)(object)ReadBits(bitLength);

            if (typeof(T) == typeof(byte) && bitLength <= 8)
                return (T)(object)ReadBits(bitLength).ToByte(littleEndian);

            if (typeof(T) == typeof(byte[]) && bitLength % 8 == 0)
                return (T)(object)ReadBits(bitLength).ToBytes(littleEndian);

            if (typeof(T) == typeof(char) && bitLength <= 8)
                return (T)(object)ReadBits(bitLength).ToChar(littleEndian);

            if (typeof(T) == typeof(char[]) && bitLength > 8 && bitLength % 8 == 0)
                return (T)(object)ReadBits(bitLength).ToCharArray(littleEndian);

            if (typeof(T) == typeof(string) && bitLength % 8 == 0)
                return (T)(object)ReadBits(bitLength).ToStr(littleEndian);

            if (typeof(T) == typeof(UInt16) && bitLength <= 16)
                return (T)(object)ReadBits(bitLength).ToUInt16(littleEndian);

            if (typeof(T) == typeof(UInt32) && bitLength <= 32)
                return (T)(object)ReadBits(bitLength).ToUInt32(littleEndian);

            if (typeof(T) == typeof(UInt64) && bitLength <= 64)
                return (T)(object)ReadBits(bitLength).ToUInt64(littleEndian);

            if (!skipBitPositioning)
            {
                SetBitPosition(prevBitPosition);
            }

            return default;
        }
    }
}
