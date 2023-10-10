using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2
{
    public static class Debugging
    {
        public static bool debuggingEnabled = false;

        public static void WriteBitPositionMessage<T>(Bit[] bits, T result)
        {
            if (!debuggingEnabled) return;

            StackTrace st = new StackTrace();
            StackFrame[] frames = st.GetFrames();

            string str = "";
            foreach (Bit bit in bits)
            {
                str += bit.ToString();
            }

            if (str.Length > 40) str = str.Substring(0, 40) + "...";

            Logger.WriteLine(BitwiseBinaryReader.instance!.bytePosition - (bits.Length / 8), BitwiseBinaryReader.instance!.bitPosition - bits.Length, BitwiseBinaryReader.instance!.bitPosition, str.PadRight(44) + " : " + result);
        }
    }
}
