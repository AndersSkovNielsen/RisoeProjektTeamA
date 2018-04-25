﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace RisoeProjektTeamA.Model
{
    class Udstyr
    {
        public enum Type { type1, type2, type3, type4}
        public DateTime Installationsdato { get; set; }
        public DateTime NæsteTjekDato { get; set; }
        public DateTime TidtilNæsteTjek { get; set; }
        public string Beskrivelse { get; set; }

    }
}
