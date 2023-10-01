using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Items
{
    public partial class ItemStructure
    {
        public bool Identified {    get { return (bool)basicItemStructure?.flags[4]!; } set { basicItemStructure!.flags[4] = (bool)value!; } }
        public bool Socketed {      get { return (bool)basicItemStructure?.flags[11]!; } set { basicItemStructure!.flags[11] = (bool)value!; } }
        public bool Ear {           get { return (bool)basicItemStructure?.flags[16]!; } set { basicItemStructure!.flags[16] = (bool)value!; } }
        public bool SimpleItem {    get { return (bool)basicItemStructure?.flags[21]!; } set { basicItemStructure!.flags[21] = (bool)value!; } }
        public bool Ethereal {      get { return (bool)basicItemStructure?.flags[22]!; } set { basicItemStructure!.flags[22] = (bool)value!; } }
        public bool Personalized {  get { return (bool)basicItemStructure?.flags[24]!; } set { basicItemStructure!.flags[24] = (bool)value!; } }
        public bool Runeword {      get { return (bool)basicItemStructure?.flags[26]!; } set { basicItemStructure!.flags[26] = (bool)value!; } }

        public string? Code { get; set; } = null;
        public UInt32? Id { get; set; } = null;
        public byte? iLevel { get; set; } = null;
        public byte? Quality { get; set; } = null;
        public byte SetItemMask { get; set; } = 0;

        public bool Stackable { get; set; } = false;
        public int Quantity { get; set; } = 1;
        public int NumberOfSockets { get; set; } = 0;
        
        public bool MultipleGraphics { get; set; } = false;
        public byte? GraphicId { get; set; } = null;

        public string? PersonalizedName { get; set; } = null;

        public BasicItemStructure? basicItemStructure { get; set; } = null;
        public byte? Parent { get; set; } = null;
        public byte? Equipped { get; set; } = null;
        public Point? Location { get; set; } = null;
        public byte? Store { get; set; } = null;
        public byte? SocketedItemCount { get; set; } = null;
        public List<ItemStructure> SocketedItems { get; set; } = new List<ItemStructure>();

        public List<MagicalAttribute> magicalAttributes { get; set; } = new List<MagicalAttribute>();

        public bool AutoAffix { get; set; } = false;
        public UInt16? AutoAffixId { get; set; } = null;
        public UInt16[]? MagicPrefixIds { get; set; } = null;
        public UInt16[]? MagicSuffixIds { get; set; } = null;
        public UInt32? RarePrefixId { get; set; } = null;
        public UInt32? RareSuffixId { get; set; } = null;
        public UInt32? RunewordId { get; set; } = null;
        public List<string>? ItemType { get; set; } = null;

        public UInt16 Durability { get; set; } = 0;
        public UInt16 MaxDurability { get; set; } = 0;

        public Armor? ArmorClass { get; set; } = null;
        public Weapon? WeaponClass { get; set; } = null;

        public bool? RealmData { get; set; } = null;
    }
}
