using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Attributes
    {
        public const int BytePosition = 765;
        public const int BitPosition = BytePosition * sizeof(long);
        public static int? Length { get; private set; } = null;

        // Convert this into SQL?
        public static Dictionary<int, KeyValuePair<int, int>> AttributeBitLength = new Dictionary<int, KeyValuePair<int, int>>
        {
            // Attribute ID     <Bit Length, ValShift>                      // Stat Name
            { 0,                new KeyValuePair<int, int>(10, -1) },		//Strength
            { 1,                new KeyValuePair<int, int>(10, -1) },		//Energy
            { 2,                new KeyValuePair<int, int>(10, -1) },		//Dexterity
            { 3,                new KeyValuePair<int, int>(10, -1) },		//Vitality
            { 4,                new KeyValuePair<int, int>(10, -1) },		//Unused stats
            { 5,                new KeyValuePair<int, int>(8,  -1) },       //Unused skills
            { 6,                new KeyValuePair<int, int>(21,  8) },		//Current HP
            { 7,                new KeyValuePair<int, int>(21,  8) },		//Max HP
            { 8,                new KeyValuePair<int, int>(21,  8) },		//Current mana
            { 9,                new KeyValuePair<int, int>(21,  8) },		//Max mana
            { 10,               new KeyValuePair<int, int>(21,  8) },		//Current stamina,
            { 11,               new KeyValuePair<int, int>(21,  8) },		//Max stamina,
            { 12,               new KeyValuePair<int, int>(7,  -1) },		//Level
            { 13,               new KeyValuePair<int, int>(32, -1) },		//Experience,
            { 14,               new KeyValuePair<int, int>(25, -1) },		//Gold,
            { 15,               new KeyValuePair<int, int>(25, -1) }		//Stashed gold
        };

        public static Dictionary<int, uint> GetAttributes(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);
            if (reader.ReadBits(16).To_String() != "gf")
                throw new Exception("Error finding Attributes header");

            List<bool> bits = new List<bool>();
            while(reader.PeekBits(16).To_String() != "if") bits.AddRange(reader.ReadBits(8));

            Dictionary<int, uint> attributes = new Dictionary<int, uint>();

            BitwiseBinaryReader r = new BitwiseBinaryReader(bits.ToArray());
            {
                while (r.PeekBits(9).ToUInt16() != 0x1FF)
                {
                    // Reads 9 bits, converts each bit to its numeric value (2^index if true),
                    //  and sums them to get the ushort attributeId. (bits are in reverse for some reason)
                    ushort attributeId = r.ReadBits(9).ToUInt16();

                    // Reads n bits, converts each bit to it's numeric value (2^index if true),
                    //  and sums them to get the uint attributeValue
                    uint attributeValue = uint.MaxValue;
                    if (AttributeBitLength.TryGetValue(attributeId, out KeyValuePair<int, int> kvp))
                    {
                        attributeValue = (uint)r.ReadBits(kvp.Key).ToInt32();
                    }
                    else throw new KeyNotFoundException(nameof(AttributeBitLength));
                    if (attributeValue == uint.MaxValue)
                        throw new Exception("Invalid attribute value, corrupt save?");

                    // We then check to see if the attribute value is bitshifted
                    if(AttributeBitLength.Single(x => x.Key == (int)attributeId).Value.Value != -1)
                    {
                        attributeValue >>= AttributeBitLength.Single(x => x.Key == (int)attributeId).Value.Value;
                    }

                    attributes.Add(attributeId, attributeValue);
                }
            }

            if (attributes[12] != D2S.instance!.level)
                throw new Exception("Level missmatch, corrupt save?");

            Length = bits.Count() + 16;

            // Re align the bytes
            reader.Align();
            //reader.SetBytePosition(BytePosition + (int)Length);
            return attributes;
        }
    }
}
