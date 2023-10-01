using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2SLib2.BinaryHandler
{
    public class InvalidItemException : Exception
    {
        public InvalidItemException() : base() { }

        public InvalidItemException(string message) : base(message) { }

        public InvalidItemException(string message, Exception inner) : base(message, inner) { }
    }
}
