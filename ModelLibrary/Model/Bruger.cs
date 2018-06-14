using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
     public class Bruger
    { //classen skal slettes


        //Navn og Type er ikke til denne iteration
        //public string FuldNavn { get; set; }
        //public enum Type {Tekniker, Admin }

        public string Initialer { get; set; }
        public string KodeOrd { get; set; }
        

        public Bruger(string initialer, string kodeOrd)
        {
            //FuldNavn = fuldNavn;
            Initialer = initialer;
            KodeOrd = kodeOrd;
        }

        public Bruger()
        {
            
        }

        public Bruger(Bruger bruger)
        {
            Initialer = bruger.Initialer;
            KodeOrd = bruger.KodeOrd;
        }

       
     }
}
