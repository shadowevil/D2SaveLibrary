using D2SLib2.BinaryHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2
{
    public static class Logger
    {
        public static string LogPath { get; set; } = string.Empty;
        private static Log? log;

        public static void WriteLine(int bytePosition, int bitPosition, string str)
        {
            if (log == null) log = new Log(LogPath);
            if (log.fs == null) throw new Exception("Log file stream was null");
            if(log.fs.BaseStream.CanWrite)
            {
                string combinedPos = $"{bytePosition}:{bitPosition}";
                string paddedCombinedPos = combinedPos.PadRight(22, ' ');
                log.fs.WriteLine($"[{paddedCombinedPos}]: {str}");
                Console.WriteLine($"[{paddedCombinedPos}]: {str}");
            }
        }

        public static void WriteLine(int bytePosition, int bitPosition, int newBitPosition, string str)
        {
            if (log == null) log = new Log(LogPath);
            if (log.fs == null) throw new Exception("Log file stream was null");
            if (log.fs.BaseStream.CanWrite)
            {
                string combinedPos = $"{bytePosition}:{bitPosition}:{newBitPosition}[{bitPosition-newBitPosition}]";
                string paddedCombinedPos = combinedPos.PadRight(22, ' ');
                log.fs.WriteLine($"[{paddedCombinedPos}]: {str}");
                Console.WriteLine($"[{paddedCombinedPos}]: {str}");
            }
        }

        public static void WriteSection(BitwiseBinaryReader br, int bitOffset, string str)
        {
            if (log == null) log = new Log(LogPath);
            if (log.fs == null) throw new Exception("Log file stream was null");
            if (log.fs.BaseStream.CanWrite)
            {
                string combinedPos = $"{br.bytePosition - (bitOffset / 8)}:{br.bitPosition - bitOffset}:{br.bitPosition}[{br.bitPosition - (br.bitPosition - bitOffset)}]";
                string paddedCombinedPos = combinedPos.PadRight(22, ' ');
                log.fs.WriteLine($"[{paddedCombinedPos}]: \t{str}");
            }
        }

        public static void WriteBeginSection(BitwiseBinaryReader br, string str)
        {
            if (log == null) log = new Log(LogPath);
            if (log.fs == null) throw new Exception("Log file stream was null");
            if (log.fs.BaseStream.CanWrite)
            {
                string combinedPos = $"{br.bytePosition}:{br.bitPosition}";
                string paddedCombinedPos = combinedPos.PadRight(22, ' ');
                log.fs.WriteLine($"[{paddedCombinedPos}]: {str}");
            }
        }

        public static void WriteEndSection(BitwiseBinaryReader br, string str)
        {
            WriteBeginSection(br, str);
        }

        public static void WriteHeader(string str)
        {
            if (log == null) log = new Log(LogPath);
            if (log.fs == null) throw new Exception("Log file stream was null");
            if (log.fs.BaseStream.CanWrite)
            {
                log.fs.WriteLine(str);
            }
        }

        public static void Close()
        {
            if (log == null) return;
            if (log.fs == null) return;

            log.fs.Close();
            log.fs.Dispose();
            log.fs = null;
            log = null;
        }
    }

    public class Log
    {
        public string LogPath { get; private set; } = string.Empty;
        public StreamWriter? fs = null;

        public Log(string logpath)
        {
            LogPath = logpath;
            OpenLog();
        }

        public void OpenLog()
        {
            fs = new StreamWriter(LogPath, false);
        }

        public void CloseLog()
        {
            if (fs == null) return;
            fs.Close();
        }
    }
}
