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
        public string? Signature = string.Empty;
        public UInt32 Version = UInt32.MaxValue;
        public UInt16 Size = UInt16.MaxValue;

        public WaypointDifficulty? Normal { get; set; } = null;
        public WaypointDifficulty? Nightmare { get; set; } = null;
        public WaypointDifficulty? Hell { get; set; } = null;

        public WaypointBook() { }

        public static WaypointBook Read(BitwiseBinaryReader mainReader)
        {
            WaypointBook book = new WaypointBook();

            book.Signature = mainReader.Read<string>(WaypointOffsets.OFFSET_SIGNATURE);
            if (book.Signature != WaypointOffsets.OFFSET_SIGNATURE.Signature)
                throw new OffsetException("Unable to locate waypoint signature, corrupt save?");

            book.Version = mainReader.Read<UInt32>(WaypointOffsets.OFFSET_VERSION);
            book.Size = mainReader.Read<UInt16>(WaypointOffsets.OFFSET_SIZE);

            book.Normal = WaypointDifficulty.Read(mainReader, WaypointOffsets.OFFSET_NORMAL);
            book.Nightmare = WaypointDifficulty.Read(mainReader, WaypointOffsets.OFFSET_NIGHTMARE);
            book.Hell = WaypointDifficulty.Read(mainReader, WaypointOffsets.OFFSET_HELL);

            return book;
        }

        public bool WriteWaypoints(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != WaypointOffsets.OFFSET_SIGNATURE.Offset)
                return false;
           writer.WriteBits(WaypointOffsets.OFFSET_SIGNATURE.Signature.ToBits());
           writer.WriteBits(Version.ToBits((uint)WaypointOffsets.OFFSET_VERSION.BitLength));
           writer.WriteBits(Size.ToBits((uint)WaypointOffsets.OFFSET_SIZE.BitLength));

            Normal!.WriteDifficulty(writer, WaypointOffsets.OFFSET_NORMAL.Offset);
            Nightmare!.WriteDifficulty(writer, WaypointOffsets.OFFSET_NIGHTMARE.Offset);
            Hell!.WriteDifficulty(writer, WaypointOffsets.OFFSET_HELL.Offset);

            return true;
        }
    }
}
