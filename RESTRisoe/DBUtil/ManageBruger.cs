using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public class ManageBruger : IManageBruger
    {

        private String connectionString = @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private String insertSql = "insert into RisoeBruger Values (@Initialer, @Kodeord)";

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


        private void TilføjVærdiBruger(Bruger bruger, SqlCommand command)
        {
            command.Parameters.AddWithValue("@Initialer", bruger.Initialer);
            command.Parameters.AddWithValue("@Kodeord", bruger.KodeOrd);
           
        }
    }
}