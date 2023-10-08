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

        private Bit[]? progressionFlags = new Bit[PlayerInformationOffsets.OFFSET_PROGRESSION.BitLength];
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

        public PlayerInformation() { }

        public static PlayerInformation Read(BitwiseBinaryReader mainReader)
        {
            PlayerInformation playerInfo = new PlayerInformation();

            playerInfo.ActiveWeapon = mainReader.Read<UInt32>(PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON.BitLength, $"Active Weapon: {playerInfo.ActiveWeapon}");

            playerInfo.statusClass = new StatusClass(mainReader.Read<Bit[]>(PlayerInformationOffsets.OFFSET_CHARACTER_STATUS));
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_CHARACTER_STATUS.BitLength, $"Status Class: {playerInfo.statusClass.Flags!.ToStringRepresentation()}");

            playerInfo.progressionFlags = mainReader.Read<Bit[]>(PlayerInformationOffsets.OFFSET_PROGRESSION);
            Logger.WriteSection(mainReader, PlayerInformationOffsets.OFFSET_PROGRESSION.BitLength, $"Progression Flags: {playerInfo.progressionFlags!.ToStringRepresentation()}");

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
                playerInfo.AssignedSkills[i] = mainReader.ReadBits(PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.BitLength / 16).ToUInt32();
                if (playerInfo.AssignedSkills[i] == UInt16.MaxValue) playerInfo.AssignedSkills[i] = 0;
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

            Logger.WriteBeginSection("[Begin Inventory Reading]");
            mainReader.SetBitPosition(Inventory.InventoryOffset);
            playerInfo.inventory = Inventory.Read(mainReader);
            Inventory.EndInventoryOffset = mainReader.bitPosition;
            Logger.WriteEndSection("[End Inventory Reading]");

            Logger.WriteBeginSection("[Begin Corpse Inventory Reading]");
            playerInfo.ReadCorpseInformation(mainReader);
            Logger.WriteEndSection("[End Corpse Inventory Reading]");

            if (playerInfo.statusClass.Expansion)
            {
                Logger.WriteBeginSection("[Begin Mercenary Inventory Reading]");
                playerInfo.ReadMercenaryInventory(mainReader);
                Logger.WriteEndSection("[End Mercenary Inventory Reading]");

                Logger.WriteBeginSection("[Begin Iron Golem Inventory Reading]");
                playerInfo.ReadIronGolemInventory(mainReader);
                Logger.WriteEndSection("[End Iron Golem Inventory Reading]");
            }

            // To ensure no issues when setting bits
            playerInfo.Progression = new ProgressionClass(playerInfo.progressionFlags!, playerInfo.playerClass, playerInfo.statusClass.Expansion, playerInfo.statusClass.Hardcore);
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
                IronGolemItem = new ItemStructure(mainReader);
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

        public bool WriteActiveWeapon(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_ACTIVE_WEAPON.Offset)
                return false;

           writer.WriteBits(ActiveWeapon.ToBits());
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

           writer.WriteBits(statusClass!.GetFlags().ToBytes().ToBits(), false);
           return true;
        }

        public bool WriteProgression(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_PROGRESSION.Offset)
                return false;

           writer.WriteBits(progressionFlags!.ToBytes().ToBits());
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

           writer.WriteBits(((byte)playerClass).ToBits());
            return true;
        }

        public bool WritePlayerLevel(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_PLAYERLEVEL.Offset)
            {
                if (writer.GetBytes().Length + 2 != PlayerInformationOffsets.OFFSET_PLAYERLEVEL.Offset)
                    return false;

               writer.WriteBits(((byte)0x10).ToBits());
               writer.WriteBits(((byte)0x1E).ToBits());
            }

           writer.WriteBits(((byte)playerLevel).ToBits());
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
           writer.WriteBits(((UInt32)unixTimestamp).ToBits());
            return true;
        }

        public bool WriteAssignedSkills(BitwiseBinaryWriter writer)
        {
            if(writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.Offset)
            {
                if (writer.GetBytes().Length + 4 != PlayerInformationOffsets.OFFSET_ASSIGNED_SKILLS.Offset)
                    return false;

               writer.WriteBits(((UInt32)0xFFFFFFFF).ToBits());
            }

            for (int i = 0; i < 16; i++)
            {
                UInt32 skillId = AssignedSkills[i];
                if (skillId == 0)
                {
                   writer.WriteBits(0xFFFF.ToBits());
                }
                else
                {
                   writer.WriteBits(skillId.ToBits());
                }
            }

           writer.WriteBits(LeftMouseSkill.ToBits());
           writer.WriteBits(RightMouseSkill.ToBits());
           writer.WriteBits(SwitchLeftMouseSkill.ToBits());
           writer.WriteBits(SwitchRightMouseSkill.ToBits());

            return true;
        }

        public bool WriteMapSeed(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_MAPID.Offset)
                return false;

           writer.WriteBits(MapSeed.ToBits());
            return true;
        }

        public bool WritePlayerName(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_PLAYER_NAME.Offset)
                return false;

            for(int i=0;i<16;i++)
            {
                if (PlayerName.Length > i) writer.WriteBits(((byte)PlayerName[i]).ToBits());
                else writer.WriteBits('\0'.ToBits());
            }

            return true;
        }

        public bool WriteCorpseInformation(BitwiseBinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public bool WriteMercenaryInventoryInformation(BitwiseBinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public bool WriteIronGolemInformation(BitwiseBinaryWriter writer)
        {
            throw new NotImplementedException();
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
        public bool Hardcore    { get => Flags[2]; set => Flags[2] = new Bit(value); }
        public bool Died        { get => Flags[3]; set => Flags[3] = new Bit(value); }
        public bool Expansion   { get => Flags[5]; set => Flags[5] = new Bit(value); }
        public bool Ladder      { get => Flags[6]; set => Flags[6] = new Bit(value); }

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

        // Non Expansion
            // Normal       00100000
        public bool Sir_Male = false;
        public bool Dame_Female = false;
            // Nightmare    00010000
        public bool Lord_Male = false;
        public bool Lady_Female = false;
            // Hell         00110000
        public bool Baron_Male = false;
        public bool Baroness_Female = false;
        // Non Expansion Hardcore
            // Normal       00100000
        public bool Count_Male = false;
        public bool Countess_Female = false;
            // Nightmare    00010000
        public bool Duke_Male = false;
        public bool Duchess_Female = false;
            // Hell         00110000
        public bool King_Male = false;
        public bool Queen_Female = false;

        // Expansion
            // Normal       10100000
        public bool Slayer = false;
            // Nightmare    01010000
        public bool Champion = false;
            // Hell         11110000
        public bool Patriarch_Male = false;
        public bool Matriarch_Female = false;
        // Expansion Hardcore
            // Normal       10100000
        public bool Destroyer = false;
            // Nightmare    01010000
        public bool Conqueror = false;
            // Hell         11110000
        public bool Guardian = false;

        public ProgressionClass(Bit[] flags, PlayerClass _class, bool isExpansion, bool isHardcore)
        {
            if(!isExpansion)
            {
                if (!isHardcore)
                {
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                    {
                        Sir_Male = flags[2] & !flags[3];
                        Lord_Male = !flags[2] & flags[3];
                        Baron_Male = flags[2] & flags[3];
                    }
                    else
                    {
                        Dame_Female = flags[2] & !flags[3];
                        Lady_Female = !flags[2] & flags[3];
                        Baroness_Female = flags[2] & flags[3];
                    }
                } else {
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN || _class == PlayerClass.PALADIN)
                    {
                        Count_Male = flags[2] & !flags[3];
                        Duke_Male = !flags[2] & flags[3];
                        King_Male = flags[2] & flags[3];
                    }
                    else
                    {
                        Countess_Female = flags[2] & !flags[3];
                        Duchess_Female = !flags[2] & flags[3];
                        Queen_Female = flags[2] & flags[3];
                    }
                }
            } else
            {
                if (!isHardcore)
                {
                    Slayer = flags[0] & !flags[1] & flags[2] & !flags[3];
                    Champion = !flags[0] & flags[1] & !flags[2] & flags[3];
                    if (_class == PlayerClass.NECROMANCER || _class == PlayerClass.BARBARIAN
                        || _class == PlayerClass.PALADIN || _class == PlayerClass.DRUID)
                    {
                        Patriarch_Male = flags[0] & flags[1] & flags[2] & flags[3];
                    }
                    else
                    {
                        Matriarch_Female = flags[0] & flags[1] & flags[2] & flags[3];
                    }
                }
                else
                {
                    Destroyer = flags[0] & !flags[1] & flags[2] & !flags[3];
                    Conqueror = !flags[0] & flags[1] & !flags[2] & flags[3];
                    Guardian = flags[0] & flags[1] & flags[2] & flags[3];
                }
            }
        }
    }

    public class DifficultyClass
    {
        public Bit[]? flags { get; set; } = new Bit[8];
        public bool Active { get; set; } = false;
        public bool Act1 { get; set; } = false;
        public bool Act2 { get; set; } = false;
        public bool Act3 { get; set; } = false;
        public bool Act4 { get; set; } = false;
        public bool Act5 { get; set; } = false;

        public DifficultyClass()
        {
        }

        public static DifficultyClass Read(BitwiseBinaryReader mainReader)
        {
            DifficultyClass d = new DifficultyClass();

            d.flags = mainReader.ReadSkipPositioning<Bit[]>(new OffsetStruct(PlayerInformationOffsets.OFFSET_DIFFICULTY.Offset, PlayerInformationOffsets.OFFSET_DIFFICULTY.BitLength / 3));
            if(d.flags == null)
                throw new NullReferenceException(nameof(d));

            d.Active = (bool)d.flags[7];
            d.Act1 =  (bool)d.flags[0] & !(bool)d.flags[1] & !(bool)d.flags[2];
            d.Act2 =  (bool)d.flags[0] &  (bool)d.flags[1] & !(bool)d.flags[2];
            d.Act3 = !(bool)d.flags[0] &  (bool)d.flags[1] & !(bool)d.flags[2];
            d.Act4 =  (bool)d.flags[0] &  (bool)d.flags[1] &  (bool)d.flags[2];
            d.Act5 = !(bool)d.flags[0] & !(bool)d.flags[1] &  (bool)d.flags[2];

            return d;
        }

        public void SetActiveAct(int act)
        {
            if (act <= 0) return;
            if (act > 5) return;
            if (flags == null) return;

            for (int i = 0; i < flags.Length; i++) flags[i] = new Bit(0);

            switch(act)
            {
                case 1:
                    flags[7] = new Bit(1);
                    flags[0] = new Bit(1);
                    flags[1] = new Bit(0);
                    flags[2] = new Bit(0);
                    break;
                case 2:
                    flags[7] = new Bit(1);
                    flags[0] = new Bit(1);
                    flags[1] = new Bit(1);
                    flags[2] = new Bit(0);
                    break;
                case 3:
                    flags[7] = new Bit(1);
                    flags[0] = new Bit(0);
                    flags[1] = new Bit(1);
                    flags[2] = new Bit(0);
                    break;
                case 4:
                    flags[7] = new Bit(1);
                    flags[0] = new Bit(1);
                    flags[1] = new Bit(1);
                    flags[2] = new Bit(1);
                    break;
                case 5:
                    flags[7] = new Bit(1);
                    flags[0] = new Bit(0);
                    flags[1] = new Bit(0);
                    flags[2] = new Bit(1);
                    break;
            }

            Active =    (bool)flags[7];
            Act1 =      (bool)flags[0] & !(bool)flags[1] & !(bool)flags[2];
            Act2 =      (bool)flags[0] &  (bool)flags[1] & !(bool)flags[2];
            Act3 =     !(bool)flags[0] &  (bool)flags[1] & !(bool)flags[2];
            Act4 =      (bool)flags[0] &  (bool)flags[1] &  (bool)flags[2];
            Act5 =     !(bool)flags[0] & !(bool)flags[1] &  (bool)flags[2];
        }

        public bool Write(BitwiseBinaryWriter writer, int difficultyLevel)
        {
            if (writer.GetBytes().Length != PlayerInformationOffsets.OFFSET_DIFFICULTY.Offset + difficultyLevel)
                return false;

           writer.WriteBits(flags!.ToByte().ToBits());
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
