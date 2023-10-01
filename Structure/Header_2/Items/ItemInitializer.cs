using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Items
{
    public partial class ItemStructure
    {
        public ItemStructure(BitwiseBinaryReader reader)
        {
            // 53 Bits read here
            basicItemStructure = new BasicItemStructure(reader);

            reader.ReadBits(3);
            Parent = reader.ReadBits(3).ToByte();
            Equipped = reader.ReadBits(4).ToByte();
            int x = reader.ReadBits(4).ToByte();
            int y = reader.ReadBits(4).ToByte();
            Location = new Point(x, y);
            Store = reader.ReadBits(3).ToByte();

            if (Ear)
            {
            } else
            {
                Code = string.Empty;
                for (int j = 0; j < 4; j++)
                    Code += D2S.instance!.itemCodeTree.DecodeChar(reader);
                SocketedItemCount = reader.ReadBits(SimpleItem ? 1 : 3).ToByte();
            }

            if (!SimpleItem)
            {
                ReadComplexItem(reader);
            }
            reader.Align();
            for (int sockets = 0; sockets < SocketedItemCount; sockets++) SocketedItems.Add(new ItemStructure(reader));
        }

        private void ReadComplexItem(BitwiseBinaryReader reader)
        {
            Id = reader.ReadBits(32).ToUInt32();
            iLevel = reader.ReadBits(7).ToByte();
            Quality = reader.ReadBits(4).ToByte();

            MultipleGraphics = reader.ReadBit();
            if (MultipleGraphics) GraphicId = reader.ReadBits(3).ToByte();

            AutoAffix = reader.ReadBit();
            if (AutoAffix) AutoAffixId = reader.ReadBits(11).ToUInt16();

            switch(Quality)
            {
                case 0x01:      // Inferior
                case 0x03:      // Superior
                    reader.ReadBits(3).ToUInt16();
                    break;
                case 0x04:      // Magic
                    MagicPrefixIds = new ushort[3];
                    MagicSuffixIds = new ushort[3];
                    MagicPrefixIds[0] = reader.ReadBits(11).ToUInt16();
                    MagicSuffixIds[0] = reader.ReadBits(11).ToUInt16();
                    break;
                case 0x06:      // Rare
                case 0x08:      // Craft
                    RarePrefixId = reader.ReadBits(8).ToUInt16();
                    RareSuffixId = reader.ReadBits(8).ToUInt16();
                    for (int i = 0; i < 3; i++)
                    {
                        if (reader.ReadBit())
                        {
                            MagicPrefixIds = new ushort[3];
                            MagicPrefixIds[i] = reader.ReadBits(11).ToUInt16();
                        }
                        if(reader.ReadBit())
                        {
                            MagicSuffixIds = new ushort[3];
                            MagicSuffixIds[i] = reader.ReadBits(11).ToUInt16();
                        }
                    }
                    break;
                case 0x05:      // Set
                case 0x07:      // Unique
                    reader.ReadBits(12).ToUInt16();
                    break;
                default:
                    break;
            }

            ushort propertyLists = 0;
            if(Runeword)
            {
                RunewordId = reader.ReadBits(12).ToUInt32();
                UInt32 tmpRuneId = RunewordId??0;
                if (tmpRuneId < 75) tmpRuneId -= 26;
                else tmpRuneId -= 25;
                // we will want to get runeword name

                if (RunewordId == 2718) RunewordId = 48;
                propertyLists |= (ushort)(1 << (reader.ReadBits(4).ToUInt16() + 1));
            }

            if (Personalized)
            {
                for (int i = 0; i < 15; i++)
                {
                    if ((char)reader.PeekBits(7).ToByte() == '\0')
                        break;
                    PersonalizedName += (char)reader.ReadBits(7).ToByte();
                }
            }

            if(Code!.TrimEnd() == "tbk" || Code!.TrimEnd() == "ibk")
            {
                if(MagicSuffixIds == null) MagicSuffixIds = new ushort[3];
                MagicSuffixIds[0] = reader.ReadBits(5).ToByte();
            }

            RealmData = reader.ReadBit();
            if (RealmData == true) reader.ReadBits(96);

            ItemsTemplate? itemTemplate = D2S.instance?.dbContext?.ItemsTemplates.SingleOrDefault(x => x.Code == Code.Trim());
            if (itemTemplate == null) return;
            Stackable = Convert.ToBoolean(itemTemplate.Stackable);
            ItemType = D2S.instance!.dbContext!.GetItemTypes(itemTemplate);
            if(ItemType.Contains("Any Armor"))
            {
                ArmorClass = new Armor();
                ArmorClass.armor = (ushort)(reader.ReadBits(11).ToUInt16() + Convert.ToUInt16(D2S.instance?.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Stat == "armorclass")?.SaveAdd ?? 0));
            } else if(ItemType.Contains("Weapon"))
            {
                WeaponClass = new Weapon();
            }
            if(ArmorClass != null || WeaponClass != null)
            {
                ItemStatCost iscMaxDurability = D2S.instance!.dbContext!.ItemStatCosts.Single(x => x.Stat == "maxdurability");
                MaxDurability = (ushort)(reader.ReadBits(Convert.ToInt32(iscMaxDurability.SaveBits)).ToUInt32() + Convert.ToInt32(iscMaxDurability.SaveAdd));
                if(MaxDurability > 0)
                {
                    Durability = (ushort)(reader.ReadBits(Convert.ToInt32(iscMaxDurability.SaveBits)).ToUInt16() + Convert.ToInt32(iscMaxDurability.SaveAdd));
                    reader.ReadBit(); // junk bit it appears??
                }
            }

            if (Stackable) Quantity = reader.ReadBits(9).ToUInt16();
            if (Socketed) NumberOfSockets = reader.ReadBits(4).ToByte();
            if(Quality == 0x05) // Set quality
            {
                SetItemMask = reader.ReadBits(5).ToByte();
                propertyLists |= SetItemMask;
            }

            magicalAttributes = new List<MagicalAttribute>
            {
                new MagicalAttribute(reader)
            };
            for(int i=1;i<=64;i<<=1)
            {
                if((propertyLists & i) != 0)
                {
                    magicalAttributes.Add(new MagicalAttribute(reader));
                }
            }
            // Remove the last as it should be an empty attribute
            if(magicalAttributes.Count > 0) magicalAttributes.Remove(magicalAttributes.Last());
        }
    }
}
