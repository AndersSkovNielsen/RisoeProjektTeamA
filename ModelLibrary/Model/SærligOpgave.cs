using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    class SærligOpgave : Opgave
    {
        public SærligOpgave (Udstyr udstyr, bool løst, int ventetidIDage, string beskrivelse):base ( udstyr,  løst,  ventetidIDage,  beskrivelse)
        {
            Udstyr = udstyr;
            Løst = løst;
            VentetidIDage = ventetidIDage;
            Beskrivelse = beskrivelse;
        }
    }
}
