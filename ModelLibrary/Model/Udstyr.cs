using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public enum uType
    {
        Filter,
        Termometer,
        Lufttrykmåler,
    }

    public class Udstyr
    {
        public uType Type { get; set; }
        public int UdstyrId { get; set; }
        public DateTime Installationsdato { get; set; }
        public Station Station { get; set; }
        public ObservableCollection<Opgave> OpgaveListe { get; set; }
        public string Beskrivelse { get; set; }
        


        public Udstyr(int udstyrId, DateTime installationsdato, string beskrivelse, uType type, Station station)
        {
            UdstyrId = udstyrId;
            Installationsdato = installationsdato;
            Beskrivelse = beskrivelse;
            Type = type;
            OpgaveListe = null;
            Station = station;
        }

        public Udstyr()
        {

        }

        //Copy constructor
        public Udstyr(Udstyr udstyr)
        {
            UdstyrId = udstyr.UdstyrId;
            Installationsdato = udstyr.Installationsdato;
            Beskrivelse = udstyr.Beskrivelse;
            Type = udstyr.Type;
            OpgaveListe = udstyr.OpgaveListe;
            Station = udstyr.Station;
        }

        public override string ToString()
        {
            return $"{UdstyrId}: {Beskrivelse}";
        }
    }
}
