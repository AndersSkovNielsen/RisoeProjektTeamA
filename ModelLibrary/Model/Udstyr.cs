using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class Udstyr
    {
        public enum Type
        {
            type1,
            type2,
            type3,
            type4
        }
        //Nyt prop til opdatering i dokumentation 22/05
        public int UdstyrId { get; set; }
        //slut på ny prop
        public DateTime Installationsdato { get; set; }
        public DateTime NæsteTjekDato { get; set; }
        public DateTime TidtilNæsteTjek { get; set; }
        public string Beskrivelse { get; set; }


        public Udstyr(int udstyrId, DateTime installationsdato, DateTime næsteTjekDato, DateTime tidtilNæsteTjek, string beskrivelse)
        {
            Installationsdato = installationsdato;
            NæsteTjekDato = næsteTjekDato;
            TidtilNæsteTjek = tidtilNæsteTjek;
            Beskrivelse = beskrivelse;

        }

        public Udstyr()
        {

        }

        public Udstyr(Udstyr udstyr)
        {
            Installationsdato = udstyr.Installationsdato;
            NæsteTjekDato = udstyr.NæsteTjekDato;
            TidtilNæsteTjek = udstyr.TidtilNæsteTjek;
            Beskrivelse = udstyr.Beskrivelse;
        }
    }
}
