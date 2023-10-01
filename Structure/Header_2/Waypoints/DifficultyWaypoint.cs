using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Waypoints
{
    public class DifficultyWaypoint
    {
        private readonly int BytePosition = 0;
        private readonly int BitPosition = 0;
        private const int Marker = 2;
        private const int bitfield = 8;
        private const int padding = 14;
        private const int Length = 24;

        private BitwiseBinaryReader? waypointReader { get; set; } = null;
        public byte[]? waypointData { get; set; } = null;

        public Act1? act1 { get; set; } = null;
        public Act2? act2 { get; set; } = null;
        public Act3? act3 { get; set; } = null;
        public Act4? act4 { get; set; } = null;
        public Act5? act5 { get; set; } = null;

        public DifficultyWaypoint(int offset = -1)
        {
            if (offset > 0)
            {
                BytePosition = offset;
                BitPosition = BytePosition * sizeof(long);
            }
        }

        public void ReadWaypointBytes(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            waypointReader = new BitwiseBinaryReader(reader.ReadBits(Length * 8));
            waypointData = waypointReader.ReadBits(Length*8).ToBytes();

            waypointReader.SetBitPosition(0);
            byte[] buff = waypointReader.ReadBits(16).ToBytes();
            if (buff[0] != 0x02 && buff[1] != 0x01)
                throw new Exception("Waypoint marker not found");

            act1 = Act1.ReadActBytes(waypointReader);
            var t = act1.Waypoints;
            act2 = Act2.ReadActBytes(waypointReader);
            var t2 = act2.Waypoints;
            act3 = Act3.ReadActBytes(waypointReader);
            var t3 = act3.Waypoints;
            act4 = Act4.ReadActBytes(waypointReader);
            var t4 = act4.Waypoints;
            act5 = Act5.ReadActBytes(waypointReader);
            var t5 = act5.Waypoints;
        }
    }
}
