using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Items
{
    public static class ItemMain
    {
        public static int BytePosition
        {
            get
            {
                return Skills.BytePosition + Skills.Length;
            }
        }
        public static int BitPosition { get { return BytePosition * sizeof(long); } }
        public const int Length = 4;

        public static List<ItemStructure> GetItems(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            if (reader.ReadBits(16).To_String() != "JM")
                throw new Exception("Item header missing");

            int itemCount = reader.ReadBits(16).ToUInt16();

            List<ItemStructure> items = new List<ItemStructure>();

            for(int i=0;i<itemCount;i++)
            {
                items.Add(new ItemStructure(reader));
            }

            return items;
        }
    }
}
