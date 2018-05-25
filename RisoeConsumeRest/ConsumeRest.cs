using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Exceptions;
using ModelLibrary.Model;
using Newtonsoft.Json;

namespace RisoeConsumeRest
{
    class ConsumeRest
    {
        private string Uri = "http://localhost:59327/api/Opgave/";
        //
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
            Opgave testOpgave10 = new Opgave(10,"en testopgave",StatusType.IkkeLøst,2,HentUdstyrFraId(2));

            IndsætOpgave(testOpgave10);

            Console.WriteLine(HentEnOpgave(10));

            Console.WriteLine("");
            Console.ReadKey();

            //---------------------------------------(Test af OpdaterOpgave())
            Console.Clear();
            Console.WriteLine("Test af opdatering af en opgave (Opgave 10).");
            Console.WriteLine("Her ser vi den nuværende opgave 10, som så bliver opdateret og skrevet ud igen.");
            Console.WriteLine("");
            testOpgave10 = new Opgave(10,"Test af opdatering",StatusType.IkkeLøst,5,HentUdstyrFraId(3));

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
                List<Opgave> opgaveListe = new List<Opgave>();
                string jsonStr = client.GetStringAsync(Uri).Result;
                opgaveListe = JsonConvert.DeserializeObject<List<Opgave>>(jsonStr);
                
                return opgaveListe;
            }
        }
        
        public Opgave HentEnOpgave(int nr)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = client.GetStringAsync(Uri + nr).Result; // info fra body
                Opgave opgave = new Opgave();

                opgave = JsonConvert.DeserializeObject<Opgave>(jsonStr);

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

        //Hjælp til at hente udstyr
        private String connectionString =
            @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        ;

        private String queryStringFromID = "select * from RisoeUdstyr where UdstyrId = @UdstyrId";

        private String queryStringFromID2 = "select * from RisoeStation where StationNr = @StationNr";


        public Udstyr HentUdstyrFraId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@UdstyrId", id);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return ReadUdstyr(reader);
                }
            }
            return null; //Kan vi skrive dette?
        }

        private Udstyr ReadUdstyr(SqlDataReader reader) //denne metode skal justeres så den tager fat de rigtige steder i DB
        {
            int udstyrId = reader.GetInt32(0);
            int stationId = reader.GetInt32(1);

            Station station = HentStationFraId(stationId);

            uType type = uType.Filter;
            try
            {
                string typeStr = reader.GetString(2);
                type = (uType)Enum.Parse(typeof(uType), typeStr);

                CheckEnumParseU(type, udstyrId);
            }
            catch (ParseToEnumException)
            {
                ParseToEnumException parseFailEx = new ParseToEnumException(udstyrId);
                string log = parseFailEx.ToString();

            }

            DateTime instDato = reader.GetDateTime(3);
            string beskrivelse = reader.GetString(4);

            return new Udstyr(udstyrId, instDato, beskrivelse, type, station);
        }

        private void CheckEnumParseU(uType checkType, int checkId)
        {
            if (!(checkType == uType.Filter ||
                  checkType == uType.Termometer ||
                  checkType == uType.Lufttrykmåler))
            {
                int exId = checkId;
                throw new ParseToEnumException(exId);
            }
        }

        public Station HentStationFraId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID2, connection);
                command.Parameters.AddWithValue("@StationNr", id);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return ReadStation(reader);
                }
            }
            return null; //Kan vi skrive dette?
        }

        private Station ReadStation(SqlDataReader reader)
        {
            int stationNr = reader.GetInt32(0);
            String navn = reader.GetString(1);

            return new Station(navn, stationNr);
        }
    }
}
