using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public sealed class Header
    {
        public string Signature = string.Empty;
        public FileVersion Version = FileVersion.V00_To_06;
        public UInt32 FileSize = UInt32.MaxValue;
        public UInt32 FileSizeBits = UInt32.MaxValue;
        public string checksum = string.Empty;

        public Header() { }

        public static Header? Read(BitwiseBinaryReader mainReader)
        {
            Header _header = new Header();
            _header.Signature = mainReader.Read<UInt32>(HeaderOffsets.OFFSET_SIGNATURE).ToString("X8");
            Logger.WriteSection(mainReader, HeaderOffsets.OFFSET_SIGNATURE.BitLength, $"Signature: {_header.Signature}");

            _header.Version = (FileVersion)mainReader.Read<UInt32>(HeaderOffsets.OFFSET_VERSION);
            Logger.WriteSection(mainReader, HeaderOffsets.OFFSET_VERSION.BitLength, $"Version: {_header.Version}");

            if(_header.Version != FileVersion.D2R_V14x_LATEST)
            {
                Logger.WriteLine(0, 0, "WRONG VERSION");
                D2S.instance!.CloseFlag = true;
                return null;
            }

            _header.FileSize = mainReader.Read<UInt32>(HeaderOffsets.OFFSET_FILESIZE);
            Logger.WriteSection(mainReader, HeaderOffsets.OFFSET_FILESIZE.BitLength, $"File Size: {_header.FileSize}");

            _header.FileSizeBits = _header.FileSize * 8;

            if (mainReader.BitArray.Length != _header.FileSizeBits || mainReader.ByteArray!.Length != _header.FileSize)
                throw new FileLoadException("File size missmatch, corrupt save?");

            _header.checksum = "0x" + mainReader.Read<UInt32>(HeaderOffsets.OFFSET_CHECKSUM).ToString("X8");
            var calcChecksum = Checksum.CalculateChecksum(mainReader.ByteArray);
            Logger.WriteSection(mainReader, HeaderOffsets.OFFSET_CHECKSUM.BitLength, $"Checksum: {_header.checksum} == {calcChecksum}");

            if (!_header.checksum.Equals(calcChecksum))
                throw new Exception("File checksum not calculated correctly. Corrupt save?");

            return _header;
        }

        public bool Write(BitwiseBinaryWriter mainWriter, BitwiseBinaryReader mainReader)
        {
            if (mainWriter.GetBytes().Length != HeaderOffsets.OFFSET_SIGNATURE.Offset)
                return false;
            mainWriter.WriteBits(UInt32.Parse(Signature, NumberStyles.HexNumber, CultureInfo.InvariantCulture).ToBits((uint)HeaderOffsets.OFFSET_SIGNATURE.BitLength, Endianness.BigEndian));

            if (mainWriter.GetBytes().Length != HeaderOffsets.OFFSET_VERSION.Offset)
                return false;
            mainWriter.WriteBits(((int)Version).ToBits((uint)HeaderOffsets.OFFSET_VERSION.BitLength, Endianness.LittleEndian));

            if (mainWriter.GetBytes().Length != HeaderOffsets.OFFSET_FILESIZE.Offset)
                return false;
            mainWriter.WriteVoidBits(32);

            if (mainWriter.GetBytes().Length != HeaderOffsets.OFFSET_CHECKSUM.Offset)
                return false;
            mainWriter.WriteVoidBits((uint)HeaderOffsets.OFFSET_CHECKSUM.BitLength);

            return true;
        }

        public static void WriteNewFileSize(ref byte[] bytes, uint size)
        {
            byte[] newFileSizeBytes = bytes.Length.ToBits(32).ToBytes(32);
            for(int i= HeaderOffsets.OFFSET_FILESIZE.Offset; i<HeaderOffsets.OFFSET_FILESIZE.Offset+4; i++)
            {
                bytes[i] = newFileSizeBytes[i - HeaderOffsets.OFFSET_FILESIZE.Offset];
            }
        }
    }
}
