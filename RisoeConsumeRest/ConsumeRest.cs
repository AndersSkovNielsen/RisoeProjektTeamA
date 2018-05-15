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
            Console.WriteLine("Test af REST service");
            //---------------------------------------(Test af HentAlleOpgaver())
            Console.WriteLine("Test af hentning af alle opgaver");
            Console.WriteLine("Her bør være en komplet liste af alle nuværende opgaver.");
            Console.WriteLine("");

            List<Opgave> opgaveListe = HentAlleOpgaver();

            foreach (var o in opgaveListe) //<=== udskriver opgaven hentet fra Databasen.
            {
                Console.WriteLine(o);
            }

            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------(test af HentEnOpgave())
            Console.Clear();
            Console.WriteLine("Test af hentning af en enkelt opgave (Opgave 1).");
            Console.WriteLine("Her bør være opgave 1.");
            Console.WriteLine("");

            Console.WriteLine(HentEnOpgave(1));

            Console.WriteLine("");
            Console.ReadKey();
            //---------------------------------------(test af IndsætOpgave())
            Console.Clear();
            Console.WriteLine("Test af indsætning af en opgave (Opgave 10).");
            Console.WriteLine("Her indsættets en ny opgave, opgave 10, som så bliver vist bagefter.");
            Console.WriteLine("");
            Opgave testOpgave10 = new Opgave(10, "En test Opgave", StatusType.IkkeLøst, 2);

            IndsætOpgave(testOpgave10);

            Console.WriteLine(HentEnOpgave(10));

            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------(Test af OpdaterOpgave())
            Console.Clear();
            Console.WriteLine("Test af opdatering af en opgave (Opgave 10).");
            Console.WriteLine("Her ser vi den nuværende opgave 10, som så bliver opdateret og skrevet ud igen.");
            Console.WriteLine("");
            testOpgave10 = new Opgave(10, "Test af Opdatering", StatusType.Løst, 5);

            Console.WriteLine("Nuværende:\n" + HentEnOpgave(10));

            OpdaterEnOpgave(10, testOpgave10);

            Console.WriteLine("Opdateret:\n" + HentEnOpgave(10));

            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------(test af SletOpgave())
            Console.Clear();
            Console.WriteLine("Test af sletning af en opgave (Opgave 10).");
            Console.WriteLine("Her slettes opgave 10 og hele listen bliver så vist bagefter.");
            Console.WriteLine("");

            SletOpgave(10);

            foreach (var o in HentAlleOpgaver())
            {
                Console.WriteLine(o);
            }

            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------

        }

        //Metoder brugt til test
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
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + nr).Result; // info fra body
                Opgave opgave = JsonConvert.DeserializeObject<Opgave>(jsonStr);
                return opgave;
            }
        }

        public bool IndsætOpgave(Opgave opgave)
        {
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
