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

        public Bit[] Waypoints = new Bit[8];

        public Act(int waypointCount) => WaypointCount = waypointCount;

        public static Act Read(BitwiseBinaryReader mainReader, int waypointCount, int byteOffset)
        {
            Act act = new Act(waypointCount);

            mainReader.SetBytePosition(byteOffset);

            if (act.WaypointCount < 1) return act;
            act.Waypoints[0] = mainReader.ReadBit();

            if (act.WaypointCount < 2) return act;
            act.Waypoints[1] = mainReader.ReadBit();

            if (act.WaypointCount < 3) return act;
            act.Waypoints[2] = mainReader.ReadBit();

            if (act.WaypointCount < 4) return act;
            act.Waypoints[3] = mainReader.ReadBit();

            if (act.WaypointCount < 5) return act;
            act.Waypoints[4] = mainReader.ReadBit();

            if (act.WaypointCount < 6) return act;
            act.Waypoints[5] = mainReader.ReadBit();

            if (act.WaypointCount < 7) return act;
            act.Waypoints[6] = mainReader.ReadBit();

            if (act.WaypointCount < 8) return act;
            act.Waypoints[7] = mainReader.ReadBit();

            return act;
        }
    }
}
