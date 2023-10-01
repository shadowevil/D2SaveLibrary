using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2
{
    public class OffsetException : Exception
    {
        public OffsetException() : base() { }

        public OffsetException(string message) : base(message) { }

        public OffsetException(string message, Exception inner) : base(message, inner) { }
    }
}
