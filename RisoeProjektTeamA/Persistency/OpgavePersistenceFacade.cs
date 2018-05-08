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
        public List<Opgave> HentAlleOpgaver()
        {
            String OpgaveUri = "http://localhost:59327/api/Opgave"; // URL til din REST

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(OpgaveUri).Result; // info fra body
                List<Opgave> opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr);
                return opgaveListe;
            }
        }

        public Opgave HentEnOpgave(int nr)
        {
            String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr; // URL til din REST

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(OpgaveUri).Result; // info fra body
                Opgave opgave = JsonConvert.DeserializeObject<Opgave>(jsonStr);
                return opgave;
            }
        }
        
        //rest-ande-easj.azurewebsites.net
        public bool IndsætOpgave(Opgave opgave)
        {
            String OpgaveUri = "http://localhost:59327/api/Opgave"; // URL til din REST

            String json = JsonConvert.SerializeObject(opgave);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.PostAsync(OpgaveUri, content).Result;

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
            String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr; // URL til din REST

            String json = JsonConvert.SerializeObject(opgave);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.PutAsync(OpgaveUri, content).Result;

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
            String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr; // URL til din REST

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.DeleteAsync(OpgaveUri).Result;

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
