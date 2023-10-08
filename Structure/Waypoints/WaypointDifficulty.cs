using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Waypoints
{
    public class WaypointDifficulty
    {
        private readonly byte[] Marker = new byte[2] { 0x02, 0x01 };
        public HashSet<Act> Acts { get; set; } = new HashSet<Act>();

        public WaypointDifficulty() { }

        public static WaypointDifficulty? Read(BitwiseBinaryReader mainReader, OffsetStruct byteOffset)
        {
            WaypointDifficulty dif = new WaypointDifficulty();

            mainReader.SetBytePosition(byteOffset.Offset);
            byte[] marker = mainReader.ReadBits(byteOffset.BitLength).ToBytes();
            if (marker[0] != dif.Marker[0] && marker[1] != dif.Marker[1])
                throw new OffsetException("Error reading waypoint difficulty marker, corrupt save?");

            dif.Acts.Add(Act.Read(mainReader, WaypointOffsets.ACT1_WAYPOINT_COUNT, byteOffset.Offset + WaypointOffsets.OFFSET_ACT1.Offset));
            dif.Acts.Add(Act.Read(mainReader, WaypointOffsets.ACT2_WAYPOINT_COUNT, byteOffset.Offset + WaypointOffsets.OFFSET_ACT2.Offset));
            dif.Acts.Add(Act.Read(mainReader, WaypointOffsets.ACT3_WAYPOINT_COUNT, byteOffset.Offset + WaypointOffsets.OFFSET_ACT3.Offset));
            dif.Acts.Add(Act.Read(mainReader, WaypointOffsets.ACT4_WAYPOINT_COUNT, byteOffset.Offset + WaypointOffsets.OFFSET_ACT4.Offset));
            dif.Acts.Add(Act.Read(mainReader, WaypointOffsets.ACT5_WAYPOINT_COUNT, byteOffset.Offset + WaypointOffsets.OFFSET_ACT5.Offset));

            return dif;
        }

        public bool WriteDifficulty(BitwiseBinaryWriter writer, int difficultyOffset)
        {
            if (writer.GetBytes().Length != difficultyOffset)
                return false;
           writer.WriteBits(Marker.ToBits());

            foreach(Act a in Acts)
            {
                a.WriteWaypoints(writer);
            }

           writer.WriteVoidBits(17 * 8);
            return true;
        }
    }
}
