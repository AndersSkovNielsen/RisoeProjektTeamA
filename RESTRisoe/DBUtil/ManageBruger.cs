using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary.Exceptions;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public class ManageBruger : IManageBruger
    {

        private String connectionString = @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private String insertSql = "insert into RisoeBruger Values (@Initialer, @Kodeord)";

        private String queryStringFromID = "select * from RisoeBruger where Initialer = @Initialer";
        private String deleteSql = "delete from RisoeBruger where Initialer = @Initialer";

        private String queryString = "select * from RisoeBruger";

        public List<Bruger> HentAlleBruger()
        {
            List<Bruger> brugere = new List<Bruger>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    
                    brugere.Add(LæsBruger(reader));
                }
            }
            return brugere;
        }

        public Bruger HentBrugerFraInitialer(string brugerInitialer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@Initialer", brugerInitialer);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    
                    return LæsBruger(reader);
                }
            }
            return null;
        }

        public bool indsætBruger(Bruger bruger)

        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                

                //Brug af TilføjVærdiOpgave metode (DRY)
                TilføjVærdiBruger(bruger, command);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }


        public Bruger SletBruger(string brugerInitialer)
        {
            Bruger bruger = HentBrugerFraInitialer(brugerInitialer);
            if (bruger == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@Initialer", brugerInitialer);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return bruger;
                }
                return null;
            }
        }



        private void TilføjVærdiBruger(Bruger bruger, SqlCommand command)
        {
            command.Parameters.AddWithValue("@Initialer", bruger.Initialer);
            command.Parameters.AddWithValue("@Kodeord", bruger.KodeOrd);
        }

        private Bruger LæsBruger(SqlDataReader reader)
        {
            string initialer = reader.GetString(0);
            String kodeOrd = reader.GetString(1);
            
            

            return new Bruger(initialer, kodeOrd);
        }


    }//---
}