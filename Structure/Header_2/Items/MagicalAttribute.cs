using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Items
{
    public class MagicalAttribute
    {
        private const UInt16 magicmindam = 52;
        private const UInt16 item_maxdamage_percent = 17;
        private const UInt16 firemindam = 48;
        private const UInt16 lightmindam = 50;
        private const UInt16 coldmindam = 54;
        private const UInt16 poisonmindam = 57;

        public UInt16? Id { get; set; }

        public MagicalAttribute(BitwiseBinaryReader reader)
        {
            if (reader.PeekBits(9).ToUInt16() == 0x1ff) return;
            Id = reader.ReadBits(9).ToUInt16();

            ItemStatCost? stat = D2S.instance?.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Id == Id);
            if (stat == null) throw new Exception($"No ItemStatCost record found for id: {Id} at bit position: {reader.bitPosition - 9}");
        }
    }
}
