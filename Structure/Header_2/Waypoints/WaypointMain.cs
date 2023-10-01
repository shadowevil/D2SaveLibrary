using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Header.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Waypoints
{
    public class WaypointMain
    {
        public static WaypointMain? instance { get; private set; } = null;
        private const int BytePosition = 633;
        private const int BitPosition = BytePosition * sizeof(long);
        private const int Length = 80;

        private const int NormalOffset = 0 + WaypointHeader.Length;
        private const int NightmareOffset = 24 + WaypointHeader.Length;
        private const int HellOffset = 48 + WaypointHeader.Length;

        private BitwiseBinaryReader? waypointReader { get; set; } = null;

        public DifficultyWaypoint? Normal { get; set; } = null;
        public DifficultyWaypoint? Nightmare { get; set; } = null;
        public DifficultyWaypoint? Hell { get; set; } = null;

        public static WaypointMain ReadWaypointBytes(BitwiseBinaryReader reader)
        {
            instance = new WaypointMain();
            reader.SetBitPosition(BitPosition);
            instance.waypointReader = new BitwiseBinaryReader(reader.ReadBits(Length * 8));

            instance.Normal = new DifficultyWaypoint(NormalOffset);
            instance.Normal.ReadWaypointBytes(instance.waypointReader);

            instance.Nightmare = new DifficultyWaypoint(NightmareOffset);
            instance.Nightmare.ReadWaypointBytes(instance.waypointReader);

            instance.Hell = new DifficultyWaypoint(HellOffset);
            instance.Hell.ReadWaypointBytes(instance.waypointReader);

            return instance;
        }

        public string Descriptor
        {
            get
            {
                if (waypointReader == null)
                    throw new ArgumentNullException(nameof(waypointReader));

                return WaypointHeader.GetDescriptor(waypointReader);
            }
        }

        public UInt32 Version
        {
            get
            {
                if (waypointReader == null)
                    throw new ArgumentNullException(nameof(waypointReader));

                return WaypointHeader.GetVersion(waypointReader);
            }
        }

        public UInt16 Size
        {
            get
            {
                if (waypointReader == null)
                    throw new ArgumentNullException(nameof(waypointReader));

                return WaypointHeader.GetSize(waypointReader);
            }
        }
    }
}
