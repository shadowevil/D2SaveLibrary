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
            mainReader.SetBytePosition(OtherOffsets.OFFSET_CHARACTER_MENU_APPEARANCE.Offset);
            menuAppearance = mainReader.ReadBytes(OtherOffsets.OFFSET_CHARACTER_MENU_APPEARANCE.ByteLength);
        }

        public void ReadD2R(BitwiseBinaryReader mainReader)
        {
            mainReader.SetBytePosition(OtherOffsets.OFFSET_D2R_CHARACTER_MENU_APPEARANCE.Offset);
            menuAppearanceD2R = mainReader.ReadBytes(OtherOffsets.OFFSET_D2R_CHARACTER_MENU_APPEARANCE.ByteLength);
        }
    }
}
