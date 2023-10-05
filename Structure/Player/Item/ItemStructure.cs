using D2SLib2.BinaryHandler;
using D2SLib2.Model;
using D2SLib2.Structure.Player.Item.MagicalAffixes;
using System.Diagnostics;
using System.Drawing;
using System.Text.Json.Serialization;

namespace D2SLib2.Structure.Player.Item
{
    public enum ItemParent
    {
        NONE = -1,
        STORED = 0,
        EQUIPPED = 1,
        BELT = 2,
        CURSOR = 4,
        ITEM = 6,
    }

    public enum ItemEquipped
    {
        NONE                 = 0,
        HELMET               = 1,
        AMULET               = 2,
        ARMOR_CHEST          = 3,
        RIGHT_WEAPON         = 4,
        LEFT_WEAPON          = 5,
        RIGHT_RING           = 6,
        LEFT_RING            = 7,
        BELT                 = 8,
        BOOTS                = 9,
        GLVOES               = 10,
        RIGHT_SWAP_WEAPON    = 11,
        LEFT_SWAP_WEAPON     = 12
    }

    public enum ItemStored
    {
        NONE            = 0,
        INVENTORY       = 1,
        CUBE            = 4,
        STASH           = 5
    }

    public enum ItemQuality
    {
        NONE            = 0,
        INFERIOR        = 1,
        NORMAL          = 2,
        SUPERIOR        = 3,
        MAGIC           = 4,
        SET             = 5,
        RARE            = 6,
        UNIQUE          = 7,
        CRAFT           = 8
    }

    public enum ItemVersion
    {
        v100,  // v1.00 - v1.03 item
        v104,  // v1.04 - v1.06 item
        v107,  // v1.07 item
        v108,  // v1.08 item
        v109,  // v1.09 item
        v110,  // v1.10 - v1.14d item
        v100R, // v1.0.x - v1.1.x Diablo II: Resurrected item
        v120,  // v1.2.x - v1.3.x Diablo II: Resurrected Patch 2.4 item
        v140,  // v1.4.x+ Diablo II: Resurrected Patch 2.5 item
    }

    public class ItemStructure
    {
        public ItemStructure(BitwiseBinaryReader mainReader)
        {
            ReadBasicItemFlags(mainReader);

            itemVersion = (ItemVersion)mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_ITEM_VERSION);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_ITEM_VERSION.BitLength, $"Item version: {itemVersion}");

            Parent = (ItemParent)mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_PARENT_LOCATION);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_PARENT_LOCATION.BitLength, $"Parent Location: {Parent}");

            Equipped = (ItemEquipped)mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_EQUIPPED_LOCATION);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_EQUIPPED_LOCATION.BitLength, $"Equipped Location: {Equipped}");

            var x = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_INV_X_POSITION);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_INV_X_POSITION.BitLength, $"Position.X: {x}");
            var y = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_INV_Y_POSITION);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_INV_Y_POSITION.BitLength, $"Position.Y: {y}");
            Position = new Point(x, y);

            Stored = (ItemStored)mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_STORED_LOCATION);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_STORED_LOCATION.BitLength, $"Stored Location: {Stored}");

            if (Ear)
            {
                EarFileIndex = mainReader.ReadItemBits<byte>(new ItemOffsetStruct(-1, 3));
                Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, 3).BitLength, $"Ear File Index: {EarFileIndex}");
                EarLevel = mainReader.ReadItemBits<byte>(new ItemOffsetStruct(-1, 7));
                Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, 7).BitLength, $"Ear Level: {EarLevel}");
                PersonalizedName = ReadPersonalizedName(mainReader);
            } else
            {
                Code = ReadItemCode(mainReader);

                itemTemplate = D2S.instance?.dbContext?.ItemsTemplates.SingleOrDefault(x => x.Code == Code.TrimEnd());
                if (itemTemplate == null) throw new InvalidItemException("Unable to retrieve Item Template from data store");

                ItemType = GetItemTypes(itemTemplate);

                if (SimpleItem && ItemType != null && ItemType.Contains("Quest"))
                {
                    ItemStatCost questItemDifficultyISC = D2S.instance?.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Id == 356) ??
                                                                throw new Exception("Unable to find questitemdifficulty (356) in itemstatcost datastore.");

                    SocketedItemCount = (byte)(mainReader.ReadItemBits<byte>(new ItemOffsetStruct(-1, (int)(questItemDifficultyISC.SaveBits ?? 0))) - (questItemDifficultyISC.SaveAdd ?? 0));
                    Logger.WriteSection(mainReader, new ItemOffsetStruct(-1, (int)(questItemDifficultyISC.SaveBits ?? 0)).BitLength, $"Quest Item Simple Socketed Count: {SocketedItemCount}");
                }
                else
                {
                    if (SimpleItem)
                    {
                        SocketedItemCount = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_SIMPLE_SOCKETED_COUNT);
                        Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_SIMPLE_SOCKETED_COUNT.BitLength, $"Simple Socketed Count: {SocketedItemCount}");
                    }
                    else
                    {
                        SocketedItemCount = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_SOCKETED_COUNT);
                        Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_SOCKETED_COUNT.BitLength, $"Socketed Count: {SocketedItemCount}");
                    }
                }
            }

            if (!SimpleItem)
            {
                ReadComplexItem(mainReader);
            }
            mainReader.Align();
            for (int sockets = 0; sockets < SocketedItemCount; sockets++) SocketedItems.Add(new ItemStructure(mainReader));
        }

        private Bit[] BasicItemFlags = new Bit[32];
        public bool Identified { get { return BasicItemFlags[4]; } set { BasicItemFlags[4] = (Bit)(value ? 1 : 0); } }
        public bool Socketed { get { return BasicItemFlags[11]; } set { BasicItemFlags[11] = (Bit)(value ? 1 : 0); } }
        public bool Ear { get { return BasicItemFlags[16]; } set { BasicItemFlags[16] = (Bit)(value ? 1 : 0); } }
        public bool SimpleItem { get { return BasicItemFlags[21]; } set { BasicItemFlags[21] = (Bit)(value ? 1 : 0); } }
        public bool Ethereal { get { return BasicItemFlags[22]; } set { BasicItemFlags[22] = (Bit)(value ? 1 : 0); } }
        public bool Personalized { get { return BasicItemFlags[24]; } set { BasicItemFlags[24] = (Bit)(value ? 1 : 0); } }
        public bool Runeword { get { return BasicItemFlags[26]; } set { BasicItemFlags[26] = (Bit)(value ? 1 : 0); } }
        public bool Stackable { get; set; } = false;

        public UInt16 QuestDifficulty { get; set; } = 0;

        public byte EarFileIndex { get; set; } = 0;
        public byte EarLevel { get; set; } = 0;
        public ItemVersion itemVersion { get; set; } = ItemVersion.v140;

        public ItemParent Parent { get; set; } = ItemParent.NONE;
        public ItemEquipped Equipped { get; set; } = ItemEquipped.NONE;
        public Point Position { get; set; } = new Point(-1, -1);
        public ItemStored Stored { get; set; } = ItemStored.NONE;

        public string Code { get; set; } = string.Empty;
        public UInt32 Id { get; set; } = UInt32.MaxValue;
        public byte ItemLevel { get; set; } = byte.MaxValue;
        public ItemQuality Quality { get; set; } = ItemQuality.NONE;
        public string PersonalizedName { get; set; } = string.Empty;
        public HashSet<string> ItemType { get; set; } = new HashSet<string>();
        public Armor? armor { get; set; } = null;
        public Weapon? weapon { get; set; } = null;
        public UInt16 MaxDurability { get; set; } = 0;
        public UInt16 Durability { get; set; } = 0;
        public UInt16 Quantity { get; set; } = 0;
        public byte NumberOfSockets { get; set; } = 0;

        public bool MultipleGraphics = false;
        public byte GraphicId = 0;

        public HashSet<MagicalAttributes> magicalAttributes { get; set; } = new HashSet<MagicalAttributes>();

        public UInt16[] MagicPrefixId { get; set; } = new UInt16[3];
        public UInt16[] MagicSuffixId { get; set; } = new UInt16[3];
        public UInt16 RarePrefixId { get; set; } = 0;
        public UInt16 RareSuffixId { get; set; } = 0;
        public UInt16 CraftPrefixId { get; set; } = 0;
        public UInt16 CraftSuffixId { get; set; } = 0;
        public byte SetItemMask { get; set; } = 0;

        public UInt32 RunewordId { get; set; } = 0;
        public UInt16 propertyList { get; set; } = 0;

        public bool AutoAffix { get; set; } = false;
        public UInt16 AutoAffixId { get; set; } = 0;

        public UInt16 InferiorFileIndex { get; set; } = 0;
        public UInt16 SuperiorFileIndex { get; set; } = 0;
        public UInt16 SetFileIndex { get; set; } = 0;
        public UInt16 UniqueFileIndex { get; set; } = 0;

        public bool RealmDataExists { get; set; } = false;
        public Bit[]? RealmData { get; set; } = null;

        public byte SocketedItemCount { get; set; } = 0;
        public List<ItemStructure> SocketedItems { get; set; } = new List<ItemStructure>();

        [JsonIgnore]
        private ItemsTemplate? itemTemplate { get; set; } = null;

        private void ReadBasicItemFlags(BitwiseBinaryReader mainReader)
        {
            BasicItemFlags = mainReader.ReadItemBits<Bit[]>(InventoryOffsets.OFFSET_BASIC_ITEM_FLAGS) ?? throw new NullReferenceException();
        }

        private void ReadComplexItem(BitwiseBinaryReader mainReader)
        {
            Id = mainReader.ReadItemBits<UInt32>(InventoryOffsets.OFFSET_ID);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_ID.BitLength, $"Id: {Id}");

            ItemLevel = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_ITEM_LEVEL);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_ITEM_LEVEL.BitLength, $"Item Level: {ItemLevel}");

            Quality = (ItemQuality)mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_QUALITY);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_QUALITY.BitLength, $"Quality: {Quality}");

            MultipleGraphics = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_HAS_MULTIPLE_GRAPHICS);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_HAS_MULTIPLE_GRAPHICS.BitLength, $"Multiple Graphics Bool: {MultipleGraphics}");
            if (MultipleGraphics)
            {
                GraphicId = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_MULTIPLE_GRAPHICS);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_MULTIPLE_GRAPHICS.BitLength, $"Multiple Graphics Id: {GraphicId}");
            }

            AutoAffix = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_HAS_AUTO_AFFIX);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_HAS_AUTO_AFFIX.BitLength, $"Auto Affix Bool: {AutoAffix}");
            if (AutoAffix)
            {
                AutoAffixId = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_AUTO_AFFIX);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_AUTO_AFFIX.BitLength, $"Auto Affix Id: {AutoAffixId}");
            }

            switch(Quality)
            {
                case ItemQuality.INFERIOR:
                    InferiorFileIndex = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_FILE_INDEX_INFERIOR);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_FILE_INDEX_INFERIOR.BitLength, $"Inferior File Index: {InferiorFileIndex}");
                    break;
                case ItemQuality.NORMAL: break;
                case ItemQuality.SUPERIOR:
                    SuperiorFileIndex = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_FILE_INDEX_SUPERIOR);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_FILE_INDEX_SUPERIOR.BitLength, $"Superior File Index: {SuperiorFileIndex}");
                    break;
                case ItemQuality.MAGIC:
                    MagicPrefixId[0] = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_MAGIC_PREFIX_ID);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_MAGIC_PREFIX_ID.BitLength, $"Magic Prefix Id [0]: {MagicPrefixId[0]}");
                    MagicSuffixId[0] = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_MAGIC_SUFFIX_ID);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_MAGIC_SUFFIX_ID.BitLength, $"Magic Suffix Id [0]: {MagicSuffixId[0]}");
                    break;
                case ItemQuality.RARE:
                    RarePrefixId = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_RARE_PREFIX_ID);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RARE_PREFIX_ID.BitLength, $"Rare Prefix Id: {RarePrefixId}");
                    RareSuffixId = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_RARE_SUFFIX_ID);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RARE_SUFFIX_ID.BitLength, $"Rare Suffix Id: {RareSuffixId}");
                    for (int i = 0; i < 3; i++)
                    {
                        bool isPrefixAvailable = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_RARE_PREFIX_VALUE_BOOL);
                        Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RARE_PREFIX_VALUE_BOOL.BitLength, $"Rare Prefix Bool: {isPrefixAvailable}");
                        if (isPrefixAvailable)
                        {
                            MagicPrefixId[i] = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_RARE_PREFIX_VALUE);
                            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RARE_PREFIX_VALUE.BitLength, $"Rare Magic Prefix Value [{i}]: {MagicPrefixId[i]}");
                        }

                        bool isSuffixAvailable = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_RARE_SUFFIX_VALUE_BOOL);
                        Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RARE_SUFFIX_VALUE_BOOL.BitLength, $"Rare Suffix Bool: {isSuffixAvailable}");
                        if (isSuffixAvailable)
                        {
                            MagicSuffixId[i] = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_RARE_SUFFIX_VALUE);
                            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RARE_SUFFIX_VALUE.BitLength, $"Rare Magic Suffix Value [{i}]: {MagicSuffixId[i]}");
                        }
                    }
                    break;
                case ItemQuality.CRAFT:
                    CraftPrefixId = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_CRAFT_PREFIX_ID);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_CRAFT_PREFIX_ID.BitLength, $"Craft Prefix Id: {CraftPrefixId}");
                    CraftSuffixId = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_CRAFT_SUFFIX_ID);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_CRAFT_SUFFIX_ID.BitLength, $"Craft Suffix Id: {CraftSuffixId}");
                    for (int i=0;i<3;i++)
                    {
                        bool isPrefixAvailable = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_CRAFT_PREFIX_VALUE_BOOL);
                        Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_CRAFT_PREFIX_VALUE_BOOL.BitLength, $"Craft Prefix Bool: {isPrefixAvailable}");
                        if (isPrefixAvailable)
                        {
                            MagicPrefixId[i] = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_CRAFT_PREFIX_VALUE);
                            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_CRAFT_PREFIX_VALUE.BitLength, $"Craft Magic Prefix Value [{i}]: {MagicPrefixId[i]}");
                        }

                        bool isSuffixAvailable = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_CRAFT_PREFIX_VALUE_BOOL);
                        Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_CRAFT_PREFIX_VALUE_BOOL.BitLength, $"Craft Suffix Bool: {isSuffixAvailable}");
                        if (isSuffixAvailable)
                        {
                            MagicSuffixId[i] = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_CRAFT_SUFFIX_VALUE);
                            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_CRAFT_SUFFIX_VALUE.BitLength, $"Craft Magic Suffix Value [{i}]: {MagicSuffixId[i]}");
                        }
                    }
                    break;
                case ItemQuality.SET:
                    SetFileIndex = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_SET_FILE_INDEX);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_SET_FILE_INDEX.BitLength, $"Set File Index: {SetFileIndex}");
                    break;
                case ItemQuality.UNIQUE:
                    UniqueFileIndex = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_UNIQUE_FILE_INDEX);
                    Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_UNIQUE_FILE_INDEX.BitLength, $"Unique File Index: {UniqueFileIndex}");
                    break;
            }

            if(Runeword)
            {
                RunewordId = mainReader.ReadItemBits<UInt32>(InventoryOffsets.OFFSET_RUNEWORD_ID);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RUNEWORD_ID.BitLength, $"Runeword Id: {RunewordId}");
                UInt32 tmpRuneId = RunewordId;
                if (tmpRuneId < 75) tmpRuneId -= 26;
                else tmpRuneId -= 25;
                //Runeword = Core.SqlContext.Runes.Single(x => x.Name!.Substring(8) == tmpRuneId.ToString())?.RuneName!;
                if (RunewordId == 2718) RunewordId = 48;
                UInt16 RunewordProperty = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_RUNEWORD_PROPERTY);
                propertyList |= (UInt16)(1 << (RunewordProperty + 1));
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_RUNEWORD_PROPERTY.BitLength, $"Runeword Property: (1 << ({RunewordProperty} + 1) | [{propertyList}])");
            }

            if(Personalized)
            {
                PersonalizedName = ReadPersonalizedName(mainReader);
            }

            if(Code.TrimEnd() == InventoryOffsets.OFFSET_TOWN_PORTAL_BOOK.ItemCode)
            {
                MagicSuffixId[0] = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_TOWN_PORTAL_BOOK);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_TOWN_PORTAL_BOOK.BitLength, $"Town Portal Book Spell Id: {MagicSuffixId[0]}");
            } else if(Code.TrimEnd() == InventoryOffsets.OFFSET_IDENTIFY_BOOK.ItemCode)
            {
                MagicSuffixId[0] = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_IDENTIFY_BOOK);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_IDENTIFY_BOOK.BitLength, $"Identification Book Spell Id: {MagicSuffixId[0]}");
            }

            RealmDataExists = mainReader.ReadItemBits<Bit>(InventoryOffsets.OFFSET_REALM_DATA_BOOL);
            Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_REALM_DATA_BOOL.BitLength, $"Realm Data: {RealmDataExists}");
            if (RealmDataExists)
            {
                RealmData = new Bit[96];
                RealmData = mainReader.ReadItemBits<Bit[]>(InventoryOffsets.OFFSET_REALM_DATA)
                    ?? throw new InvalidItemException("Unable to read items Realm Data, corrupt item/save?");
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_REALM_DATA_BOOL.BitLength, $"Realm Data: {RealmData.ToStringRepresentation()}");
            }

            if(ItemType.Contains("Any Armor"))
            {
                armor = new Armor();
                armor.ArmorValue = (UInt16)(mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_ARMOR_VALUE) - (UInt16)(D2S.instance?.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Stat == "armorclass")?.SaveAdd ?? 0));
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_ARMOR_VALUE.BitLength, $"Armor Value: {armor.ArmorValue}");
            } else if(ItemType.Contains("Weapon"))
            {
                weapon = new Weapon();
            }

            if(armor != null || weapon != null)
            {
                ItemStatCost iscMaxDurability = D2S.instance!.dbContext!.ItemStatCosts.Single(x => x.Stat == "maxdurability");
                ItemStatCost iscDurability = D2S.instance!.dbContext!.ItemStatCosts.Single(x => x.Stat == "durability");
                MaxDurability = (UInt16)(mainReader.ReadItemBits<UInt16>(new ItemOffsetStruct(-1, (int)(iscMaxDurability.SaveBits ?? 0))) - (iscMaxDurability.SaveAdd ?? 0));
                Logger.WriteSection(mainReader, (int)(iscMaxDurability.SaveBits ?? 0), $"Max Durability: {MaxDurability}");
                if (MaxDurability > 0)
                {
                    Durability = (UInt16)(mainReader.ReadItemBits<UInt16>(new ItemOffsetStruct(-1, (int)(iscDurability.SaveBits ?? 0))) - (iscDurability.SaveAdd ?? 0));
                    Logger.WriteSection(mainReader, (int)(iscDurability.SaveBits ?? 0), $"Durability: {Durability}");
                }
            }

            if (Stackable || itemTemplate!.Stackable >= 1)
            {
                Quantity = mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_QUANTITY);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_QUANTITY.BitLength, $"Quantity: {Quantity}");
            }
            if (Socketed)
            {
                NumberOfSockets = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_SOCKET_COUNT);
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_SOCKET_COUNT.BitLength, $"Number of Sockets: {NumberOfSockets}");
            }

            if(Quality == ItemQuality.SET)
            {
                SetItemMask = mainReader.ReadItemBits<byte>(InventoryOffsets.OFFSET_SET_ITEM_MASK);
                propertyList |= SetItemMask;
                Logger.WriteSection(mainReader, InventoryOffsets.OFFSET_SET_ITEM_MASK.BitLength, $"Set Item Mask: {SetItemMask} | {propertyList}");
            }

            // General magical attributes
            magicalAttributes = new HashSet<MagicalAttributes>();
            {
                if (MagicalAttributes.Read(mainReader) is MagicalAttributes attrib)
                {
                    magicalAttributes.Add(attrib);
                }
            }
            
            // This is for sets
            for(int i=1;i<=64;i<<=1)
            {
                if((propertyList & i) != 0)
                {
                    if (MagicalAttributes.Read(mainReader) is MagicalAttributes attrib)
                    {
                        magicalAttributes.Add(attrib);
                    }
                }
            }
        }

        private HashSet<string> GetItemTypes(ItemsTemplate itemTemplate)
        {
            HashSet<string> result = new HashSet<string>();
            string? itemTypeCode = itemTemplate.Type;
            var dbContext = D2S.instance?.dbContext;

            if (dbContext == null || itemTypeCode == null) return result;

            var allItemTypes = dbContext.ItemTypes.ToDictionary(x => x.Code, x => x);

            Action<string, string> addEquivTypes = (startCode, equivField) =>
            {
                ItemType? itemType = allItemTypes.ContainsKey(startCode) ? allItemTypes[startCode] : null;
                while (itemType != null)
                {
                    result.Add(itemType.ItemType1!);
                    string? nextCode = equivField == "Equiv1" ? itemType.Equiv1 : itemType.Equiv2;
                    if (nextCode == null) itemType = null;
                    else itemType = allItemTypes.ContainsKey(nextCode) ? allItemTypes[nextCode] : null;
                }
            };

            // Add Equiv1 and Equiv2 types
            addEquivTypes(itemTypeCode, "Equiv1");
            addEquivTypes(itemTypeCode, "Equiv2");

            return result;
        }

        private string ReadItemCode(BitwiseBinaryReader mainReader)
        {
            string result = string.Empty;
            BitwiseBinaryReader codeReader = new BitwiseBinaryReader(mainReader.PeekItemBits<Bit[]>(InventoryOffsets.OFFSET_CODE)
                                                ?? throw new NullReferenceException("Unable to read item code from stream, corrupt save?"));
            for (int i = 0; i < 4; i++)
            {
                result += D2S.instance!.itemCodeTree.DecodeChar(codeReader);
            }

            if (!IsValidItem(result))
            {
                Logger.Close();
                throw new InvalidItemException($"The code: {result} was not found in the item store.");
            }

            mainReader.SetBitPosition(mainReader.bitPosition + codeReader.bitPosition);
            Logger.WriteSection(mainReader, 0, $"Item Code: {result}");

            return result;
        }

        private string ReadPersonalizedName(BitwiseBinaryReader mainReader)
        {
            string result = string.Empty;
            BitwiseBinaryReader personalizedReader = new BitwiseBinaryReader(mainReader.PeekItemBits<Bit[]>(InventoryOffsets.OFFSET_PERSONALIZED_NAME_SECTION)
                ?? throw new InvalidItemException("Unable to read bits for personalized name."));
            for (int i = 0; i < 16; i++)
            {
                if (D2S.instance!.fileHeader.Version > D2SLib2.Structure.Header.FileVersion.D2R_V10x_To_11x)
                {
                    if (personalizedReader.PeekItemBits<char>(InventoryOffsets.OFFSET_PERSONALIZED_CHAR) == '\0')
                        break;
                    result += personalizedReader.ReadItemBits<char>(InventoryOffsets.OFFSET_PERSONALIZED_CHAR);
                } else
                {
                    if (personalizedReader.PeekItemBits<char>(new ItemOffsetStruct(InventoryOffsets.OFFSET_PERSONALIZED_CHAR.BitOffset, InventoryOffsets.OFFSET_PERSONALIZED_CHAR.BitLength - 1)) == '\0')
                        break;
                    result += personalizedReader.ReadItemBits<char>(new ItemOffsetStruct(InventoryOffsets.OFFSET_PERSONALIZED_CHAR.BitOffset, InventoryOffsets.OFFSET_PERSONALIZED_CHAR.BitLength - 1));
                }
            }

            mainReader.SetBitPosition(mainReader.bitPosition + personalizedReader.bitPosition + 8);
            Logger.WriteSection(mainReader, personalizedReader.bitPosition, $"Personalized Name: {result}");
            return result;
        }

        private bool IsValidItem(string code)
        {
            return (D2S.instance!.dbContext!.ItemsTemplates.FirstOrDefault(x => x.Code == code.Trim()) != null);
        }
    }
}
