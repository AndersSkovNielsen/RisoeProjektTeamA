using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class Station
    {
        public string Navn { get; set; }
        public int Nr { get; set; }
        public ObservableCollection<Udstyr> Udstyrsliste { get; set; }


        public Station(string navn, int nr, ObservableCollection<Udstyr> udstyrsliste)
        {
            Navn = navn;
            Nr = nr;
            Udstyrsliste = udstyrsliste;
        }
    }
}
