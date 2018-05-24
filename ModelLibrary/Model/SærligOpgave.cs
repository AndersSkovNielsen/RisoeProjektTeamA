using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    class SærligOpgave : Opgave
    { // classen skal slettes
        public SærligOpgave() : base()
        {

        }

        //Constructor til senere brug ved udvidelse med arv
        //public SærligOpgave (Udstyr udstyr, string status, int ventetidIDage, string beskrivelse):base ( udstyr,  status,  ventetidIDage,  beskrivelse)
        //{
        //    Udstyr = udstyr;
        //    Status = status;
        //    VentetidIDage = ventetidIDage;
        //    Beskrivelse = beskrivelse;
        //}
    }
}
