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

        public Header fileHeader { get; set; }
        public PlayerInformation playerInformation { get; set; }
        public MenuAppearance menuAppearance { get; set; }
        public Mercenary mercenary { get; set; }
        public QuestBook questBook { get; set; }
        public WaypointBook waypointBook { get; set; }
        public NPC NPC { get; set; }

        [JsonIgnore]
        public HuffmanTree itemCodeTree = new HuffmanTree();

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

            Logger.WriteBeginSection(reader, "[Looking for Inventory]");
            Inventory.FindInventoryOffsetInBytes(reader);
            Logger.WriteEndSection(reader, "[End of Inventory Search]");
            Logger.WriteBeginSection(reader, "[Looking for Skills]");
            SkillsClass.FindSkillOffsetInBytes(reader);
            Logger.WriteEndSection(reader, "[End of Skills Search]");

            Logger.WriteBeginSection(reader, "[Begin Reading Header]");
            fileHeader = Header.Read(reader);
            Logger.WriteEndSection(reader, "[End Reading Header]");

            Logger.WriteBeginSection(reader, "[Begin Player Information]");
            playerInformation = PlayerInformation.Read(reader);
            Logger.WriteEndSection(reader, "[End Player Information]");

            menuAppearance = new MenuAppearance();


            menuAppearance.Read(reader);


            menuAppearance.ReadD2R(reader);


            mercenary = Mercenary.Read(reader);


            questBook = QuestBook.Read(reader);


            waypointBook = WaypointBook.Read(reader);


            NPC = NPC.Read(reader);



            Logger.Close();
        }

        public static void ConvertD2SToJson(D2S d2sInstance)
        {
            string jsonString = JsonConvert.SerializeObject(d2sInstance, Formatting.Indented);
            string? filePathDirectory = Path.GetDirectoryName(d2sInstance.OpenFilePath);
            string fileName = d2sInstance.playerInformation.PlayerName + ".json";

            File.WriteAllText(filePathDirectory + "\\" + fileName, jsonString);
        }
    }
}