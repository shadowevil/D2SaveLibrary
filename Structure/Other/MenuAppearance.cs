using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Other
{
    public class MenuAppearance
    {
        public MenuAppearance() { }

        public byte[] menuAppearance = new byte[OtherOffsets.OFFSET_CHARACTER_MENU_APPEARANCE.ByteLength];
        public byte[] menuAppearanceD2R = new byte[OtherOffsets.OFFSET_D2R_CHARACTER_MENU_APPEARANCE.ByteLength];

        public void Read(BitwiseBinaryReader mainReader)
        {
            menuAppearance = mainReader.Read<byte[]>(OtherOffsets.OFFSET_CHARACTER_MENU_APPEARANCE) ?? throw new Exception("Unable to read Character Menu Appearance");
        }

        public void ReadD2R(BitwiseBinaryReader mainReader)
        {
            menuAppearanceD2R = mainReader.Read<byte[]>(OtherOffsets.OFFSET_D2R_CHARACTER_MENU_APPEARANCE) ?? throw new Exception("Unable to read D2S Character Menu Appearance");
        }

        public bool Write(BitwiseBinaryWriter writer)
        {
            if (writer.GetBytes().Length != OtherOffsets.OFFSET_CHARACTER_MENU_APPEARANCE.Offset)
                return false;

           writer.WriteBits(menuAppearance.ToBits());
            return true;
        }

        public bool WriteD2S(BitwiseBinaryWriter writer)
        {
            if(writer.GetBytes().Length != OtherOffsets.OFFSET_D2R_CHARACTER_MENU_APPEARANCE.Offset)
            {
                if (writer.GetBytes().Length + 28 != OtherOffsets.OFFSET_D2R_CHARACTER_MENU_APPEARANCE.Offset)
                    return false;

                writer.WriteVoidBits(28 * 8);
            }

           writer.WriteBits(menuAppearanceD2R.ToBits());

            return true;
        }
    }
}
