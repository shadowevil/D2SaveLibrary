using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Player.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Other
{
    public class Mercenary
    {
        public bool Dead = false;
        public UInt32 Seed = UInt32.MaxValue;
        public UInt16 NameId = UInt16.MaxValue;
        public UInt16 MercType = UInt16.MaxValue;
        public UInt32 Experience = UInt32.MaxValue;

        public Mercenary() { }

        public static Mercenary Read(BitwiseBinaryReader mainReader)
        {
            Mercenary merc = new Mercenary();

            mainReader.SetBytePosition(OtherOffsets.OFFSET_MERCENARY_IS_DEAD.Offset);
            merc.Dead = (mainReader.ReadBits(OtherOffsets.OFFSET_MERCENARY_IS_DEAD.BitLength).ToUInt16() >= 0);

            mainReader.SetBytePosition(OtherOffsets.OFFSET_MERCENARY_SEED.Offset);
            merc.Seed = mainReader.ReadBits(OtherOffsets.OFFSET_MERCENARY_SEED.BitLength).ToUInt32();

            mainReader.SetBytePosition(OtherOffsets.OFFSET_MERCENARY_NAMEID.Offset);
            merc.NameId = mainReader.ReadBits(OtherOffsets.OFFSET_MERCENARY_NAMEID.BitLength).ToUInt16();

            mainReader.SetBytePosition(OtherOffsets.OFFSET_MERCENARY_TYPE.Offset);
            merc.MercType = mainReader.ReadBits(OtherOffsets.OFFSET_MERCENARY_TYPE.BitLength).ToUInt16();

            mainReader.SetBytePosition(OtherOffsets.OFFSET_MERCENARY_EXPERIENCE.Offset);
            merc.Experience = mainReader.ReadBits(OtherOffsets.OFFSET_MERCENARY_EXPERIENCE.BitLength).ToUInt32();

            return merc;
        }
    }
}
