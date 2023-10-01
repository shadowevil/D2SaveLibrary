using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class Skills
    {
        public static int BytePosition
        {
            get
            {
                if (Attributes.Length == null)
                    throw new NullReferenceException();

                return Attributes.BytePosition + (int)Attributes.Length;
            }
        }
        public static int BitPosition
        {
            get
            {
                return BytePosition * sizeof(long);
            }
        }

        public const int Length = 32;

        public static Dictionary<int, int> SkillOffsetByClass = new Dictionary<int, int>()
        {
            // Class ID     Offset
            { 0,            6 },     // Amazon
            { 1,            36 },    // Sorceress
            { 2,            66 },    // Necromancer
            { 3,            96 },    // Paladin
            { 4,            126 },   // Barbarian
            { 5,            221 },   // Druid
            { 6,            251 },   // Assassin
        };

        public static Dictionary<int, int> GetSkills(BitwiseBinaryReader reader)
        {
            reader.SetBitPosition(BitPosition);

            if (reader.ReadBits(16).To_String() != "if")
                throw new Exception("Skills header not found");

            Dictionary<int, int> SkillOffsetsAndLevel = new Dictionary<int, int>();

            int cClass = (int)D2S.instance!.cClass!;
            if (SkillOffsetByClass.TryGetValue(cClass, out int skillOffset))
            {
                for (int i = 0; i < 30; i++)
                {
                    SkillOffsetsAndLevel.Add(skillOffset + i, reader.ReadBits(8).ToByte());
                }
            }
            else
                throw new KeyNotFoundException();

            return SkillOffsetsAndLevel;
        }
    }
}
