using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
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

        public static Header Read(BitwiseBinaryReader mainReader)
        {
            Header _header = new Header();
            _header.Signature = mainReader.Read<UInt32>(HeaderOffsets.OFFSET_SIGNATURE).ToString("X8");
            _header.Version = (FileVersion)mainReader.Read<UInt32>(HeaderOffsets.OFFSET_VERSION);
            _header.FileSize = mainReader.Read<UInt32>(HeaderOffsets.OFFSET_FILESIZE);
            _header.FileSizeBits = _header.FileSize * 8;

            if (mainReader.BitArray.Length != _header.FileSizeBits || mainReader.ByteArray!.Length != _header.FileSize)
                throw new FileLoadException("File size missmatch, corrupt save?");

            _header.checksum = "0x" + mainReader.Read<UInt32>(HeaderOffsets.OFFSET_CHECKSUM).ToString("X8");
            var calcChecksum = Checksum.CalculateChecksum(mainReader.ByteArray);
            if (!_header.checksum.Equals(calcChecksum))
                throw new Exception("File checksum not calculated correctly. Corrupt save?");

            return _header;
        }
    }
}
