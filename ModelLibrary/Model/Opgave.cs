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
        public int ID { get; set; }
        public enum Status {Løst, IkkeLøst, Fejlet}

        //Status som en String
        public string StatusStr { get; set; }

        public int VentetidIDage { get; set; }
        public string Beskrivelse { get; set; }
        //public enum Type { Regulær, Særlig }
        //public Udstyr Udstyr { get; set; }
        //public enum Prioritet { Lav, Mellem, Høj, Kritisk }

        public Opgave()
        {

        }

        //Test constructor af simpel opgave til 1. iteration
        //Constructor hvor Status er givet med en enum, som bliver tjekket og dernæst sat til en string
        public Opgave(int id, string beskrivelse, Status status, int ventetid)
        {
            ID = id;
            Beskrivelse = beskrivelse;
            VentetidIDage = ventetid;

            if (status == Status.Fejlet) StatusStr = status.ToString();
            else if (status == Status.IkkeLøst) StatusStr = status.ToString();
            else if (status == Status.Løst) StatusStr = status.ToString();
            else throw new Exception("WeaponType does not exist");
        }

        //Constructor hvor Status er givet med en enum, som bliver tjekket og dernæst sat til en string
        public Opgave(string beskrivelse, Status status, int ventetid)
        {
            Beskrivelse = beskrivelse;
            VentetidIDage = ventetid;

            if (status == Status.Fejlet) StatusStr = status.ToString();
            else if (status == Status.IkkeLøst) StatusStr = status.ToString();
            else if (status == Status.Løst) StatusStr = status.ToString();
            else throw new Exception("WeaponType does not exist");
        }

        //Constructor hvor Status er givet med en string
        public Opgave(int id, string beskrivelse, string status, int ventetid)
        {
            ID = id;
            Beskrivelse = beskrivelse;
            StatusStr = status;
            VentetidIDage = ventetid;
        }

        //Constructor hvor Status er givet med en string
        public Opgave(string beskrivelse, string status, int ventetid)
        {
            Beskrivelse = beskrivelse;
            StatusStr = status;
            VentetidIDage = ventetid;
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
            return $"Opgave {ID}:\n{Beskrivelse} (Udføres hver {VentetidIDage}. dag)\n"/* + "Status: {Status}\n"*/; //Status i ToString virker ikke med enum, måske
        }
    }
}
