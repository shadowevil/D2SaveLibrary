using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Header
{
    public static class AssignedSkills
    {
        public const int BytePosition = 56;
        public const int BitPosition = BytePosition * sizeof(long);
        public const int Length = 64;

        public static UInt32[] GetAssignedSkills(BitwiseBinaryReader reader)
        {
            UInt32[] skillIds = new UInt32[Length/4]; // Should be 16 skill ID's 4 bytes each (64 bytes / 4 == 16)
            reader.SetBitPosition(BitPosition);
            for(int i=0;i<16;i++)
            {
                // We set the value
                skillIds[i] = reader.ReadBits(32).ToUInt32();
                // We check if it's a ushort max value which is
                //  65535 and not a valid skill so we put 0x00
                skillIds[i] = skillIds[i] == UInt16.MaxValue ? 0 : skillIds[i];
            }
            return skillIds;
        }
    }
}
