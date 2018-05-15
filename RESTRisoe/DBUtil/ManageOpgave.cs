using ModelLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;
using ModelLibrary.Exceptions;
using static ModelLibrary.Model.Opgave;

namespace RESTRisoe.DBUtil
{
    public class ManageOpgave : IManageOpgave
    {
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

        //ny metode skal opdateret i dokumentation 09/05
        private void CheckEnumParse(StatusType checkStatus,int checkId)
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
        private Opgave ReadOpgave(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            String beskrivelse = reader.GetString(1);
            StatusType status=StatusType.IkkeLøst;
            try
            {
                String statusStr = reader.GetString(2);
                status = (StatusType) Enum.Parse(typeof(StatusType), statusStr);
                CheckEnumParse(status, id);
            }
            catch (ParseToEnumException)
            {
               ParseToEnumException parseFailEx= new ParseToEnumException(id);
                string log = parseFailEx.ToString(); //string til log for exceptions på REST Siden. ikke lagret endnu. mangler liste til at blive lagret i.
                
            }

            //String statusStr = reader.GetString(2);
            //StatusType status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
            //checkEnumParse(status, id);
            int ventetid = reader.GetInt32(3);

            return new Opgave(id, beskrivelse, status, ventetid);
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