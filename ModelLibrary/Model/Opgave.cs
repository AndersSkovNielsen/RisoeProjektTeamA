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

        public Opgave()
        {
            Udstyr = new Udstyr();
        }
        
        public Opgave(string beskrivelse, StatusType status, int ventetid)
        {
            Beskrivelse = beskrivelse;
            Status = status;
            VentetidIDage = ventetid;
            Udstyr = new Udstyr();
        }

        public Opgave(Opgave opgave)
        {
            ID = opgave.ID;
            Beskrivelse = opgave.Beskrivelse;
            Status = opgave.Status;
            VentetidIDage = opgave.VentetidIDage;

            if (opgave.Udstyr == null)
            {
                Udstyr = new Udstyr();
            }
            else
                Udstyr = opgave.Udstyr;
        }

        public Opgave(int id, string beskrivelse, StatusType status, int ventetid, Udstyr udstyr)
        {
            ID = id;
            Beskrivelse = beskrivelse;
            Status = status;
            Udstyr = udstyr;
            VentetidIDage = ventetid;
        }

        public override string ToString()
        {
            return $"Opgave #{ID}: {Beskrivelse}";
        }

        //3. iterations property
        public Udstyr Udstyr { get; set; }
    }
}
