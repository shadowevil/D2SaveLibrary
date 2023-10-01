using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Player.Item.MagicalAffixes
{
    public class MagicalAttributes
    {
        private const UInt16 magicmindam = 52;
        private const UInt16 item_maxdamage_percent = 17;
        private const UInt16 firemindam = 48;
        private const UInt16 lightmindam = 50;
        private const UInt16 coldmindam = 54;
        private const UInt16 poisonmindam = 57;

        public HashSet<MagicalAttribute> MagicalList = new HashSet<MagicalAttribute>();

        public MagicalAttributes() { }

        public static MagicalAttributes? Read(BitwiseBinaryReader mainReader)
        {
            MagicalAttributes attributes = new MagicalAttributes();

            UInt16 Id = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID);
            while(Id != 0x1FF)
            {
                attributes.MagicalList.Add(MagicalAttribute.Read(mainReader, Id));
                switch(Id)
                {
                    case magicmindam:
                    case item_maxdamage_percent:
                    case firemindam:
                    case lightmindam:
                        attributes.MagicalList.Add(MagicalAttribute.Read(mainReader, (UInt16)(Id + 1)));
                        break;
                    case coldmindam:
                    case poisonmindam:
                        attributes.MagicalList.Add(MagicalAttribute.Read(mainReader, (UInt16)(Id + 1)));
                        attributes.MagicalList.Add(MagicalAttribute.Read(mainReader, (UInt16)(Id + 2)));
                        break;
                }
                Id = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID);
            }

            return attributes;
        }
    }

    public class MagicalAttribute
    {
        public ushort? Id { get; set; } = null;
        public string Attribute { get; set; } = string.Empty;
        public int? SkillTab { get; set; } = null;
        public int? SkillId { get; set; } = null;
        public int? SkillLevel { get; set; } = null;
        public int? MaxCharges { get; set; } = null;
        public int? Param { get; set; } = null;
        public int Value { get; set; } = 0;

        public static MagicalAttribute Read(BitwiseBinaryReader mainReader, UInt16 _id)
        {
            MagicalAttribute attrib = new MagicalAttribute();
            ItemStatCost? property = D2S.instance?.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Id == _id);
            if(property == null)
                throw new NullReferenceException(nameof(property));

            attrib.Id = _id;
            attrib.Attribute = property.Stat!;

            if(property.SaveParamBits != 0)
            {
                UInt32 SaveParam = mainReader.ReadItemBits<UInt32>(new ItemOffsetStruct(-1, (int)property.SaveParamBits!));
                UInt32 SaveBits = mainReader.ReadItemBits<UInt32>(new ItemOffsetStruct(-1, (int)property.SaveBits!));
            }

            return attrib;
        }
    }
}
