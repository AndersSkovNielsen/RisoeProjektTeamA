using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace REST.DBUtil
{
    public class ManageOpgave : IManageOpgave
    {
        private String connectionString = @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private String queryString = "select * from RisoeOpgave";

        public List<TestOpgave> GetAllTestOpgave()
        {
            List<TestOpgave> opgaver = new List<TestOpgave>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    String beskrivelse = reader.GetString(1);
                    String status = reader.GetString(2);
                    int ventetid = reader.GetInt32(3);

                    opgaver.Add(new TestOpgave(id, beskrivelse, status, ventetid));
                }

            }
            return opgaver;
        }
    }
}