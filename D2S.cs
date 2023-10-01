using D2SLib2.Model;
using D2SLib2.BinaryHandler;
using D2SLib2.Structure.Header;
using D2SLib2.Structure.Other;
using D2SLib2.Structure.Player;
using D2SLib2.Structure.Quests;
using D2SLib2.Structure.Waypoints;
using D2SLib2.Structure.Player.Item;
using Newtonsoft.Json;

// How to scaffold your database you dipshit: Scaffold-DbContext "DataSource=.\\D2SLibData.db" Microsoft.EntityFrameworkCore.Sqlite -outputdir tmpModel

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
            Inventory.FindInventoryOffsetInBytes(reader);

            fileHeader = Header.Read(reader);
            playerInformation = PlayerInformation.Read(reader);
            menuAppearance = new MenuAppearance();
            menuAppearance.Read(reader);
            menuAppearance.ReadD2R(reader);
            mercenary = Mercenary.Read(reader);
            questBook = QuestBook.Read(reader);
            waypointBook = WaypointBook.Read(reader);
            NPC = NPC.Read(reader);

            ConvertD2SToJson(this);
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