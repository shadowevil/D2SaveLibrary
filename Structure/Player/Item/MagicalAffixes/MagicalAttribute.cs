using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace D2SLib2.Structure.Player.Item.MagicalAffixes
{
    public class MagicalAttributes
    {
        public HashSet<MagicalAttribute> MagicalList = new HashSet<MagicalAttribute>();

        public MagicalAttributes() { }

        public static string[] filters = new string[]
        {
            "res-all",
            "res-all-max",
            "dmg-fire",
            "dmg-ltng",
            "dmg-mag",
            "dmg-cold",
            "dmg-pois",
            "dmg-throw",
            "dmg-norm",
            "dmg-elem",
            "dmg-elem-min",
            "dmg-elem-max",
            "all-stats",
            "swing2",
            "swing3"
        };

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

                ItemStatCost statcost = D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == Id) ??
                    throw new Exception($"Stat Id {Id} not found in ItemStatCost");

                IQueryable<Property>? properties = D2S.instance?.dbContext?.Properties?.Where(x => x.Stat1 == statcost.Stat
                                                                                                && !filters.Contains(x.Code));
                
                if (properties == null)
                    throw new Exception($"Property not found for {Id} in the Properties datastore");

                List<ItemStatCost> ItemStatCostSet = new List<ItemStatCost>()
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
                        if(stats != null) if(stats.Count() > 0) ItemStatCostSet.AddRange(stats!);
                    }
                }

                /*
                0	    strength
                21	    mindamage
                39	    fireresist
                40	    maxfireresist
                48	    firemindam
                49	    firemaxdam
                50	    lightmindam
                52	    magicmindam
                54	    coldmindam
                57	    poisonmindam
                159	    item_throw_mindamage
                */

                switch (Id)
                {
                    case 17:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == 18)!);
                        break;
                    case 159:
                    case 21:
                        ItemStatCostSet.RemoveAt(ItemStatCostSet.Count - 1);
                        break;
                    case 54:
                    case 57:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == Id + 1)!);
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == Id + 2)!);
                        break;
                    case 48:
                    case 50:
                    case 52:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == Id + 1)!);
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

        public bool Write(BitwiseBinaryWriter writer)
        {
            for(int i=0;i<MagicalList.Count;)
            {
                ItemStatCost? isc = D2S.instance!.dbContext!.ItemStatCosts.SingleOrDefault(x => x.Id == MagicalList.ElementAt(i).Id);
                writer.WriteBits(MagicalList.ElementAt(i).Id.ToBits((uint)InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID.BitLength));

                IQueryable<Property>? properties = D2S.instance?.dbContext?.Properties?.Where(x => x.Stat1 == isc!.Stat && !filters.Contains(x.Code));
                if (properties == null)
                    throw new Exception($"Property not found for {MagicalList.ElementAt(i).Id} in the Properties datastore");

                List<ItemStatCost> ItemStatCostSet = new List<ItemStatCost>()
                {
                    isc
                };

                switch (MagicalList.ElementAt(i).Id)
                {
                    case 17:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == 18)!);
                        break;
                    case 159:
                    case 21:
                        ItemStatCostSet.RemoveAt(ItemStatCostSet.Count - 1);
                        break;
                    case 54:
                    case 57:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == MagicalList.ElementAt(i).Id + 1)!);
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == MagicalList.ElementAt(i).Id + 2)!);
                        break;
                    case 48:
                    case 50:
                    case 52:
                        ItemStatCostSet.Add(D2S.instance!.dbContext!.ItemStatCosts!.SingleOrDefault(x => x.Id == MagicalList.ElementAt(i).Id + 1)!);
                        break;
                }

                foreach (var item in ItemStatCostSet)
                {
                    MagicalList.ElementAt(i).Write(writer, item);
                    i++;
                }
            }
            writer.WriteBits(0x1ff.ToBits((uint)InventoryOffsets.OFFSET_MAGICAL_ATTRIABUTE_ID.BitLength));
            return true;
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
                        attrib.SkillTab = param & 0x7;
                        attrib.SkillLevel = (param >> 3) & 0x1fff;
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
                value = mainReader.ReadItemBits<uint>(new ItemOffsetStruct(-1, (int)property.SaveBits!));
                Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)property.SaveBits!).BitLength, $"[{attrib.Id}]{attrib.Attribute} Value: {value}");
                if(property.SaveAdd != null && property.SaveAdd != 0)
                {
                    value -= (uint)property.SaveAdd;
                }

                if (property.Encode != null)
                {
                    switch (property.Encode)
                    {
                        case 2:
                        case 3:
                            attrib.CurrentCharges = (int)(value & 255);
                            attrib.MaxCharges = (int)(value >> 8) & 255;
                            break;
                    }
                }
                attrib.Value = value;
                Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)property.SaveBits!).BitLength, $"[{attrib.Id}]{attrib.Attribute} Value-sA-encode: {value}");
            }

            return attrib;
        }

        public bool Write(BitwiseBinaryWriter writer, ItemStatCost? property)
        {
            if (property!.SaveParamBits != null)
            {
                if (Param != null)
                {
                    writer.WriteBits(((int)Param).ToBits((uint)(property.SaveParamBits ?? 0)));
                }
                else
                {
                    int ParamBits = 0;
                    if (property.Encode != null)
                    {
                        switch (property.Descfunc)
                        {
                            case 14: // +skill to skilltab
                                ParamBits |= (SkillTab ?? 0) & 0x7;
                                ParamBits |= ((SkillLevel ?? 0) & 0x1fff) << 3;
                                break;
                        }

                        switch (property.Encode)
                        {
                            case 1:
                            case 2: // Chance to cast
                            case 3: // Charges
                                ParamBits |= ((SkillLevel ?? 0) & 63);
                                ParamBits |= ((SkillId ?? 0) & 1023) << 6;
                                break;
                            default:
                                break;
                        }
                        if (Param != null) ParamBits = (int)Param;
                        writer.WriteBits(ParamBits.ToBits((uint)(property.SaveParamBits ?? 0)));
                    }
                }
            }
            if (property.SaveBits != null)
            {
                uint ParamValue = (UInt32)Value + (UInt32)(property.SaveAdd ?? 0);

                if (property.Encode != null)
                {
                    switch (property.Encode)
                    {
                        case 2:
                        case 3:
                            ParamValue |= (uint)(((MaxCharges ?? 0) & 0xff) << 8);
                            break;
                    }
                }
                writer.WriteBits(((uint)ParamValue).ToBits((uint)(property.SaveBits ?? 0)));
            }
            return true;
        }
    }
}
