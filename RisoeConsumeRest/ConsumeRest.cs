using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using Newtonsoft.Json;

namespace RisoeConsumeRest
{
    class ConsumeRest
    {
        private string Uri = "http://localhost:59327/api/Opgave/";

        public void Test()
        {
            Console.WriteLine("test af rest service");
            //---------------------------------------(test af HentAlleOpgaver())
            Console.WriteLine("test af HentalleOpgaver");
            Console.WriteLine("her bør være en komplet liste af alle nuværene opgaver");
            Console.WriteLine("");
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri).Result; // info fra body
                List<Opgave> opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr); //<=== convertere fra json sprog

                foreach (var o in opgaveListe) //<=== udskriver opgaven hentet fra Databasen.
                {
                    Console.WriteLine(o);
                }
                 
            }
            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------(test af HentEnOpgave())
            Console.Clear();
            Console.WriteLine("Test af HentEnOpgave");
            Console.WriteLine("Her bør være en enkelt opgave. Den første.");
            Console.WriteLine("");

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + 1).Result; // info fra body
                Opgave opgave = JsonConvert.DeserializeObject<Opgave>(jsonStr); //<=== converter fra json sprog
                Console.WriteLine(opgave); //<=== Skriver opgaven ud der er hentet fra databasen
            }

            Console.WriteLine("");
            Console.ReadKey();
            //---------------------------------------(test af IndsætOpgave())
            Console.Clear();
            Console.WriteLine("Test af Indsætopgave");
            Console.WriteLine("Her indsættets en ny opgave, opgave 10, som så bliver vist bagefter.");
            Console.WriteLine("");
            Opgave testOpgave10 = new Opgave(10, "En test Opagve", StatusType.IkkeLøst , 2);
            String json = JsonConvert.SerializeObject(testOpgave10);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.PostAsync(Uri, content).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    bool res = JsonConvert.DeserializeObject<bool>(resultStr);
                    foreach (var o in HentAlleOpgaver())
                    {
                        Console.WriteLine(o);
                    }
                }

            }

            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------(Test af OpdaterOpgave())
            Console.Clear();
            Console.WriteLine("Test af opdatering af en opgave (opgave 10 lavet tidligere)");
            Console.WriteLine("Her opdateres opgave 10, og bliver så skrevet ud.");
            Console.WriteLine("");
            testOpgave10 = new Opgave(10, "Test af Opdatering", StatusType.Løst, 5);
            json = JsonConvert.SerializeObject(testOpgave10);
            content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.PutAsync(Uri + 10, content).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    bool res = JsonConvert.DeserializeObject<bool>(resultStr);
                    foreach (var o in HentAlleOpgaver())
                    {
                        Console.WriteLine(o);
                    }
                }
            }
            Console.WriteLine("");
            Console.ReadKey();
            //---------------------------------------(test af SletOpgave())
            Console.Clear();
            Console.WriteLine("Test af Sletning af en opgave.(Opgave 10)");
            Console.WriteLine("Her slettes opgave 10, hele listen bliver så vist bagefter");
            Console.WriteLine("");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resultMessage = client.DeleteAsync(Uri + 10).Result;

                if (resultMessage.IsSuccessStatusCode)
                {
                    string resultStr = resultMessage.Content.ReadAsStringAsync().Result;
                    Opgave opgave = JsonConvert.DeserializeObject<Opgave>(resultStr);
                   
                    
                }
                foreach (var o in HentAlleOpgaver())
                {
                    Console.WriteLine(o);
                }
            }


            Console.WriteLine("");
            Console.ReadKey();
            //---------------------------------------


        }

        public List<Opgave> HentAlleOpgaver()
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri).Result; // info fra body
                List<Opgave> opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr);
                return opgaveListe;
            }
        }
        public Opgave HentEnOpgave(int nr)
        {
            //Eksempel på DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr;

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + nr).Result; // info fra body
                Opgave opgave = JsonConvert.DeserializeObject<Opgave>(jsonStr);
                return opgave;
            }
        }

        public bool IndsætOpgave(Opgave opgave)
        {
            //Eksempel på DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/";

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
            //Eksempel på DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr;

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
            //Eksempel på DRY
            //String OpgaveUri = "http://localhost:59327/api/Opgave/" + nr;

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
