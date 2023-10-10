using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.Structure.Quests
{
    public class QuestBook
    {
        public string Signature = string.Empty;
        public UInt32 Version = UInt32.MaxValue;
        public UInt16 Size = UInt16.MaxValue;

        public QuestDifficulty? Normal { get; set; } = null;
        public QuestDifficulty? Nightmare { get; set; } = null;
        public QuestDifficulty? Hell { get; set; } = null;

        public QuestBook() { }

        public static QuestBook Read(BitwiseBinaryReader mainReader)
        {
            QuestBook questBook = new QuestBook();

            mainReader.SetBytePosition(QuestOffsets.OFFSET_SIGNATURE.Offset);
            if (mainReader.Peek<string>(QuestOffsets.OFFSET_SIGNATURE) != QuestOffsets.OFFSET_SIGNATURE.Signature)
                throw new OffsetException("Unable to read quest signature, corrupt file?");
            questBook.Signature = mainReader.ReadBits(QuestOffsets.OFFSET_SIGNATURE.BitLength).ToString((uint)QuestOffsets.OFFSET_SIGNATURE.BitLength, Endianness.BigEndian);

            questBook.Version = mainReader.Read<UInt32>(QuestOffsets.OFFSET_VERSION);
            questBook.Size = mainReader.Read<UInt16>(QuestOffsets.OFFSET_SIZE);

            questBook.Normal = QuestDifficulty.Read(mainReader, QuestOffsets.OFFSET_NORMAL.Offset);
            questBook.Nightmare = QuestDifficulty.Read(mainReader, QuestOffsets.OFFSET_NIGHTMARE.Offset);
            questBook.Hell = QuestDifficulty.Read(mainReader, QuestOffsets.OFFSET_HELL.Offset);

            return questBook;
        }

        public bool WriteQuests(BitwiseBinaryWriter writer)
        {
            if(writer.GetBytes().Length != QuestOffsets.OFFSET_SIGNATURE.Offset)
            {
                if (writer.GetBytes().Length + 52 != QuestOffsets.OFFSET_SIGNATURE.Offset)
                    return false;

                writer.WriteVoidBits(52 * 8);
            }
           writer.WriteBits(Signature.ToBits());

            if (writer.GetBytes().Length != QuestOffsets.OFFSET_VERSION.Offset)
                return false;
           writer.WriteBits(Version.ToBits((uint)QuestOffsets.OFFSET_VERSION.BitLength));

            if (writer.GetBytes().Length != QuestOffsets.OFFSET_SIZE.Offset)
                return false;
           writer.WriteBits(Size.ToBits((uint)QuestOffsets.OFFSET_SIZE.BitLength));

            Normal!.Write(writer, QuestOffsets.OFFSET_NORMAL);
            Nightmare!.Write(writer, QuestOffsets.OFFSET_NIGHTMARE);
            Hell!.Write(writer, QuestOffsets.OFFSET_HELL);

            return true;
        }
    }
}
