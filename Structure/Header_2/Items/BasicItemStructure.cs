using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header.Items
{
    public class BasicItemStructure
    {
        private BitwiseBinaryReader BasicItemReader;
        public const int Length = 4;

        public bool[] flags = new bool[32];

        public BasicItemStructure(BitwiseBinaryReader reader)
        {
            if(reader == null)
                throw new ArgumentNullException(nameof(reader));

            BasicItemReader = new BitwiseBinaryReader(reader.ReadBits(Length * 8));

            flags = BasicItemReader.ReadBits(32);
        }
    }
}
