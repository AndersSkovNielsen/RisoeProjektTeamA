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
        public int StationsId { get; set; }
        public ObservableCollection<Udstyr> Udstyrsliste { get; set; }


        public Station(string navn, int nr)
        {
            Navn = navn;
            StationsId = nr;
            Udstyrsliste = null;
        }

        public Station()
        {
            
        }
        public Station(Station station)
        {
            Navn=station.Navn;
            StationsId=station.StationsId;
            Udstyrsliste = station.Udstyrsliste;
        }

        public override string ToString()
        {
            return $"Navn: {Navn}, Nr: {StationsId}";
        }
    }
}
