using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using ModelLibrary.Exceptions;
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

        private string queryFromStationString = "select * from RisoeUdstyr where StationNr = @StationNr";

        private String queryStringFromID = "select * from RisoeUdstyr where UdstyrId = @UdstyrId";

        private String insertSql =
                "insert into RisoeUdstyr Values (@UdstyrID, @StationNr, @Type, @Installationsdato, @Beskrivelse)"
            ;

        private String deleteSql = "delete from RisoeOpgave where UdstyrId = @UdstyrID";

        private String updateSql = "update RisoeUdstyr " +
                                   "set UdstyrId = @UdstyrID, StationNr = @StationNr, Type = @Type, InstallationsDato = @Installationsdato, Beskrivelse = @Beskrivelse " +
                                    "where UdstyrId = @ID"
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
                    udstyr.Add(ReadUdstyr(reader));
                }
            }
            return udstyr;
        }

        public List<Udstyr> HentAltUdstyrForStation(int stationId)
        {
            List<Udstyr> udstyr = new List<Udstyr>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryFromStationString, connection);
                command.Parameters.AddWithValue("@StationNr", stationId);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    udstyr.Add(ReadUdstyr(reader));
                }
            }
            return udstyr;
        }

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

        


        public bool IndsætUdstyr(Udstyr udstyr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                
                TilføjVærdiUdstyr(udstyr, command);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool OpdaterUdstyr(Udstyr udstyr, int udstyrID)
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);

                TilføjVærdiUdstyr(udstyr, command);
                command.Parameters.AddWithValue("@ID", udstyrID);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public Udstyr SletUdstyr(int id)
        {
            Udstyr udstyr = HentUdstyrFraId(id);
            if (udstyr == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@ID", id);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return udstyr;
                }
                return null;
            }
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

        //Hjælpemetoder
        private Udstyr ReadUdstyr(SqlDataReader reader) //denne metode skal justeres så den tager fat de rigtige steder i DB
        {
            int udstyrId = reader.GetInt32(0);
            int stationId = reader.GetInt32(1);

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
            
            return new Udstyr(udstyrId, instDato, beskrivelse, type,new Station("unittestStation",99));
        }

        private void TilføjVærdiUdstyr(Udstyr udstyr, SqlCommand command)
        {
            command.Parameters.AddWithValue("@UdstyrID", udstyr.UdstyrId);
            command.Parameters.AddWithValue("@Beskrivelse", udstyr.Beskrivelse);
            command.Parameters.AddWithValue("@Status", udstyr.Type.ToString());
            command.Parameters.AddWithValue("@Installationsdato", udstyr.Installationsdato);
        }
    }
}