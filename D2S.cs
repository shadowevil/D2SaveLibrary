using D2SLib2.Model;
using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Header;
using D2SLib2.Structure.Other;
using D2SLib2.Structure.Player;
using D2SLib2.Structure.Quests;
using D2SLib2.Structure.Waypoints;
using D2SLib2.Structure.Player.Item;
using Newtonsoft.Json;
using System.Formats.Asn1;

// How to scaffold your database you dipshit:
//              Scaffold-DbContext "DataSource=C:\\Users\\ShadowEvil\\source\\repos\\D2SLib2\\D2STesting\\D2SLibData.db" Microsoft.EntityFrameworkCore.Sqlite -outputdir tmpModel

namespace D2SLib2
{
    public class D2S
    {
        [JsonIgnore]
        public string OpenFilePath = string.Empty;

        [JsonIgnore]
        public static D2S? instance { get; private set; } = null;

        [JsonIgnore]
        public D2slibDataContext? dbContext { get; private set; } = null;

        public Header? fileHeader { get; set; }
        public PlayerInformation? playerInformation { get; set; } = null;

        [JsonIgnore]
        public MenuAppearance? menuAppearance { get; set; } = null;

        public Mercenary? mercenary { get; set; } = null;
        public QuestBook? questBook { get; set; } = null;
        public WaypointBook? waypointBook { get; set; } = null;
        public NPCIntroduction? NPC { get; set; } = null;

        [JsonIgnore]
        public HuffmanTree itemCodeTree = new HuffmanTree();

        [JsonIgnore]
        public bool CloseFlag = false;

        public D2S(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException($"{filepath} not found.");
            OpenFilePath = filepath;
            instance = this;
            itemCodeTree.Build();
            dbContext = new D2slibDataContext();

            BitwiseBinaryReader reader = new BitwiseBinaryReader(OpenFilePath);
            string fileName = Path.GetFileNameWithoutExtension(OpenFilePath);
            Logger.LogPath = Directory.GetCurrentDirectory() + $"\\Logs\\{fileName}.d2slog";
            Logger.WriteHeader("#--------------------------------------------------------------------------------------------------------------------#");
            Logger.WriteHeader("#-                                                                                                                  -#");
            Logger.WriteHeader("#-                                      D2S Log File for debugging                                                  -#");
            Logger.WriteHeader("#-                                               v0.5.0.1                                                           -#");
            Logger.WriteHeader("#-                                                                                                                  -#");
            Logger.WriteHeader("#--------------------------------------------------------------------------------------------------------------------#");

            Logger.WriteBeginSection("[Looking for Inventory]");
            Inventory.FindInventoryOffsetInBytes(reader);
            Logger.WriteEndSection("[End of Inventory Search]");
            Logger.WriteBeginSection("[Looking for Skills]");
            SkillsClass.FindSkillOffsetInBytes(reader);
            Logger.WriteEndSection("[End of Skills Search]");

            Logger.WriteBeginSection("[Begin Reading Header]");
            fileHeader = Header.Read(reader);
            Logger.WriteEndSection("[End Reading Header]");
            if (!CloseFlag)
            {
                Logger.WriteBeginSection("[Begin Mercenary]");
                mercenary = Mercenary.Read(reader);
                Logger.WriteEndSection("[End Mercenary]");

                Logger.WriteBeginSection("[Begin Player Information]");
                playerInformation = PlayerInformation.Read(reader);
                Logger.WriteEndSection("[End Player Information]");

                Logger.WriteBeginSection("[Begin D2Menu Appearance]");
                menuAppearance = new MenuAppearance();
                menuAppearance.Read(reader);
                Logger.WriteEndSection("[End D2Menu Appearance]");

                Logger.WriteBeginSection("[Begin D2R Appearance]");
                menuAppearance.ReadD2R(reader);
                Logger.WriteEndSection("[End D2R Appearance]");

                Logger.WriteBeginSection("[Begin Quests]");
                questBook = QuestBook.Read(reader);
                Logger.WriteEndSection("[End Quests]");

                Logger.WriteBeginSection("[Begin Waypoints]");
                waypointBook = WaypointBook.Read(reader);
                Logger.WriteEndSection("[End Waypoints]");

                Logger.WriteBeginSection("[Begin NPC]");
                NPC = NPCIntroduction.Read(reader);
                Logger.WriteEndSection("[End NPC]");
            }
            Logger.Close();
        }

        public static void ConvertD2SToJson(D2S d2sInstance)
        {
            string jsonString = JsonConvert.SerializeObject(d2sInstance, Formatting.Indented);
            string? filePathDirectory = Path.GetDirectoryName(d2sInstance.OpenFilePath);
            string fileName = d2sInstance.playerInformation!.PlayerName + ".json";

            File.WriteAllText(filePathDirectory + "\\" + fileName, jsonString);
        }
    }
}