using ModelLibrary.Model;
using RisoeProjektTeamA.Persistency;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisoeProjektTeamA.Model
{
    public class LogbogSingleton
    {
        private static LogbogSingleton _instance = null;
        public ObservableCollection<Opgave> OpgaveListe { get; set; }

        //Gem til senere
        //public ObservableCollection<Log> Logliste { get; set; }
        //public ObservableCollection<Bruger> Brugerliste { get; set; }
        //public ObservableCollection<Station> StationsListe { get; set; }
        //public bool Status { get; set; }
        
        public OpgavePersistenceFacade Facade { get; set; }

        public void Add(Opgave opgave)
        {
            OpgaveListe.Add(opgave);
        }

        //Anders' Instance
        public static LogbogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogbogSingleton();
                }
                return _instance;
            }
        }

        //Frederik's Instance
        //public static LogbogSingleton Instance()
        //{
        //    return _instance ?? (_instance = new LogbogSingleton());
        //}

        private LogbogSingleton()
        {
            Facade = new OpgavePersistenceFacade();
            OpgaveListe = new ObservableCollection<Opgave>(Facade.HentAlleOpgaver());

            //Gem til senere
            //Logliste = new ObservableCollection<Log>();
        }

        
        //Senere iteration
        private void SorterOpgEfterPrio(ObservableCollection<Opgave> OpgListe)
        {
            OpgListe = OpgaveListe;

            for (int i = 0; i < OpgListe.Count; i++)
            {
                //if (typeof(OpgListe[i].Opgave.Prioritet.)>(OpgListe[i-1].Opgave.Prioritet)))// Hvorfor er denne sætning ulovlig?
            }
        }
    }
}
