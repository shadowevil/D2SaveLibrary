using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Player
{
    public class Attributes
    {
        public HashSet<Attribute> attributeList = new HashSet<Attribute>();

        public Attributes() { }

        public static Attributes Read(BitwiseBinaryReader mainReader)
        {
            Attributes att = new Attributes();

            mainReader.SetBytePosition(AttributeOffsets.OFFSET_SIGNATURE.Offset);
            if (mainReader.ReadBits(AttributeOffsets.OFFSET_SIGNATURE.BitLength).ToStr() != "gf")
                throw new OffsetException("Unable to get Attributes signature, corrupt save?");

            att.attributeList = new HashSet<Attribute>();

            while(mainReader.PeekBits(9).ToUInt16() != 0x1FF)
            {
                ushort attributeId = mainReader.ReadBits(9).ToUInt16();
                uint attributeValue = uint.MaxValue;
                if (D2S.instance!.dbContext!.ItemStatCosts.SingleOrDefault(x => x.Id == attributeId)?.CsvBits is double csvBits)
                {
                    attributeValue = mainReader.ReadBits((int)csvBits).ToUInt32();
                }
                else throw new Exception($"Unable to query database with attribute Id: {attributeId}");

                if (attributeValue == uint.MaxValue)
                    throw new Exception("Invalid attribute value, corrupt save?");

                if(D2S.instance!.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Id == attributeId)?.ValShift is double valShift)
                {
                    if(valShift > 0)
                    {
                        attributeValue >>= (int)valShift;
                    }
                }

                att.attributeList.Add(new Attribute() { Id = attributeId, Value = attributeValue });
            }

            mainReader.Align();

            return att;
        }
    }

    [DebuggerDisplay("{Id}: {Value}")]
    public class Attribute
    {
        public UInt16 Id { get; set; } = UInt16.MaxValue;
        public UInt32 Value { get; set; } = UInt32.MaxValue;
    }
}
