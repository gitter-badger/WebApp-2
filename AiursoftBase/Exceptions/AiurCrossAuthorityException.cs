using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Exceptions
{
    public class AiurCrossAuthorityException : Exception
    {
        public AiurCrossAuthorityException() { }
        public AiurCrossAuthorityException(string message) : base(message) { }
    }
}
