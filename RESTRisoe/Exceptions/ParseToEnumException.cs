using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary.Model;


namespace RESTRisoe.Exceptions
{
    public class ParseToEnumException : Exception
    {
        private static Opgave _selectedOpgave;
        public static string MessageParse =
                $"Fejl under hentning af OpgaveStatus ved hentningning fra database for opgave nr {_selectedOpgave.ID} "
            ; 


        public override string ToString()
        {
            return MessageParse;
        }

        public ParseToEnumException(Opgave selectedOpgave)
        {
            _selectedOpgave = selectedOpgave;
        }
    }
}