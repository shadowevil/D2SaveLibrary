using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Waypoints
{
    public class Act
    {
        private int WaypointCount = 0;

        private Bit[] Waypoints = new Bit[8];
        public bool Waypoint1 { get => Waypoints[0]; set => Waypoints[0] = new Bit(value); }
        public bool Waypoint2 { get => Waypoints[1]; set => Waypoints[1] = new Bit(value); }
        public bool Waypoint3 { get => Waypoints[2]; set => Waypoints[2] = new Bit(value); }
        public bool Waypoint4 { get => Waypoints[3]; set => Waypoints[3] = new Bit(value); }
        public bool Waypoint5 { get => Waypoints[4]; set => Waypoints[4] = new Bit(value); }
        public bool Waypoint6 { get => Waypoints[5]; set => Waypoints[5] = new Bit(value); }
        public bool Waypoint7 { get => Waypoints[6]; set => Waypoints[6] = new Bit(value); }
        public bool Waypoint8 { get => Waypoints[7]; set => Waypoints[7] = new Bit(value); }

        public Act(int waypointCount) => WaypointCount = waypointCount;

        public static Act Read(BitwiseBinaryReader mainReader, int waypointCount, int byteOffset)
        {
            Act act = new Act(waypointCount);

            mainReader.SetBytePosition(byteOffset);
            act.Waypoints = mainReader.ReadBits(8);

            return act;
        }

        public bool WriteWaypoints(BitwiseBinaryWriter writer)
        {
           writer.WriteBits(Waypoints.ToByte().ToBits());
            return true;
        }
    }
}
