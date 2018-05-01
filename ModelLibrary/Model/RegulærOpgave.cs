using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class RegulærOpgave : Opgave
    {
        public int IntervalIDage { get; set; }

        public RegulærOpgave() : base()
        {

        }
        //Constructor til senere brug ved udvidelse med arv
        //public RegulærOpgave(Udstyr udstyr, string status, int ventetidIDage, string beskrivelse, int intervalIDage) : base(udstyr, status, ventetidIDage, beskrivelse)
        //{
        //    IntervalIDage = intervalIDage;
        //}
    }
}
