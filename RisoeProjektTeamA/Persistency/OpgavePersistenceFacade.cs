using ModelLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString()); //hvordan får vi denne besked ud i View? 
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
                    Console.WriteLine(ex.ToString()); //samme problem som hentalleopgaver  
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
