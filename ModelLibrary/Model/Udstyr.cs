using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public enum uType
    {
        type1,
        type2,
        type3,
        type4,
        Filter,//tilføjet til test. skal slettes
        filter//tilføjet til test. skal slettes
    }

    public class Udstyr
    {
        public uType Type { get; set; }
        //Nyt prop til opdatering i dokumentation 22/05
        public int UdstyrId { get; set; }
        //slut på ny prop
        public DateTime Installationsdato { get; set; }
        //public DateTime NæsteTjekDato { get; set; }
        //public DateTime SidsteTjekDato { get; set; }
        public string Beskrivelse { get; set; }


        public Udstyr(int udstyrId, DateTime installationsdato, /*DateTime næsteTjekDato, DateTime sidsteTjek,*/ string beskrivelse, uType type)
        {
            Installationsdato = installationsdato;
            //NæsteTjekDato = næsteTjekDato;
            //SidsteTjekDato = sidsteTjek;
            Beskrivelse = beskrivelse;
            Type = type;

        }

        public Udstyr()
        {

        }

        public Udstyr(Udstyr udstyr)
        {
            Installationsdato = udstyr.Installationsdato;
            //NæsteTjekDato = udstyr.NæsteTjekDato;
            //SidsteTjekDato = udstyr.SidsteTjekDato;
            Beskrivelse = udstyr.Beskrivelse;
        }
    }
}
