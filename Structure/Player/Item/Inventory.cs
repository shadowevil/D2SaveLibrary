using D2SLib2.BinaryHandler;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Player.Item
{
    public class Inventory
    {
        public int ItemCount = 0;
        public HashSet<ItemStructure> ItemList = new HashSet<ItemStructure>();

        public Inventory() { }

        public static Inventory Read(BitwiseBinaryReader mainReader)
        {
            Inventory inventory = new Inventory();
            mainReader.SetBitPosition(InventoryOffset);
            inventory.ItemCount = (int)mainReader.ReadItemBits<UInt16>(InventoryOffsets.OFFSET_ITEM_COUNT);
            
            inventory.ItemList = new HashSet<ItemStructure>();
            for(int i=0;i<inventory.ItemCount;i++)
            {
                inventory.ItemList.Add(new ItemStructure(mainReader));
            }

            return inventory;
        }

        [DebuggerDisplay("This is not intended to be accessed to change outside the original runtime.")]
        public static int InventoryOffset = -1;
        public static void FindInventoryOffsetInBytes(BitwiseBinaryReader mainReader)
        {
            mainReader.SetBytePosition(InventoryOffsets.OFFSET_START_SEARCH.Offset);
            while (mainReader.PeekBits(16).ToStr() != InventoryOffsets.OFFSET_START_SEARCH.Signature) mainReader.ReadByte();

            if (mainReader.ReadSkipPositioning<string>(InventoryOffsets.OFFSET_START_SEARCH) != InventoryOffsets.OFFSET_START_SEARCH.Signature)
                throw new OffsetException("Unable to find Inventory offset, corrupt save?");

            InventoryOffset = mainReader.bitPosition;
        }
    }
}
