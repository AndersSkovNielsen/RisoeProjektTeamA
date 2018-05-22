using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public class ManageUdstyr : IManageUdstyr
    {
        private String connectionString =
                @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            ;

        //Sql strings skal tjekkes pga. manglende adgang til DB 22/05
        private String queryString = "select * from RisoeUdstyr";

        private string queryFromUdstyrString = "select * from RisoeOpgave where UdstyrId= @udstyrId";
        private String queryStringFromID = "select * from RisoeUdstyr where ID = @ID";

        private String insertSql =
                "insert into RisoeUdstyr Values (@UdstyrID, @StationNr, @Type, @Installationsdato, @SidsteTjekDato, @NæsteTjekDato, @Beskrivelse)"
            ;

        private String deleteSql = "delete from RisoeOpgave where UdstyrID = @UdstyrID";

        private String updateSql = "update RisoeUdstyr " +
                                   "set UdstyrID = @UdstyrID, StationNr= @StationNr, Type= @Type, Installationsdato= @Installationsdato, SidsteTjekDato= @SidsteTjekDato, NæsteTjekDato= @NæsteTjekDato, Beskrivelse= @Beskrivelse"
            ;
        //slut på Sql Strings

        public List<Udstyr> HentAltUdstyr()
        {
            List<Udstyr> udstyr = new List<Udstyr>();

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
                    udstyr.Add(ReadUdstyr(reader));
                }
            }
            return udstyr;
        }

        public List<Udstyr> HentAlleOpgaverForUdstyr(int udstyrId)
        {
            throw new NotImplementedException();
        }

        public Udstyr HentUdstyrFraId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@ID", UdstyrId);

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
                    return ReadUdstyr(reader);
                }
            }

            public bool IndsætUdstyr(Udstyr udstyr)
            {
                throw new NotImplementedException();
            }

            public bool OpdaterUdstyr(Udstyr udstyr, int id)
            {
                throw new NotImplementedException();
            }

            public Udstyr SletUdstyr(int id)
            {
                throw new NotImplementedException();
            }
        }
    }
}