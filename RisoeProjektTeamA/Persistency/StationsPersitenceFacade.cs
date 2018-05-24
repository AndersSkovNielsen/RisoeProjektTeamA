using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using Newtonsoft.Json;
using RisoeProjektTeamA.View;

namespace RisoeProjektTeamA.Persistency
{
    public class StationsPersitenceFacade
    {
        private string Uri = "http://localhost:59327/api/Stations/"
            ; // URL til din REST (rest-ande-easj.azurewebsites.net (ikke testet))

        public List<Station> HentAlleStationer()
        {

            using (HttpClient client = new HttpClient())
            {
                List<Station> StationsListe = new List<Station>();
                try
                {
                    string jsonStr = client.GetStringAsync(Uri).Result;
                    // info fra body

                    StationsListe = JsonConvert.DeserializeObject<List<Station>>(jsonStr);
                }
                catch (Exception x)
                {
                    MessageDialogHandler.Show("Fejl i hetning", "Der er sket en fejl under kontakt med Databasen.");
                }
                return StationsListe;
            }
        }


        public Station HentEnStation(int nr)
        {

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + nr).Result; // info fra body
                Station station = new Station();
                try
                {
                    station = JsonConvert.DeserializeObject<Station>(jsonStr);
                }
                catch (Exception ex)
                {
                    MessageDialogHandler.Show("Fejl i hetning", "Der er sket en fejl under kontakt med Databasen.");
                }
                return station;
            }
        }

        public bool IndsætStation(Station station)
        {

            String json = JsonConvert.SerializeObject(station);
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

        public bool OpdaterEnStation(int nr, Station station)
        {


            String json = JsonConvert.SerializeObject(station);
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

        public Station SletStation(int nr)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.DeleteAsync(Uri + nr).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    Station station = JsonConvert.DeserializeObject<Station>(resultStr);
                    return station;
                }
            }

            return null;
        }
    }
}
