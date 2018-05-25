using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public enum StatusType { Løst, IkkeLøst, Fejlet }

    public class Opgave
    {
        public int ID { get; set; }
        public StatusType Status { get; set; }
        public int VentetidIDage { get; set; }
        public string Beskrivelse { get; set; }
        
        //3. iterations property
        public Udstyr Udstyr { get; set; }

        //Senere udvidelser
        //public enum Type { Regulær, Særlig }
        //public Udstyr Udstyr { get; set; }
        //public enum Prioritet { Lav, Mellem, Høj, Kritisk }

        public Opgave()
        {

        }

        //Test constructor af simpel opgave til 1. iteration
        public Opgave(int id, string beskrivelse, StatusType status, int ventetid, Udstyr udstyr)
        {
            ID = id;
            Beskrivelse = beskrivelse;
            Status = status;
            Udstyr = udstyr;
            VentetidIDage = ventetid;
        }
        
        public Opgave(string beskrivelse, StatusType status, int ventetid)
        {
            Beskrivelse = beskrivelse;
            Status = status;
            VentetidIDage = ventetid;
        }

        public Opgave(Opgave opgave)
        {
            ID = opgave.ID;
            Beskrivelse = opgave.Beskrivelse;
            Status = opgave.Status;
            VentetidIDage = opgave.VentetidIDage;
            Udstyr = opgave.Udstyr;
        }

        //Constructor til senere brug med Udstyr udvidelse
        //public Opgave(Udstyr udstyr, string status, int ventetidIDage, string beskrivelse)
        //{
        //    Udstyr = udstyr;
        //    Status = status;
        //    VentetidIDage = ventetidIDage;
        //    Beskrivelse = beskrivelse;
        //}

        //private async void dagPasseret()
        //{
        //    //hvordan skriver registrer vi datoskift?

        //    ;

        //}

        //protected virtual void StigPrioritet()
        //{
        //    ;
        //}

        public override string ToString()
        {
            return $"Opgave {ID}:\n{Beskrivelse} (Udføres hver {VentetidIDage}. dag)\nStatus: {Status}\n";
        }
    }
}
