using D2SLib2.BinaryHandler;
using Microsoft.Toolkit.HighPerformance;
using D2SLib2.Structure.Player.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //private Bit[,] DifficultyFlags = new Bit[3, 8];
        //public DifficultyClass Normal => new DifficultyClass(DifficultyFlags.GetRow(0).ToArray());
        //public DifficultyClass Nightmare => new DifficultyClass(DifficultyFlags.GetRow(1).ToArray());
        //public DifficultyClass Hell => new DifficultyClass(DifficultyFlags.GetRow(2).ToArray());
        public DifficultyClass? Normal { get; set; } = null;
        public DifficultyClass? Nightmare { get; set; } = null;
        public DifficultyClass? Hell { get; set; } = null;

        public UInt32 MapSeed = UInt32.MaxValue;

        public string PlayerName = string.Empty;

        public Attributes? attributes = null;
        public SkillsClass? skillsClass = null;
        public Inventory? inventory = null;

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

            playerInfo.inventory = Inventory.Read(mainReader);

            // To ensure no issues when setting bits
            playerInfo.Progression = new ProgressionClass(playerInfo.progressionFlags!, playerInfo.playerClass, playerInfo.statusClass.Expansion, playerInfo.statusClass.Hardcore);
            return playerInfo;
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
        public Bit[]? Flags { get; set; } = null;
        public bool Hardcore = false;
        public bool Died = false;
        public bool Expansion = false;
        public bool Ladder = false;

        public StatusClass(Bit[]? flags)
        {
            if(flags == null)
                throw new ArgumentNullException(nameof(flags));
            Flags = flags;
            Hardcore = flags[2];
            Died = flags[3];
            Expansion = flags[5];
            Ladder = flags[6];
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
