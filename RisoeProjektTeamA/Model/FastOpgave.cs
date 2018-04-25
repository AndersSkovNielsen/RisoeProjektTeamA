using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisoeProjektTeamA.Model
{
    class FastOpgave:Opgave
    {
        public int IntervalIDage { get; set; }


        public FastOpgave(int intervalIDage)
        {
            IntervalIDage = intervalIDage;
        }
    }
}
