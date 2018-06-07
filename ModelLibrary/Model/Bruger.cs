using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
     public class Bruger
    { //classen skal slettes
        public string FuldNavn { get; set; }
        public string Initialer { get; set; }
        public string KodeOrd { get; set; }
        public enum Type {Tekniker, Admin }

        public Bruger(string fuldNavn, string initialer, string kodeOrd)
        {
            FuldNavn = fuldNavn;
            Initialer = initialer;
            KodeOrd = kodeOrd;
        }

        public Bruger()
        {
            
        }

    }
}
