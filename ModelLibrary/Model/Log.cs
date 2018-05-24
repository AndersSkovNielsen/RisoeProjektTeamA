using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class Log
    { //classen skal slettes
      public string Emne { get; set; }
        public DateTime Dato { get; set; }
        public string Tekniker { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
    }
}
