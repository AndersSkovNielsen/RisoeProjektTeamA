using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using ModelLibrary.Exceptions;

namespace RisoeConsumeDatabase
{
    class ConsumeDatabase
    {
        //This is the main running program for testing the code
        public void Main()
        {
            //Code below this line-----------------------------------------------------------------------------------------------------
            //Hent Opgave (test)
            Console.WriteLine("Test af hentning af alle opgave");
            Console.WriteLine("");

            List<Opgave> mineOpgaver = HentAlleOpgaver();

            foreach (var op in mineOpgaver)
            {
                Console.WriteLine(op);
            }

            Console.ReadKey();
            Console.Clear();

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ 
            //Hent en Opgave (test)
            Console.WriteLine("Test af hentning af specifikke opgaver (1 og 3)");
            Console.WriteLine("");

            Console.WriteLine(HentOpgaveFraId(1));
            Console.WriteLine(HentOpgaveFraId(3));

            Console.ReadKey();
            Console.Clear();

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //Indsæt en Opgave (test)
            Console.WriteLine("Test af indsætning af opgave");
            Console.WriteLine("");

            Opgave OP = new Opgave(10,"test",StatusType.IkkeLøst,1, HentUdstyrFraId(3));

            IndsætOpgave(OP);

            Console.WriteLine(HentOpgaveFraId(10));

            Console.ReadKey();
            Console.Clear();

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ 
            //Opdater Opgave (test)
            Console.WriteLine("Test af opdatering af opgave");
            Console.WriteLine("");

            Opgave NewOP = new Opgave(10,"test2",StatusType.Løst,2, HentUdstyrFraId(1));

            OpdaterOpgave(NewOP, 10);

            Console.WriteLine(HentOpgaveFraId(10));

            Console.ReadKey();
            Console.Clear();

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //Slet Opgave (Test)
            Console.WriteLine("Test af sletning af opgave");
            Console.WriteLine("");

            SletOpgave(10);

            List<Opgave> mineOpgaverNy = HentAlleOpgaver();

            foreach (var op in mineOpgaverNy)
            {
                Console.WriteLine(op);
            }

            //Code above this line---------------------------------------------------------------------------------------------------
        }

        //Below is all the code that should be tested, before getting put in the main rest service.
        private String connectionString = @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private String queryString = "select * from RisoeOpgave";
        private String queryStringFromID = "select * from RisoeOpgave where ID = @ID";
        private String insertSql = "insert into RisoeOpgave Values (@OpgaveID, @Beskrivelse, @Status, @Ventetid, @UdstyrID)";
        private String deleteSql = "delete from RisoeOpgave where ID = @ID";
        private String updateSql = "update RisoeOpgave " +
                                   "set ID = @OpgaveID, Beskrivelse = @Beskrivelse, Status = @Status, VentetidIDage = @Ventetid " +
                                   "where ID = @ID";

        public List<Opgave> HentAlleOpgaver()
        {
            List<Opgave> opgaver = new List<Opgave>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    opgaver.Add(ReadOpgave(reader));
                }
            }
            return opgaver;
        }

        public Opgave HentOpgaveFraId(int opgaveId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@ID", opgaveId);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return ReadOpgave(reader);
                }
            }
            return null;
        }

        public bool IndsætOpgave(Opgave opgave)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);

                TilføjVærdiOpgave(opgave, command);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool OpdaterOpgave(Opgave opgave, int opgaveID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                
                TilføjVærdiOpgave(opgave, command);
                command.Parameters.AddWithValue("@ID", opgaveID);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public Opgave SletOpgave(int opgaveID)
        {
            Opgave opgave = HentOpgaveFraId(opgaveID);
            if (opgave == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@ID", opgaveID);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return opgave;
                }
                return null;
            }
        }

        private void CheckEnumParseO(StatusType checkStatus, int checkId)
        {
            if (!(checkStatus == StatusType.Fejlet ||
                  checkStatus == StatusType.IkkeLøst ||
                  checkStatus == StatusType.Løst))
            {
                int exId = checkId;
                throw new ParseToEnumException(exId);
            }
        }

        //HentAlle og HentFraID (DRY)
        private Opgave ReadOpgave(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            String beskrivelse = reader.GetString(1);

            StatusType status = StatusType.IkkeLøst;
            try
            {
                String statusStr = reader.GetString(2);
                status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
                CheckEnumParseO(status, id);
            }
            catch (ParseToEnumException)
            {
                ParseToEnumException parseFailEx = new ParseToEnumException(id);
                string log = parseFailEx.ToString(); //string til log for exceptions på REST Siden. ikke lagret endnu. mangler liste til at blive lagret i.

            }

            int ventetid = reader.GetInt32(3);
            int udstyrId = reader.GetInt32(4);

            Udstyr udstyr = HentUdstyrFraId(udstyrId);

            return new Opgave(id, beskrivelse, status, udstyrId, udstyr); //hvad der der galt med opgave konstructor?
        }

        //Indsæt og Opdater (DRY)
        private void TilføjVærdiOpgave(Opgave opgave, SqlCommand command)
        {
            command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
            command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
            command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
            command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);
            command.Parameters.AddWithValue("@UdstyrID", opgave.Udstyr.UdstyrId);
        }

        //Extra hjælp for at hente udstyr
        private String queryStringFromIDUdstyr = "select * from RisoeUdstyr where UdstyrId = @UdstyrId";

        private String queryStringFromID2 = "select * from RisoeStation where StationNr = @StationNr";

        public Udstyr HentUdstyrFraId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromIDUdstyr, connection);
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
