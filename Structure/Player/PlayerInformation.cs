using D2SLib2.BinaryHandler;
using Microsoft.Toolkit.HighPerformance;
using D2SLib2.Structure.Player.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Formats.Asn1;

namespace D2SLib2.Structure.Player
{
    public class PlayerInformation
    {
        public UInt32 ActiveWeapon = UInt32.MaxValue;
        public StatusClass? statusClass = null;

        public ProgressionClass? Progression = null;

        public PlayerClass playerClass = PlayerClass.AMAZON;
        public byte playerLevel = byte.MaxValue;
        public DateTime CreatedTimeStamp = DateTime.MinValue;

        public UInt32[] AssignedSkills = new UInt32[16];
        public UInt32 LeftMouseSkill = UInt32.MaxValue;
        public UInt32 RightMouseSkill = UInt32.MaxValue;
        public UInt32 SwitchLeftMouseSkill = UInt32.MaxValue;
        public UInt32 SwitchRightMouseSkill = UInt32.MaxValue;

        public DifficultyClass? Normal { get; set; } = null;
        public DifficultyClass? Nightmare { get; set; } = null;
        public DifficultyClass? Hell { get; set; } = null;

        public UInt32 MapSeed = UInt32.MaxValue;

        public string PlayerName = string.Empty;

        public Attributes? attributes = null;
        public SkillsClass? skillsClass = null;
        public Inventory? inventory = null;

        public int IsPlayerDeadCount = 0;
        public List<Point>? CorpseLocation { get; set; } = null;
        public Inventory? CorpseInventory = null;

        public Inventory? MercenaryInventory = null;

        public bool HasIronGolem = false;
        public ItemStructure? IronGolemItem { get; set; } = null;

        public int BlockFactor => (int)(D2S.instance!.dbContext!.Charstats.SingleOrDefault(x => x.Class.ToLower() == playerClass.ToString().ToLower())?.BlockFactor ?? 0);

        public PlayerInformation() { }

        public static PlayerInformation Read(BitwiseBinaryReader mainReader)
        {
            PlayerInformation playerInfo = new PlayerInformation();

            playerInfo.ActiveWeapon = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON.BitLength, $"Active Weapon: {playerInfo.ActiveWeapon}");

            playerInfo.statusClass = new StatusClass(mainReader.Read<Bit[]>(PlayerInformationOffsets.OFFSET_CHARACTER_STATUS));
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_CHARACTER_STATUS.BitLength, $"Status Class: {playerInfo.statusClass.Flags!.ToStringRepresentation()}");

            playerInfo.Progression = new ProgressionClass(mainReader.Read<Bit[]>(PlayerInformationOffsets.OFFSET_PROGRESSION) ?? throw new Exception("Something happened"));
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_PROGRESSION.BitLength, $"Progression Flags: {playerInfo.Progression.progressionFlags!.ToStringRepresentation()}");

            playerInfo.playerClass = (PlayerClass)mainReader.Read<byte>(PlayerInformationOffsets.OFFSET_PLAYERCLASS);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_PLAYERCLASS.BitLength, $"Player Class: {playerInfo.playerClass}");

            playerInfo.playerLevel = mainReader.Read<byte>(PlayerInformationOffsets.OFFSET_PLAYERLEVEL);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_PLAYERLEVEL.BitLength, $"Player Level: {playerInfo.playerLevel}");

            var timestamp = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_UNIX_TIMESTAMP);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            playerInfo.CreatedTimeStamp = epoch.AddSeconds(timestamp).ToLocalTime();
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_UNIX_TIMESTAMP.BitLength, $"Last played timestamp: {playerInfo.CreatedTimeStamp}");

            mainReader.SetBytePosition(PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.Offset);
            for(int i=0;i<16;i++)
            {
                playerInfo.AssignedSkills[i] = mainReader.ReadBits(PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.BitLength / 16)
                                                                    .ToUInt32((uint)PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.BitLength / 16, Endianness.BigEndian);
                if (playerInfo.AssignedSkills[i] == UInt16.MaxValue) playerInfo.AssignedSkills[i] = 0x00;
                Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.BitLength / 16, $"Assigned Skill: {playerInfo.AssignedSkills[i]}");
            }

            playerInfo.LeftMouseSkill = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_LEFTMOUSESKILL);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_LEFTMOUSESKILL.BitLength, $"Left Mouse Skill: {playerInfo.LeftMouseSkill}");

            playerInfo.RightMouseSkill = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_RIGHTMOUSESKILL);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_RIGHTMOUSESKILL.BitLength, $"Right Mouse Skill: {playerInfo.RightMouseSkill}");

            playerInfo.SwitchLeftMouseSkill = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_SWITCHLEFTMOUSESKILL);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_SWITCHLEFTMOUSESKILL.BitLength, $"Switch Left Mouse Skill: {playerInfo.SwitchLeftMouseSkill}");

            playerInfo.SwitchRightMouseSkill = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_SWITCHRIGHTMOUSESKILL);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_SWITCHRIGHTMOUSESKILL.BitLength, $"Switch Right Mouse Skill: {playerInfo.SwitchRightMouseSkill}");


            mainReader.SetBytePosition(PlayerInformationOffsets.OFFSET_DIFFICULTY.Offset);
            playerInfo.Normal = DifficultyClass.Read(mainReader);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_DIFFICULTY.BitLength / 3, $"Normal Active Difficulty: {playerInfo.Normal.flags!.ToStringRepresentation()}");

            playerInfo.Nightmare = DifficultyClass.Read(mainReader);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_DIFFICULTY.BitLength / 3, $"Nightmare Active Difficulty: {playerInfo.Nightmare.flags!.ToStringRepresentation()}");

            playerInfo.Hell = DifficultyClass.Read(mainReader);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_DIFFICULTY.BitLength / 3, $"Hell Active Difficulty: {playerInfo.Hell.flags!.ToStringRepresentation()}");

            playerInfo.MapSeed = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_MAPID);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_MAPID.BitLength / 3, $"Map Seed: {playerInfo.MapSeed}");

            playerInfo.PlayerName = mainReader.Read<string>(PlayerInformationOffsets.OFFSET_PLAYER_NAME)!.TrimEnd('\0');
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_PLAYER_NAME.BitLength / 3, $"Player Name: {playerInfo.PlayerName}");

            playerInfo.attributes = Attributes.Read(mainReader);
            if (playerInfo.attributes.attributeList.SingleOrDefault(x => x.Id == 12)?.Value != playerInfo.playerLevel)
                throw new Exception($"Attribute level and file level missmatch, corrupt save? ({playerInfo.attributes.attributeList.SingleOrDefault(x => x.Id == 12)?.Value} {playerInfo.playerLevel})");

            playerInfo.skillsClass = SkillsClass.Read(mainReader, playerInfo.playerClass);

            return playerInfo;
        }

        public void ReadIronGolemInventory(BitwiseBinaryReader mainReader)
        {
            if (Inventory.EndOfMercenaryOffset == -1) throw new Exception("End of mercenary offset not set");

            mainReader.SetBitPosition(Inventory.EndOfMercenaryOffset);
            if (mainReader.ReadSkipPositioning<string>(PlayerInformationOffsets.OFFSET_IRON_GOLEM_MARKER) != PlayerInformationOffsets.OFFSET_IRON_GOLEM_MARKER.Signature)
                throw new Exception("Iron Golem Inventory marker not found");
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_IRON_GOLEM_MARKER.BitLength, $"Iron Golem Marker: {PlayerInformationOffsets.OFFSET_IRON_GOLEM_MARKER.Signature}");

            if ((HasIronGolem = Convert.ToBoolean(mainReader.ReadSkipPositioning<byte>(PlayerInformationOffsets.OFFSET_IRON_GOLEM_BOOL))))
            {
                IronGolemItem = new ItemStructure(mainReader, 1);
            }
        }

        public void ReadMercenaryInventory(BitwiseBinaryReader mainReader)
        {
            if (Inventory.EndCorpseOffset == -1) throw new Exception("End of corpse information not set");

            mainReader.SetBitPosition(Inventory.EndCorpseOffset);
            if (mainReader.ReadSkipPositioning<string>(PlayerInformationOffsets.OFFSET_MERCENARY_INVENTORY_MARKER) != PlayerInformationOffsets.OFFSET_MERCENARY_INVENTORY_MARKER.Signature)
                throw new Exception("Mercenary Inventory marker not found");
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_MERCENARY_INVENTORY_MARKER.BitLength, $"Mercenary Inventory Marker: {PlayerInformationOffsets.OFFSET_MERCENARY_INVENTORY_MARKER.Signature}");

            if (D2S.instance?.mercenary!.Seed > 0)
            {
                MercenaryInventory = Inventory.Read(mainReader);
            }
            Inventory.EndOfMercenaryOffset = mainReader.bitPosition;
        }

        public void ReadCorpseInformation(BitwiseBinaryReader mainReader)
        {
            if (Inventory.EndInventoryOffset == -1) throw new Exception("End of inventory not found, unable to read Corpse Information");

            mainReader.SetBitPosition(Inventory.EndInventoryOffset);
            if (mainReader.ReadSkipPositioning<string>(PlayerInformationOffsets.OFFSET_CORPSE_INFORMATION_MARKER) != PlayerInformationOffsets.OFFSET_CORPSE_INFORMATION_MARKER.Signature)
                throw new Exception("Corpse marker not found");
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_CORPSE_INFORMATION_MARKER.BitLength, $"Corpse Marker: {PlayerInformationOffsets.OFFSET_CORPSE_INFORMATION_MARKER.Signature}");

            IsPlayerDeadCount = mainReader.ReadSkipPositioning<UInt16>(PlayerInformationOffsets.OFFSET_CORPSE_COUNT);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_CORPSE_COUNT.BitLength, $"Corpse Count: {IsPlayerDeadCount}");
            if (IsPlayerDeadCount == 0) Inventory.EndCorpseOffset = mainReader.bitPosition;

            CorpseLocation = new List<Point>();
            for (int i = 0; i < IsPlayerDeadCount; i++)
            {
                mainReader.SkipBits(32); // Unknown
                CorpseLocation.Add(
                    new Point(
                            (int)mainReader.ReadSkipPositioning<UInt32>(PlayerInformationOffsets.OFFSET_CORPSE_XY_LOCATION),
                            (int)mainReader.ReadSkipPositioning<UInt32>(PlayerInformationOffsets.OFFSET_CORPSE_XY_LOCATION)
                        )
                    );
                Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_CORPSE_XY_LOCATION.BitLength * 2, $"Corpse Location [{CorpseLocation.Count - 1}]: {CorpseLocation.Last().X} {CorpseLocation.Last().Y}");
                CorpseInventory = Inventory.Read(mainReader);
            }
            Inventory.EndCorpseOffset = mainReader.bitPosition;
        }

        public bool WriteCorpseInformation(BitwiseBinaryWriter writer)
        {
            writer.WriteBits(PlayerInformationOffsets.OFFSET_CORPSE_INFORMATION_MARKER.Signature.ToBits());
            writer.WriteBits(IsPlayerDeadCount.ToBits((uint)PlayerInformationOffsets.OFFSET_CORPSE_COUNT.BitLength));
            for (int i = 0; i < IsPlayerDeadCount; i++)
            {
                writer.WriteVoidBits(32 * 8);
                if (CorpseLocation != null)
                {
                    foreach (Point corpseLocation in CorpseLocation)
                    {
                        writer.WriteBits(corpseLocation.X.ToBits((uint)PlayerInformationOffsets.OFFSET_CORPSE_XY_LOCATION.BitLength));
                        writer.WriteBits(corpseLocation.Y.ToBits((uint)PlayerInformationOffsets.OFFSET_CORPSE_XY_LOCATION.BitLength));
                    }
                }
                if (CorpseInventory != null) CorpseInventory.WriteInventory(writer);
            }
            return true;
        }

        public bool WriteActiveWeapon(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON.Offset)
                return false;

           writer.WriteBits(ActiveWeapon.ToBits((uint)PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON.BitLength));
            return true;
        }

        public bool WriteCharacterStatus(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_CHARACTER_STATUS.Offset)
            {
                if (writer.GetBytes().Length + 16 != PlayerInformationOffsets.OFFSET_CHARACTER_STATUS.Offset)
                    return false;

               writer.WriteVoidBits(16 * 8);
            }

            writer.WriteBits(statusClass!.GetFlags());
            return true;
        }

        public bool WriteProgression(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_PROGRESSION.Offset)
                return false;

            writer.WriteBits(Progression!.progressionFlags!);
            return true;
        }

        public bool WriteClass(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_CHARACTER_STATUS.Offset)
            {
                if (writer.GetBytes().Length + 2 != PlayerInformationOffsets.OFFSET_PLAYERCLASS.Offset)
                    return false;

               writer.WriteVoidBits(2 * 8);
            }

           writer.WriteBits(((byte)playerClass).ToBits((uint)PlayerInformationOffsets.OFFSET_PLAYERCLASS.BitLength));
            return true;
        }

        public bool WritePlayerLevel(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_PLAYERLEVEL.Offset)
            {
                if (writer.GetBytes().Length + 2 != PlayerInformationOffsets.OFFSET_PLAYERLEVEL.Offset)
                    return false;

               writer.WriteBits(((byte)0x10).ToBits(8, Endianness.LittleEndian));
               writer.WriteBits(((byte)0x1E).ToBits(8, Endianness.LittleEndian));
            }

           writer.WriteBits(((byte)playerLevel).ToBits((uint)PlayerInformationOffsets.OFFSET_PLAYERLEVEL.BitLength));
            return true;
        }

        public bool WriteLastPlayed(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_UNIX_TIMESTAMP.Offset)
            {
                if (writer.GetBytes().Length + 4 != PlayerInformationOffsets.OFFSET_UNIX_TIMESTAMP.Offset)
                    return false;

               writer.WriteVoidBits(4 * 8);
            }
            TimeSpan timeSpan = CreatedTimeStamp.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            UInt32 unixTimestamp = (UInt32)timeSpan.TotalSeconds;
            writer.WriteBits(((UInt32)unixTimestamp).ToBits((uint)PlayerInformationOffsets.OFFSET_UNIX_TIMESTAMP.BitLength, Endianness.LittleEndian));
            return true;
        }

        public bool WriteAssignedSkills(BitwiseBinaryWriter writer)
        {
            if(writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.Offset)
            {
                if (writer.GetBytes().Length + 4 != PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.Offset)
                    return false;

                writer.WriteBits(UInt32.MaxValue.ToBits(32));
            }

            for (int i = 0; i < 16; i++)
            {
                UInt32 skillId = AssignedSkills[i];
                if (skillId == 0)
                {
                    writer.WriteBits(65535.ToBits(32, Endianness.LittleEndian));
                }
                else
                {
                    writer.WriteBits(skillId.ToBits((uint)PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.BitLength / 16, Endianness.LittleEndian));
                }
            }

           writer.WriteBits(LeftMouseSkill.ToBits((uint)PlayerInformationOffsets.OFFSET_LEFTMOUSESKILL.BitLength, Endianness.LittleEndian));
           writer.WriteBits(RightMouseSkill.ToBits((uint)PlayerInformationOffsets.OFFSET_RIGHTMOUSESKILL.BitLength, Endianness.LittleEndian));
           writer.WriteBits(SwitchLeftMouseSkill.ToBits((uint)PlayerInformationOffsets.OFFSET_SWITCHLEFTMOUSESKILL.BitLength, Endianness.LittleEndian));
           writer.WriteBits(SwitchRightMouseSkill.ToBits((uint)PlayerInformationOffsets.OFFSET_SWITCHRIGHTMOUSESKILL.BitLength, Endianness.LittleEndian));

            return true;
        }

        public bool WriteMapSeed(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_MAPID.Offset)
                return false;

           writer.WriteBits(MapSeed.ToBits((uint)PlayerInformationOffsets.OFFSET_MAPID.BitLength));
            return true;
        }

        public bool WritePlayerName(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_PLAYER_NAME.Offset)
                return false;

            for(int i=0;i<16;i++)
            {
                if (PlayerName.Length > i) writer.WriteBits(((byte)PlayerName[i]).ToBits((uint)PlayerInformationOffsets.OFFSET_PLAYER_NAME.BitLength / 16));
                else writer.WriteBits('\0'.ToBits((uint)PlayerInformationOffsets.OFFSET_PLAYER_NAME.BitLength / 16));
            }

            return true;
        }

        public bool WriteMercenaryInventoryInformation(BitwiseBinaryWriter writer)
        {
            writer.WriteBits(PlayerInformationOffsets.OFFSET_MERCENARY_INVENTORY_MARKER.Signature.ToBits());
            if (D2S.instance?.mercenary!.Seed > 0 && MercenaryInventory != null)
            {
                MercenaryInventory.WriteInventory(writer);
            }
            return true;
        }

        public bool WriteIronGolemInformation(BitwiseBinaryWriter writer)
        {
            writer.WriteBits(PlayerInformationOffsets.OFFSET_IRON_GOLEM_MARKER.Signature.ToBits());
            writer.WriteBits(((byte)Convert.ToUInt16(HasIronGolem)).ToBits((uint)PlayerInformationOffsets.OFFSET_IRON_GOLEM_BOOL.BitLength));
            if(HasIronGolem && IronGolemItem != null)
            {
                IronGolemItem.Write(writer);
            }
            return true;
        }
    }

    public class StatusClass
    {
        // Seems only half of this bitfield is used as it's only 1 byte (8 bits)
        // 0 ?
        // 1 ?
        // 2 IsPlayerHardcore
        // 3 HasDied (also indicates to the game if the character can be played again if hardcore)
        // 4 ?
        // 5 IsExpansion
        // 6 IsLadder (not really used in single player though?)
        // 7 ?
        public Bit[] Flags { get; private set; }
        public bool Hardcore    { get => (bool)Flags[2]; set => Flags[2] = value; }
        public bool Died        { get => (bool)Flags[3]; set => Flags[3] = value; }
        public bool Expansion   { get => (bool)Flags[5]; set => Flags[5] = value; }
        public bool Ladder      { get => (bool)Flags[6]; set => Flags[6] = value; }

        public StatusClass(Bit[]? flags)
        {
            if(flags == null)
                throw new ArgumentNullException(nameof(flags));

            Flags = flags;
        }

        public Bit[] GetFlags()
        {
            return Flags;
        }
    }

    public class ProgressionClass
    {
        // Non Expansion only worries about the 3rd and 4th bits
        // Expansion worries about the 1st up to the 4th bits
        // Hardcore is basically the same thing as it's non-hardcore bit field
        // Gender of the class isn't binary coded it is interpreted
        public Bit[]? progressionFlags = new Bit[PlayerInformationOffsets.OFFSET_PROGRESSION.BitLength];

        // Non Expansion
        // Normal       00100000
        public bool Sir_Male
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     &  ((bool)progressionFlags![2])
                     & !((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = !value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Dame_Female
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & !((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = !value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Nightmare    00010000
        public bool Lord_Male
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & !((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = !value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Lady_Female
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & !((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = !value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Hell         00110000
        public bool Baron_Male
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Baroness_Female
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Non Expansion Hardcore
        // Normal       00100000
        public bool Count_Male
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & !((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = !value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Countess_Female
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & !((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = !value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Nightmare    00010000
        public bool Duke_Male
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & !((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = !value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Duchess_Female
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & !((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = !value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Hell         00110000
        public bool King_Male
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Queen_Female
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }

        // Expansion
        // Normal       10100000
        public bool Slayer
        {
            get
            {
                return ((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & !((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = !value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Nightmare    01010000
        public bool Champion
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & ((bool)progressionFlags![1])
                     & !((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = value;
                progressionFlags![2] = !value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Hell         11110000
        public bool Patriarch_Male
        {
            get
            {
                return ((bool)progressionFlags![0])
                     & ((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = value;
                progressionFlags![1] = value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        public bool Matriarch_Female
        {
            get
            {
                return ((bool)progressionFlags![0])
                     & ((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = value;
                progressionFlags![1] = value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Expansion Hardcore
        // Normal       10100000
        public bool Destroyer
        {
            get
            {
                return ((bool)progressionFlags![0])
                     & !((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & !((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = value;
                progressionFlags![1] = !value;
                progressionFlags![2] = value;
                progressionFlags![3] = !value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Nightmare    01010000
        public bool Conqueror
        {
            get
            {
                return !((bool)progressionFlags![0])
                     & ((bool)progressionFlags![1])
                     & !((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = !value;
                progressionFlags![1] = value;
                progressionFlags![2] = !value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }
        // Hell         11110000
        public bool Guardian
        {
            get
            {
                return ((bool)progressionFlags![0])
                     & ((bool)progressionFlags![1])
                     & ((bool)progressionFlags![2])
                     & ((bool)progressionFlags![3])
                     & !((bool)progressionFlags![4])
                     & !((bool)progressionFlags![5])
                     & !((bool)progressionFlags![6])
                     & !((bool)progressionFlags![7]);
            }
            set
            {
                progressionFlags![0] = value;
                progressionFlags![1] = value;
                progressionFlags![2] = value;
                progressionFlags![3] = value;
                progressionFlags![4] = !value;
                progressionFlags![5] = !value;
                progressionFlags![6] = !value;
                progressionFlags![7] = !value;
            }
        }

        public void ClearProgression()
        {
            progressionFlags![0] = false;
            progressionFlags![1] = false;
            progressionFlags![2] = false;
            progressionFlags![3] = false;
            progressionFlags![4] = false;
            progressionFlags![5] = false;
            progressionFlags![6] = false;
            progressionFlags![7] = false;
        }
        /*
            _class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN
            _class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN || _class == PlayerClass.DRUID
         */

        public int GetCompletedDifficulty()
        {
            if (Destroyer || Slayer || Count_Male || Countess_Female || Sir_Male || Dame_Female) return 1;
            if (Conqueror || Champion || Duke_Male || Duchess_Female || Lord_Male || Lady_Female) return 2;
            if (Patriarch_Male || Matriarch_Female || King_Male || Queen_Female || Baron_Male || Baroness_Female) return 3;
            return 0;
        }

        public string GetProgressionString(PlayerClass _class, bool isExpansion, bool isHardcore)
        {
            if (isExpansion)
            {
                if (isHardcore)
                {
                    if (Destroyer) return "Destroyer";
                    if (Conqueror) return "Conqueror";
                    if (Guardian) return "Guardian";
                }
                else
                {
                    if (Slayer) return "Slayer";
                    if (Champion) return "Champion";
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN || _class == PlayerClass.DRUID)
                        if(Patriarch_Male) return "Patriarch";
                    else
                        if(Matriarch_Female) return "Matriarch";
                }
            }
            else
            {
                if (isHardcore)
                {
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                        if(Count_Male) return "Count";
                    else
                        if(Countess_Female) return "Countess";

                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                        if(Duke_Male) return "Duke";
                    else
                        if(Duchess_Female) return "Duchess";

                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                        if(King_Male) return "King";
                    else
                        if(Queen_Female) return "Queen";
                }
                else
                {
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                        if(Sir_Male) return "Sir";
                    else
                        if(Dame_Female) return "Dame";
                            
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                        if(Lord_Male) return "Lord";
                    else
                        if(Lady_Female) return "Lady";

                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                        if(Baron_Male) return "Baron";
                    else
                        if(Baroness_Female) return "Baroness";
                }
            }
            return "N/A";
        }

        public void SetProgression(PlayerClass _class, bool isExpansion, bool isHardcore, int difficulty = 0)
        {
            ClearProgression();
            if (isExpansion)
            {
                if (isHardcore)
                {
                    switch (difficulty)
                    {
                        case 1:
                            Destroyer = true;
                            break;
                        case 2:
                            Conqueror = true;
                            break;
                        case 3:
                            Guardian = true;
                            break;
                    }
                }
                else
                {
                    switch (difficulty)
                    {
                        case 1:
                            Slayer = true;
                            break;
                        case 2:
                            Champion = true;
                            break;
                        case 3:
                            if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN || _class == PlayerClass.DRUID)
                            {
                                Patriarch_Male = true;
                            } else
                            {
                                Matriarch_Female = true;
                            }
                            break;
                    }
                }
            }
            else
            {
                if (isHardcore)
                {
                    switch (difficulty)
                    {
                        case 1:
                            if(_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                            {
                                Count_Male = true;
                            } else
                            {
                                Countess_Female = true;
                            }
                            break;
                        case 2:
                            if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                            {
                                Duke_Male = true;
                            }
                            else
                            {
                                Duchess_Female = true;
                            }
                            break;
                        case 3:
                            if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                            {
                                King_Male = true;
                            }
                            else
                            {
                                Queen_Female = true;
                            }
                            break;
                    }
                }
                else
                {
                    switch (difficulty)
                    {
                        case 1:
                            if(_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                            {
                                Sir_Male = true;
                            } else
                            {
                                Dame_Female = true;
                            }
                            break;
                        case 2:
                            if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                            {
                                Lord_Male = true;
                            }
                            else
                            {
                                Lady_Female = true;
                            }
                            break;
                        case 3:
                            if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                            {
                                Baron_Male = true;
                            }
                            else
                            {
                                Baroness_Female = true;
                            }
                            break;
                    }
                }
            }
        }

        public ProgressionClass(Bit[] flags)
        {
            progressionFlags = flags;
        }
    }

    public class DifficultyClass
    {
        public Bit[]? flags { get; set; } = new Bit[8];
        public bool Active
        {
            get
            {
                return (bool)flags![7];
            }
            set
            {
                flags = new Bit[8];
                flags![7] = value;
            }
        }
        public bool Act1
        {
            get
            {
                return (bool)flags![0] & !(bool)flags![1] & !(bool)flags![2];
            }
            set
            {
                if (value == true)
                {
                    flags![0] = true;
                    flags![1] = false;
                    flags![2] = false;
                } else
                {
                    flags![0] = false;
                    flags![1] = false;
                    flags![2] = false;
                }
            }
        }
        public bool Act2
        {
            get
            {
                return (bool)flags![0] & (bool)flags![1] & !(bool)flags![2];
            }
            set
            {
                if (value == true)
                {
                    flags![0] = true;
                    flags![1] = true;
                    flags![2] = false;
                }
                else
                {
                    flags![0] = false;
                    flags![1] = false;
                    flags![2] = false;
                }
            }
        }
        public bool Act3
        {
            get
            {
                return !(bool)flags![0] & (bool)flags![1] & !(bool)flags![2];
            }
            set
            {
                if (value == true)
                {
                    flags![0] = false;
                    flags![1] = true;
                    flags![2] = false;
                }
                else
                {
                    flags![0] = false;
                    flags![1] = false;
                    flags![2] = false;
                }
            }
        }
        public bool Act4
        {
            get
            {
                return (bool)flags![0] & (bool)flags![1] & (bool)flags![2];
            }
            set
            {
                if (value == true)
                {
                    flags![0] = true;
                    flags![1] = true;
                    flags![2] = true;
                }
                else
                {
                    flags![0] = false;
                    flags![1] = false;
                    flags![2] = false;
                }
            }
        }
        public bool Act5
        {
            get
            {
                return !(bool)flags![0] & !(bool)flags![1] & (bool)flags![2];
            }
            set
            {
                if (value == true)
                {
                    flags![0] = false;
                    flags![1] = false;
                    flags![2] = true;
                }
                else
                {
                    flags![0] = false;
                    flags![1] = false;
                    flags![2] = false;
                }
            }
        }

        public DifficultyClass()
        {
        }

        public static DifficultyClass Read(BitwiseBinaryReader mainReader)
        {
            DifficultyClass d = new DifficultyClass();

            d.flags = mainReader.ReadSkipPositioning<Bit[]>(new OffsetStruct(PlayerInformationOffsets.OFFSET_DIFFICULTY.Offset, PlayerInformationOffsets.OFFSET_DIFFICULTY.BitLength / 3));
            if(d.flags == null)
                throw new NullReferenceException(nameof(d));

            return d;
        }

        public void SetActiveAct(int act)
        {
            if (act <= 0) return;
            if (act > 5) return;
            if (flags == null) return;

            switch(act)
            {
                case 1:
                    Act1 = true;
                    break;
                case 2:
                    Act2 = true;
                    break;
                case 3:
                    Act3 = true;
                    break;
                case 4:
                    Act4 = true;
                    break;
                case 5:
                    Act5 = true;
                    break;
            }
        }

        public bool Write(BitwiseBinaryWriter writer, int difficultyLevel)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_DIFFICULTY.Offset + difficultyLevel)
                return false;

            writer.WriteBits(flags!);
            return true;
        }

        /*
            Repeat for each difficulty
            bool[3, 8] (2d array 3 rows of 8 columns each)
            We only care about the first 3 bits as there only needs
            to be 5 combintations to give the desired outcome
            the 8th bit is whether or not that act is active or not

            Act1 should be true     000 00001
            Act2 should be true     100 00001
            Act3 should be true     010 00001
            Act4 should be true     110 00001
            Act5 should be true     001 00001
         */
    }
    }
