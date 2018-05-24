using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Exceptions
{
    public class ParseToEnumException : Exception
    {
        private static int _exId;
        public string MessageParse =
            $"Fejl under ved Opgavestatus eller Udstyr-type ved hentning fra database for opgave/Udstyr nr {_exId}";


        public override string ToString()
        {
            return MessageParse;
        }

        public ParseToEnumException(int exId)
        {
            _exId = exId;
        }
    }
}
