using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public class Mercenary
    {
        public static Mercenary? instance { get; private set; } = null;
        public const int BytePosition = 177;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 14;

        private BitwiseBinaryReader? mercReader { get; set; } = null;

        public static Mercenary ReadMercenaryBytes(BitwiseBinaryReader reader)
        {
            instance = new Mercenary();
            reader.SetBitPosition(BitPosition);
            instance.mercReader = new BitwiseBinaryReader(reader.ReadBits(Length*8));
            return instance;
        }

        public UInt16 IsDead
        {
            get
            {
                if (mercReader == null)
                    throw new NullReferenceException(nameof(mercReader));
                mercReader.SetBitPosition(0);
                return mercReader.ReadBits(16).ToUInt16();
            }
        }

        public UInt32 Seed
        {
            get
            {
                if (mercReader == null)
                    throw new NullReferenceException(nameof(mercReader));
                mercReader.SetBitPosition(16);
                return mercReader.ReadBits(32).ToUInt32();
            }
        }

        public UInt16 NameID
        {
            get
            {
                if (mercReader == null)
                    throw new NullReferenceException(nameof(mercReader));
                mercReader.SetBitPosition(48);
                return mercReader.ReadBits(16).ToUInt16();
            }
        }

        public UInt16 MercType
        {
            get
            {
                if (mercReader == null)
                    throw new NullReferenceException(nameof(mercReader));
                mercReader.SetBitPosition(64);
                return mercReader.ReadBits(16).ToUInt16();
            }
        }

        public UInt32 Experience
        {
            get
            {
                if (mercReader == null)
                    throw new NullReferenceException(nameof(mercReader));
                mercReader.SetBitPosition(80);
                return mercReader.ReadBits(32).ToUInt32();
            }
        }
    }
}
