﻿using ModelLibrary.Model;
using RisoeProjektTeamA.Persistency;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RisoeProjektTeamA.ViewModel;

namespace RisoeProjektTeamA.Model
{
    public class LogbogSingleton
    {
        private static LogbogSingleton _instance = new LogbogSingleton(); //Lazy initialization: _instance = null;
        public ObservableCollection<Opgave> OpgaveListe { get; set; }
        
        public OpgavePersistenceFacade OFacade { get; set; }
        
        public void AddO(Opgave opgave)
        {
            OpgaveListe.Add(opgave);
        }

        public static LogbogSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        //Lazy initialization properties

        //Anders' Instance
        //public static LogbogSingleton Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new LogbogSingleton();
        //        }
        //        return _instance;
        //    }
        //}

        //Frederik's Instance
        //public static LogbogSingleton Instance()
        //{
        //    return _instance ?? (_instance = new LogbogSingleton());
        //}

        private LogbogSingleton()
        {
            OFacade = new OpgavePersistenceFacade();
            OpgaveListe = new ObservableCollection<Opgave>();

            //3. iteration
            UFacade = new UdstyrPersistanceFacade();
            UdstyrsListe = new ObservableCollection<Udstyr>();
            SFacade = new StationsPersitenceFacade();
            StationsListe = new ObservableCollection<Station>();

            BFacade = new BrugerPersistenceFacade();
            Brugerliste = new ObservableCollection<Bruger>(BFacade.HentAlleBrugere());

            //Lange måde at få Listview til at virke fra starten:
            //Brugerliste = new ObservableCollection<Bruger>();

            //var brugere = BFacade.HentAlleBrugere();

            //foreach (var b in brugere)
            //{
            //    AddB(b);
            //}
        }

        //3. iteration
        public ObservableCollection<Udstyr> UdstyrsListe { get; set; }
        public ObservableCollection<Station> StationsListe { get; set; }

        public UdstyrPersistanceFacade UFacade { get; set; }
        public StationsPersitenceFacade SFacade { get; set; }

        public void AddU(Udstyr udstyr)
        {
            UdstyrsListe.Add(udstyr);
        }

        public void AddS(Station station)
        {
            StationsListe.Add(station);
        }

        //Kode skrevet efter rapportaflevering
        public BrugerPersistenceFacade BFacade { get; set; }
        public ObservableCollection<Bruger> Brugerliste { get; set; }

        public void AddB(Bruger bruger)
        {
            Brugerliste.Add(bruger);
        }
    }
}
