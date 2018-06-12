using System;
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
    public class UdstyrPersistanceFacade
    {
        private string Uri = "http://localhost:59327/api/Udstyr/"; // URL til din REST (rest-ande-easj.azurewebsites.net (ikke testet))

        public List<Udstyr> HentAltUdstyr()
        {

            using (HttpClient client = new HttpClient())
            {
                List<Udstyr> UdstyrsListe = new List<Udstyr>();
                try
                {
                    string jsonStr = client.GetStringAsync(Uri).Result;
                        // info fra body

                    UdstyrsListe = JsonConvert.DeserializeObject<List<Udstyr>>(jsonStr);
                }
                catch (Exception x)
                {
                    MessageDialogHandler.Show("Fejl i hetning", "Der er sket en fejl under kontakt med Databasen.");
                }
                return UdstyrsListe;
            }
        }

        public List<Udstyr> HentAltUdstyrFraStation(int stationId)
        {
            using (HttpClient client=new HttpClient())
            {
                List<Udstyr> udstyrsListe = new List<Udstyr>();
                string jsonStr = client.GetStringAsync(Uri+ "HentAltUdstyrForStation/" +stationId).Result;
                try
                {
                    udstyrsListe = JsonConvert.DeserializeObject<List<Udstyr>>(jsonStr);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return udstyrsListe;
            }
        }

        //3. iteration metode. Bruges til at hente opgaver og kun fokusere på UdstyrID.
        public List<Udstyr> HentUdstyrForStationID(int stationID)
        {
            using (HttpClient client = new HttpClient())
            {
                List<Udstyr> udstyrListe = new List<Udstyr>();
                string jsonStr = client.GetStringAsync(Uri + "HentUdstyrForStationID/" + stationID).Result;
                try
                {
                    udstyrListe = JsonConvert.DeserializeObject<List<Udstyr>>(jsonStr);
                }

                catch (ParseToEnumException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return udstyrListe;
            }
        }

        public Udstyr HentEtUdstyr(int nr)
        {

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + nr).Result; // info fra body
                Udstyr udstyr = new Udstyr();
                try
                {
                    udstyr = JsonConvert.DeserializeObject<Udstyr>(jsonStr);
                }
                catch (Exception ex)
                {
                    MessageDialogHandler.Show( "Fejl i hetning", "Der er sket en fejl under kontakt med Databasen.");
                }
                return udstyr;
            }
        }
        
        public bool IndsætUdstyr(Udstyr udstyr)
        {
            String json = JsonConvert.SerializeObject(udstyr);
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

        public bool OpdaterEtUdstyr(int nr, Udstyr udstyr)
        {
            

            String json = JsonConvert.SerializeObject(udstyr);
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

        public Udstyr SletUdstyr(int nr)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.DeleteAsync(Uri + nr).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    Udstyr udstyr = JsonConvert.DeserializeObject<Udstyr>(resultStr);
                    return udstyr;
                }
            }

            return null;
        }
    }
}

