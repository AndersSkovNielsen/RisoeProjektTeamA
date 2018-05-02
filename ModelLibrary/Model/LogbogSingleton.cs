﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class LogbogSingleton
    {
        public static LogbogSingleton _instance;
        public ObservableCollection<Opgave> OpgaveListe { get; set; }
        public ObservableCollection<Log> Logliste { get; set; }
        public ObservableCollection<Bruger> Brugerliste { get; set; }
        public ObservableCollection<Station> StationsListe { get; set; }
        public bool Status { get; set; }



        public static LogbogSingleton Instance()
        {
            return _instance ?? (_instance = new LogbogSingleton());
        }

        private LogbogSingleton()
        {
            OpgaveListe = new ObservableCollection<Opgave>();
            Logliste = new ObservableCollection<Log>();
        }

        private void sorterOpgEfterPrio(ObservableCollection<Opgave> OpgListe)
        {
            OpgListe = OpgaveListe;
         
            for (int i=0; i<OpgListe.Count;i++ )
            {
               //if (typeof(OpgListe[i].Opgave.Prioritet.)>(OpgListe[i-1].Opgave.Prioritet)))// Hvorfor er denne sætning ulovlig?
            }
        }

        public void UpdateOpgave(Opgave Selectedopgave)
        {
            
        }
    }
}