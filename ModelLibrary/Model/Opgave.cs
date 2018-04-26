using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class Opgave
    {
        public Udstyr Udstyr { get; set; }
        public bool Løst { get; set; }
        public int VentetidIDage { get; set; }
        public string Beskrivelse { get; set; }
        public enum Type { Regulær, Særlig }
        public enum Prioritet { Lav, Mellem, Høj, Kritisk }


        public Opgave(Udstyr udstyr, bool løst, int ventetidIDage, string beskrivelse)
        {
            Udstyr = udstyr;
            Løst = løst;
            VentetidIDage = ventetidIDage;
            Beskrivelse = beskrivelse;
        }

        ////private async void dagPasseret()
        //{
        //    //hvordan skriver registrer vi datoskift?

        //    ;

        //}

        protected virtual void StigPrioritet()
        {
            ;
        }

    }
}
