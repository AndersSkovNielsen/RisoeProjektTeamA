﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Exceptions;
using ModelLibrary.Model;
using Newtonsoft.Json;
using RisoeProjektTeamA.View;

namespace RisoeProjektTeamA.Persistency
{
    public class BrugerPersistenceFacade
    {
        //The URI for connecting to the database
        private String Uri = "http://localhost:59327/api/Bruger/";

        //The HTTP client for making a connection
        private HttpClient client = new HttpClient();

        public List<Bruger> HentAlleBrugere()
        {
            using (client)
            {
                List<Bruger> brugerListe = new List<Bruger>();
                
                    string jsonStr = client.GetStringAsync(Uri).Result;
                    // info fra body
                    brugerListe = JsonConvert.DeserializeObject<List<Bruger>>(jsonStr);
                
                return brugerListe;
            }
        }

        public Bruger HentEnBruger(string initial)
        {
            using (client)
            {
                Bruger bruger = new Bruger();
                string jsonStr = client.GetStringAsync(Uri + initial).Result; // info fra body
                try
                {
                    bruger = JsonConvert.DeserializeObject<Bruger>(jsonStr);
                }
                catch (Exception ex)
                {
                    MessageDialogHandler.Show(ex.ToString(), "Status kan ikke hentes fra database.");
                }
                return bruger;
            }
        }

        public bool IndsætBruger(Bruger bruger)
        {
            String json = JsonConvert.SerializeObject(bruger);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (client)
            {
                HttpResponseMessage resultMessage = client.PostAsync(Uri, content).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    bool res = JsonConvert.DeserializeObject<bool>(resultStr);
                    return res;
                }
            }
            return false;
        }

        //public bool OpdaterEnBruger(int nr, Bruger bruger)
        //{
        //    String json = JsonConvert.SerializeObject(bruger);
        //    StringContent content = new StringContent(json);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    using (client)
        //    {
        //        HttpResponseMessage resultMessage = client.PutAsync(Uri + nr, content).Result;

        //        if (resultMessage.IsSuccessStatusCode)
        //        {
        //            string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
        //            bool res = JsonConvert.DeserializeObject<bool>(resultStr);
        //            return res;
        //        }
        //    }
        //    return false;
        //}


        public Bruger SletBruger(string Initialer)
        {
            using (client)
            {
                HttpResponseMessage resultMessage = client.DeleteAsync(Uri + Initialer).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    Bruger bruger = JsonConvert.DeserializeObject<Bruger>(resultStr);
                    return bruger;
                }
            }
            return null;
        }









    }
}
