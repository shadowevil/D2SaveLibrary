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
        public UInt16 Dead = UInt16.MaxValue;
        public UInt32 Seed = UInt32.MaxValue;
        public UInt16 NameId = UInt16.MaxValue;
        public UInt16 MercType = UInt16.MaxValue;
        public UInt32 Experience = UInt32.MaxValue;

        public Mercenary() { }

        public static Mercenary Read(BitwiseBinaryReader mainReader)
        {
            Mercenary merc = new Mercenary();

            merc.Dead = mainReader.Read<UInt16>(OtherOffsets.OFFSET_MERCENARY_IS_DEAD);
            merc.Seed = mainReader.Read<UInt32>(OtherOffsets.OFFSET_MERCENARY_SEED);
            merc.NameId = mainReader.Read<UInt16>(OtherOffsets.OFFSET_MERCENARY_NAMEID);
            merc.MercType = mainReader.Read<UInt16>(OtherOffsets.OFFSET_MERCENARY_TYPE);
            merc.Experience = mainReader.Read<UInt32>(OtherOffsets.OFFSET_MERCENARY_EXPERIENCE);

            return merc;
        }

        public bool Write(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != OtherOffsets.OFFSET_MERCENARY_IS_DEAD.Offset)
            {
                if(writer.GetBytes().Length + 2 != OtherOffsets.OFFSET_MERCENARY_IS_DEAD.Offset)
                    return false;

                writer.WriteVoidBits(2 * 8);
            }

            writer.WriteBits(Dead.ToBits((uint)OtherOffsets.OFFSET_MERCENARY_IS_DEAD.BitLength));

            if (writer.GetBytes().Length != OtherOffsets.OFFSET_MERCENARY_SEED.Offset)
                return false;

            writer.WriteBits(Seed.ToBits((uint)OtherOffsets.OFFSET_MERCENARY_SEED.BitLength));

            if (writer.GetBytes().Length != OtherOffsets.OFFSET_MERCENARY_NAMEID.Offset)
                return false;

            writer.WriteBits(NameId.ToBits((uint)OtherOffsets.OFFSET_MERCENARY_NAMEID.BitLength));

            if (writer.GetBytes().Length != OtherOffsets.OFFSET_MERCENARY_TYPE.Offset)
                return false;

            writer.WriteBits(MercType.ToBits((uint)OtherOffsets.OFFSET_MERCENARY_TYPE.BitLength));

            if (writer.GetBytes().Length != OtherOffsets.OFFSET_MERCENARY_EXPERIENCE.Offset)
                return false;

            writer.WriteBits(Experience.ToBits((uint)OtherOffsets.OFFSET_MERCENARY_EXPERIENCE.BitLength));

            return true;
        }
    }
}
