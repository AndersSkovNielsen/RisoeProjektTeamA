using ModelLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI.WebControls;
using ModelLibrary.Exceptions;
using static ModelLibrary.Model.Opgave;

namespace RESTRisoe.DBUtil
{
    public class ManageOpgave : IManageOpgave
    {   /// <summary>
        /// Streng med link til database når der skal oprettes forbindelse
        /// </summary>
        private String connectionString = @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        /// <summary>
        /// SQL streng til at hente alle rækker i RisoeOpgave tabellen fra databasen
        /// </summary>
        private String queryString = "select * from RisoeOpgave";

        /// <summary>
        /// SQL streng til at hente rækker i RisoeOpgave knyttet til et særligt udstyrs ID
        /// </summary>
        private string queryFromUdstyrString = "select * from RisoeOpgave where UdstyrId = @UdstyrId";

        /// <summary>
        /// SQL Streng til at hente et bestemt udstyr ud fra angivet udstyrsID
        /// </summary>
        private String queryStringFromID = "select * from RisoeOpgave where ID = @ID";

        /// <summary>
        /// SQL til til at indsætte et Opgave Object som række i RisoeOpgave Tabellen i Databasen
        /// </summary>
        private String insertSql = "insert into RisoeOpgave Values (@OpgaveID, @Beskrivelse, @Status, @Ventetid, @UdstyrID)";

        /// <summary>
        /// SQL streng til at slette en række fra RisoeOpgave Tabellen i Databasen ud fra angivet OpgaveID
        /// </summary>
        private String deleteSql = "delete from RisoeOpgave where ID = @ID";

        /// <summary>
        /// SQL streng til at opdatere værdierne for en række i RisoeOpgave Tabellen i Databasen ud fra angivet Opgave ID, samt værdier der skal opdateres
        /// </summary>
        private String updateSql = "update RisoeOpgave " +
                                   "set ID = @OpgaveID, Beskrivelse = @Beskrivelse, Status = @Status, VentetidIDage = @Ventetid " +
                                   "where ID = @ID";
        /// <summary>
        /// Metode til at hente alle Opgaver fra databasen og samle dem i et liste-objekt
        /// </summary>
        /// <returns>List<Opgave></Opgave></returns>
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
                    //OBS! Udkommenteret kode nedenfor står som eksempel på brud med DRY-princippet. Se rapport

                    //int id = reader.GetInt32(0);
                    //String beskrivelse = reader.GetString(1);
                    //String statusStr = reader.GetString(2);
                    //StatusType status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
                    //checkEnumParse(status,id);
                    //int ventetid = reader.GetInt32(3);

                    //opgaver.Add(new Opgave(id, beskrivelse, status, ventetid));

                    //Brug af ReadOpgave metode (DRY)
                    opgaver.Add(ReadOpgave(reader));
                }
            }
            return opgaver;
        }
        
        //Bliver ikke brugt i 3. iteration. Bruges til at hente et helt Udstyr
        public List<Opgave> HentAlleOpgaverForUdstyr(int udstyrId)
        {
            List<Opgave> opgaver = new List<Opgave>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryFromUdstyrString, connection);
                command.Parameters.AddWithValue("@UdstyrId", udstyrId);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    opgaver.Add(ReadOpgave(reader));
                }
            }
            return opgaver;
        }

        /// <summary>
        /// Metode til at hente en liste af opgaver tilknytet et bestemt udstyrs-objekt
        /// </summary>
        /// <param name="udstyrId"></param>
        /// <returns></returns>
        //3. iterationsmetode, der kun henter UdstyrID til Udstyr
        public List<Opgave> HentOpgaverForUdstyrID(int udstyrId)
        {
            List<Opgave> opgaver = new List<Opgave>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryFromUdstyrString, connection);
                command.Parameters.AddWithValue("@UdstyrId", udstyrId);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    opgaver.Add(ReadOpgave(reader));
                }
            }
            return opgaver;
        }
        //slut på 3. iterationsmetode

        /// <summary>
        /// Metode til at hente et Opgave-objekt fra Databasen
        /// </summary>
        /// <param name="opgaveId"></param>
        /// <returns></returns>
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
                    //int id = reader.GetInt32(0);
                    //String beskrivelse = reader.GetString(1);
                    //String statusStr = reader.GetString(2);
                    //StatusType status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
                    //checkEnumParse(status,id);
                    //int ventetid = reader.GetInt32(3);

                    //return new Opgave(id, beskrivelse, status, ventetid);

                    //Brug af ReadOpgave metode:
                    return ReadOpgave(reader);
                }
            }
            return null;
        }


        /// <summary>
        /// Metode til at indsætte et opgave-object som række i RisoeOpgave tabellen i databasen
        /// </summary>
        /// <param name="opgave"></param>
        /// <returns>bool</returns>
        public bool IndsætOpgave(Opgave opgave)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                //command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
                //command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
                //command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
                //command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);

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

        /// <summary>
        /// Metode til at opdatere en række i RisoeOpgave Tabellen
        /// </summary>
        /// <param name="opgave"></param>
        /// <param name="opgaveID"></param>
        /// <returns>bool</returns>
        public bool OpdaterOpgave(Opgave opgave, int opgaveID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                //command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
                //command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
                //command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
                //command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);

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

        /// <summary>
        /// Metode til at slettte en række i RisoeOpgave tabellen
        /// </summary>
        /// <param name="opgaveID"></param>
        /// <returns>Opgave</returns>
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

        /// <summary>
        /// Metode til at checke for exceptions i forbindelse med Parsing af OpgaveStatus. 
        /// Fra streng-felt i database, til StatusType Enumerable i program
        /// </summary>
        /// <param name="checkStatus"></param>
        /// <param name="checkId"></param>
        //ny metode skal opdateret i dokumentation 09/05
        // er gjort 25/05
        private void CheckEnumParseO(StatusType checkStatus,int checkId)
        {
            if (!(checkStatus == StatusType.Fejlet ||
                  checkStatus == StatusType.IkkeLøst ||
                  checkStatus == StatusType.Løst))
            {
                int exId = checkId;
                throw new ParseToEnumException (exId);
            }
        }

        //HentAlle og HentFraID (DRY)
        /// <summary>
        /// Metode til at indlæse et komplet Opgave-objekt fra Databasen
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Opgave ReadOpgave(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            String beskrivelse = reader.GetString(1);

            StatusType status = StatusType.IkkeLøst;
            try
            {
                String statusStr = reader.GetString(2);
                status = (StatusType) Enum.Parse(typeof(StatusType), statusStr);
                CheckEnumParseO(status, id);
            }
            catch (ParseToEnumException)
            {
               ParseToEnumException parseFailEx= new ParseToEnumException(id);
                string log = parseFailEx.ToString(); 
                
            }
        
            int ventetid = reader.GetInt32(3);
            int udstyrId = reader.GetInt32(4);

            //Skal kalde ManageUdstyr hvis vi vil have et komplet Udstyr objekt til Opgave objektet. Ikke nødvendigt i 3. iteration.
            Udstyr udstyr = new Udstyr(udstyrId);

            return new Opgave(id, beskrivelse, status, ventetid, udstyr); //hvad der der galt med opgave konstructor?
        }

        //Indsæt og Opdater (DRY)
        /// <summary>
        /// Metode til at indsætte værdier på de rigtige felter i RisoeOpgave tabellen i databasen. 
        /// Andvendes både til indsætning og opdatering
        /// </summary>
        /// <param name="opgave"></param>
        /// <param name="command"></param>
        private void TilføjVærdiOpgave(Opgave opgave, SqlCommand command)
        {
            command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
            command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
            command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
            command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);
            command.Parameters.AddWithValue("@UdstyrID", opgave.Udstyr.UdstyrId);
        }
    }
}