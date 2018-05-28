using ModelLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Exceptions;
using RisoeProjektTeamA.View;

namespace RisoeProjektTeamA.Persistency
{
    public class OpgavePersistenceFacade
    {
        private string Uri = "http://localhost:59327/api/Opgave/"; // URL til din REST (rest-ande-easj.azurewebsites.net (ikke testet))

        public List<Opgave> HentAlleOpgaver()
        {
            //Eksempel på brud med DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/";
            //slut på eksempel 
            using (HttpClient client = new HttpClient())
            {
                List<Opgave> opgaveListe = new List<Opgave>();
                try
                {
                    string jsonStr = client.GetStringAsync(Uri).Result;
                    // info fra body
                   opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr);
                }
                catch (ParseToEnumException ex)
                {
                    MessageDialogHandler.Show(ex.ToString(), "Status kan ikke hentes fra database.");
                }
                return opgaveListe;
            }
        }

        //Bliver ikke brugt i 3. iteration. Denne henter hele vejen igennem databasen.
        public List<Opgave> HentAlleOpgaverForUdstyr(int udstyrId)
        {
            using (HttpClient client=new HttpClient())
            {
                List<Opgave> opgaveListe = new List<Opgave>();
                string jsonStr = client.GetStringAsync(Uri+ "HentAlleOpgaverForUdstyr/" + udstyrId).Result;
                try
                {
                    opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr);
                }

                catch (ParseToEnumException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return opgaveListe;
            }
        }

        //3. iteration metode. Bruges til at hente opgaver og kun fokusere på UdstyrID.
        public List<Opgave> HentOpgaverForUdstyrID(int udstyrID)
        {
            using (HttpClient client = new HttpClient())
            {
                List<Opgave> opgaveListe = new List<Opgave>();
                string jsonStr = client.GetStringAsync(Uri + "HentUdstyrIDForOpgaver/" + udstyrID).Result;
                try
                {
                    opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr);
                }

                catch (ParseToEnumException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return opgaveListe;
            }
        }

        public Opgave HentEnOpgave(int nr)
        {
            //Eksempel på brud med DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr;
            //slut på eksempel

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + nr).Result; // info fra body
                Opgave opgave=new Opgave();
                try
                {
                    opgave = JsonConvert.DeserializeObject<Opgave>(jsonStr);
                }
                catch (Exception ex)
                {
                    MessageDialogHandler.Show(ex.ToString(), "Status kan ikke hentes fra database.");
                }
                return opgave;
            }
        }
        
        public bool IndsætOpgave(Opgave opgave)
        {
            //Eksempel på brud med DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/";
            //slut på eksempel

            String json = JsonConvert.SerializeObject(opgave);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
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

        public bool OpdaterEnOpgave(int nr, Opgave opgave)
        {
            //Eksempel på brud med DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr;
            //slut på eksempel

            String json = JsonConvert.SerializeObject(opgave);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.PutAsync(Uri + nr, content).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    bool res = JsonConvert.DeserializeObject<bool>(resultStr);
                    return res;
                }
            }

            return false;
        }

        public Opgave SletOpgave(int nr)
        {
            //Eksempel på brud med DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr;
            //slut på eksempel

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.DeleteAsync(Uri + nr).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    Opgave opgave = JsonConvert.DeserializeObject<Opgave>(resultStr);
                    return opgave;
                }
            }

            return null;
        }
    }
}
