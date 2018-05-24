using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;

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

            Opgave OP = new Opgave(10,"test",StatusType.IkkeLøst,1,2,new Udstyr());

            IndsætOpgave(OP);

            Console.WriteLine(HentOpgaveFraId(10));

            Console.ReadKey();
            Console.Clear();

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ 
            //Opdater Opgave (test)
            Console.WriteLine("Test af opdatering af opgave");
            Console.WriteLine("");

            Opgave NewOP = new Opgave(10,"test2",StatusType.Løst,2,3,new Udstyr());

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
        private String insertSql = "insert into RisoeOpgave Values (@OpgaveID, @Beskrivelse, @Status, @Ventetid)";
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
                    //Brug af ReadOpgave metode (DRY)
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
                    //Brug af ReadOpgave metode:
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
                
                //Brug af TilføjVærdiOpgave metode (DRY)
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
                
                //Brug af TilføjVærdiOpgave metode (DRY)
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

        //HentAlle og HentFraID (DRY)
        private Opgave ReadOpgave(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            String beskrivelse = reader.GetString(1);
            String statusStr = reader.GetString(2);
            StatusType status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
            int ventetid = reader.GetInt32(3);
            int udstyrId = reader.GetInt32(4);

            return new Opgave(id,beskrivelse,status,id,ventetid,udstyrId);
        }

        //Indsæt og Opdater (DRY)
        private void TilføjVærdiOpgave(Opgave opgave, SqlCommand command)
        {
            command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
            command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
            command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
            command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);
        }
    }
}
