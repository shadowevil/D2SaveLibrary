using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Waypoints
{
    public class WaypointBook
    {
        public string Signature = string.Empty;
        public UInt32 Version = UInt32.MaxValue;
        public UInt16 Size = UInt16.MaxValue;

        public WaypointDifficulty? Normal { get; set; } = null;
        public WaypointDifficulty? Nightmare { get; set; } = null;
        public WaypointDifficulty? Hell { get; set; } = null;

        public WaypointBook() { }

        public static WaypointBook Read(BitwiseBinaryReader mainReader)
        {
            WaypointBook book = new WaypointBook();

            mainReader.SetBytePosition(WaypointOffsets.OFFSET_SIGNATURE.Offset);
            book.Signature = mainReader.ReadBits(WaypointOffsets.OFFSET_SIGNATURE.BitLength).ToStr();
            if (book.Signature != WaypointOffsets.OFFSET_SIGNATURE.Signature)
                throw new OffsetException("Unable to locate waypoint signature, corrupt save?");

            mainReader.SetBytePosition(WaypointOffsets.OFFSET_VERSION.Offset);
            book.Version = mainReader.ReadBits(WaypointOffsets.OFFSET_VERSION.BitLength).ToUInt32();

            mainReader.SetBytePosition(WaypointOffsets.OFFSET_SIZE.Offset);
            book.Size = mainReader.ReadBits(WaypointOffsets.OFFSET_SIZE.BitLength).ToUInt16();

            book.Normal = WaypointDifficulty.Read(mainReader, WaypointOffsets.OFFSET_NORMAL);
            book.Nightmare = WaypointDifficulty.Read(mainReader, WaypointOffsets.OFFSET_NIGHTMARE);
            book.Hell = WaypointDifficulty.Read(mainReader, WaypointOffsets.OFFSET_HELL);

            return book;
        }
    }
}
