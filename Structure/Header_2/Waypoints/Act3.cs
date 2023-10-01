using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Waypoints
{
    public class Act3
    {
        public const int BytePosition = 2;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 8;
        public const int Act = 3 - 1;

        private byte? actByte { get; set; } = null;

        public static Act3 ReadActBytes(BitwiseBinaryReader? waypointMainReader)
        {
            if (waypointMainReader == null)
                throw new ArgumentNullException(nameof(waypointMainReader));

            Act3 act = new Act3();
            waypointMainReader.SetBitPosition(BitPosition + (Act * 8));
            act.actByte = waypointMainReader.ReadBits(8).ToByte();
            return act;
        }

        public bool[] Waypoints
        {
            get
            {
                if (actByte == null)
                    throw new ArgumentNullException(nameof(actByte));

                byte b = actByte.Value;
                bool[] bits = ByteArrayConverter.GetBitField(b);
                return bits;
            }
        }
    }
}
