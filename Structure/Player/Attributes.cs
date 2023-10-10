using D2SLib2.BinaryHandler;
using D2SLib2.Model;
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
            if(mainReader.Read<string>(AttributeOffsets.OFFSET_SIGNATURE) != AttributeOffsets.OFFSET_SIGNATURE.Signature)
                throw new OffsetException("Unable to get Attributes signature, corrupt save?");

            att.attributeList = new HashSet<Attribute>();

            while(mainReader.PeekBits(9).ToUInt16(9, Endianness.BigEndian) != 0x1FF)
            {
                ushort attributeId = mainReader.ReadBits(9).ToUInt16(9, Endianness.BigEndian);
                Logger.WriteSection(mainReader, 9, $"Attribute Id: {attributeId}");
                Int32 attributeValue = Int32.MaxValue;
                if (D2S.instance!.dbContext!.ItemStatCosts.SingleOrDefault(x => x.Id == attributeId)?.CsvBits is double csvBits)
                {
                    attributeValue = mainReader.ReadBits((int)csvBits).ToInt32((uint)csvBits, Endianness.BigEndian);
                    Logger.WriteSection(mainReader, (int)csvBits, $"Attribute Value: {attributeValue}");
                }
                else
                {
                    Logger.Close();
                    throw new Exception($"Unable to query database with attribute Id: {attributeId}");
                }

                if (attributeValue == Int32.MaxValue)
                    throw new Exception("Invalid attribute value, corrupt save?");

                if(D2S.instance!.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Id == attributeId)?.ValShift is double valShift)
                {
                    if(valShift > 0)
                    {
                        attributeValue >>= (int)valShift;
                        Logger.WriteSection(mainReader, (int)csvBits, $"After Valshift Attribute Value: {attributeValue}");
                    }
                }

                att.attributeList.Add(new Attribute() { Id = attributeId, Value = attributeValue });
            }

            mainReader.Align();

            return att;
        }

        public bool Write(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != AttributeOffsets.OFFSET_SIGNATURE.Offset)
                return false;
            writer.WriteBits(AttributeOffsets.OFFSET_SIGNATURE.Signature.ToBits());

            foreach(Attribute a in attributeList)
            {
                writer.WriteBits(a.Id.ToBits(9));
                ItemStatCost? isc = D2S.instance!.dbContext?.ItemStatCosts.SingleOrDefault(x => x.Id == a.Id);
                if (isc == null) throw new Exception($"ItemStatCost not found for attribute {a.Id}");
                if (isc.CsvBits == null || isc.CsvBits <= 0) throw new Exception($"ItemStatCost CsvBits for {a.Id} returned null or 0");

                Int32 val = a.Value;
                if(isc.ValShift != null || isc.ValShift > 0)
                {
                    val <<= (int)isc.ValShift;
                }
                writer.WriteBits(((Int32)val).ToBits((uint)isc.CsvBits));
            }
            writer.WriteBits(0x1FF.ToBits(9));
            writer.Align();

            return true;
        }
    }

    [DebuggerDisplay("{Id}: {Value}")]
    public class Attribute
    {
        public UInt16 Id { get; set; } = UInt16.MaxValue;
        public Int32 Value { get; set; } = Int32.MaxValue;
    }
}
