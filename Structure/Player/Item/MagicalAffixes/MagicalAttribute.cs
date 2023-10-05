using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Player.Item.MagicalAffixes
{
    public class MagicalAttributes
    {
        public HashSet<MagicalAttribute> MagicalList = new HashSet<MagicalAttribute>();

        public MagicalAttributes() { }

        public static MagicalAttributes? Read(BitwiseBinaryReader mainReader)
        {
            MagicalAttributes attributes = new MagicalAttributes();

            UInt16 Id = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID.BitLength, $"Magical Attribute Id: {Id}");
            while(Id != 0x1ff)
            {
                if (Id > D2S.instance!.dbContext!.ItemStatCosts.OrderBy(x => x.Id).Last().Id)
                {
                    Logger.Close();
                    throw new Exception($"Invalid stat Id: {Id} at position {mainReader.bitPosition - 9}");
                }

                ItemStatCost? statcost = D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == Id);
                if (statcost == null)
                    throw new Exception($"Stat Id {Id} not found in ItemStatCost");

                IQueryable<Property>? properties = D2S.instance?.dbContext?.Properties?.Where(x => x.Stat1 == statcost.Stat
                                                                                                && x.Code != "all-stats"
                                                                                                && x.Code != "res-all"
                                                                                                && x.Code != "res-all-max"
                                                                                                && x.Code != "dmg-elem"
                                                                                                && x.Code != "dmg-elem-min"
                                                                                                && x.Code != "dmg-elem-max");
                
                if (properties == null)
                    throw new Exception($"Property not found for {Id} in the Properties datastore");

                List<ItemStatCost?> ItemStatCostSet = new List<ItemStatCost?>()
                {
                    statcost
                };
                foreach (var item in properties)
                {
                    if(item.Stat2 != null || item.Stat3 != null || item.Stat4 != null
                        || item.Stat5 != null || item.Stat6 != null || item.Stat7 != null)
                    {
                        List<ItemStatCost?> stats = new List<ItemStatCost?>();
                        if (item.Stat2 != null)
                        {
                            stats.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Stat == item.Stat2));
                        }
                        if (item.Stat3 != null)
                        {
                            stats.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Stat == item.Stat3));
                        }
                        if (item.Stat4 != null)
                        {
                            stats.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Stat == item.Stat4));
                        }
                        if (item.Stat5 != null)
                        {
                            stats.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Stat == item.Stat5));
                        }
                        if (item.Stat6 != null)
                        {
                            stats.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Stat == item.Stat6));
                        }
                        if (item.Stat7 != null)
                        {
                            stats.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Stat == item.Stat7));
                        }
                        ItemStatCostSet.AddRange(stats);
                    }
                }

                switch(Id)
                {
                    case 17:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == 18));
                        break;
                    case 159:
                    case 21:
                        ItemStatCostSet.RemoveAt(ItemStatCostSet.Count - 1);
                        break;
                }

                foreach(var item in ItemStatCostSet)
                {
                    attributes.MagicalList.Add(MagicalAttribute.Read(mainReader, item));
                }
                Id = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID.BitLength, $"Magical Attribute Id: {Id}");
            }

            return attributes;
        }
    }

    [DebuggerDisplay("{Id}: {Attribute}")]
    public class MagicalAttribute
    {
        public ushort Id { get; set; } = 0;
        public string Attribute { get; set; } = string.Empty;
        public int? SkillTab { get; set; } = null;
        public int? SkillId { get; set; } = null;
        public int? SkillLevel { get; set; } = null;
        public int? MaxCharges { get; set; } = null;
        public int? CurrentCharges {  get; set; } = null;
        public int? Param { get; set; } = null;
        public uint Value { get; set; } = 0;

        public static MagicalAttribute Read(BitwiseBinaryReader mainReader, ItemStatCost property)
        {
            MagicalAttribute attrib = new MagicalAttribute();

            attrib.Id = (ushort)property.Id;
            attrib.Attribute = property.Stat!;

            if(property.SaveParamBits != null)
            {
                UInt16 param = mainReader.ReadItemBits<UInt16>(new ItemOffsetStruct(-1, (int)property.SaveParamBits!));
                Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)property.SaveParamBits!).BitLength, $"[{attrib.Id}]{attrib.Attribute} Param: {param}");
                switch (property.Descfunc)
                {
                    case 14: // +skill to skilltab
                        break;
                }

                // Encode
                if (property.Encode != null)
                {
                    switch (property.Encode)
                    {
                        case 1:
                        case 2: // Chance to cast
                        case 3: // Charges
                            attrib.SkillLevel = param & 63;
                            attrib.SkillId = (param >> 6) & 1023;
                            break;
                        default:
                            break;
                    }
                }

                attrib.Param = param;
            }
            if(property.SaveBits != null)
            {
                uint value = 0;
                if (attrib.Id == 288 || attrib.Id == 268 || attrib.Id == 269)
                {
                    value = mainReader.ReadItemBits<UInt32>(new ItemOffsetStruct(-1, (int)property.SaveBits!));
                    Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)property.SaveBits!).BitLength, $"[{attrib.Id}]{attrib.Attribute} Value: {value}");
                }
                else
                {
                    value = mainReader.ReadItemBits<UInt16>(new ItemOffsetStruct(-1, (int)property.SaveBits!));
                    Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)property.SaveBits!).BitLength, $"[{attrib.Id}]{attrib.Attribute} Value: {value}");
                }
                if(property.SaveAdd != null && property.SaveAdd != 0)
                {
                    value -= (uint)property.SaveAdd;
                }

                switch (property.Encode)
                {
                    case 3:
                        attrib.CurrentCharges = (int)(value & 255);
                        attrib.MaxCharges = (int)(value >> 8) & 255;
                        break;
                    default:
                        attrib.Value = value;
                        break;
                }
                Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)property.SaveBits!).BitLength, $"[{attrib.Id}]{attrib.Attribute} Value-sA-encode: {value}");
            }

            return attrib;
        }
    }
}
