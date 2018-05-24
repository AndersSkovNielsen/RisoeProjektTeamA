using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary.Model;

namespace RESTRisoe.DBUtil
{
    public class ManageStation : IManageStation
    {
        private String connectionString =
                @"Data Source=ande651p-easj-dbserver.database.windows.net;Initial Catalog=ande651p-easj-DB;Integrated Security=False;User ID=asn230791;Password=Risoe2018;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            ;

        //Sql strings skal tjekkes pga. manglende adgang til DB 22/05
        private String queryString = "select * from RisoeStation";


        private String queryStringFromID = "select * from RisoeStation where StationNr = @StationNr";

        private String insertSql =
            "insert into RisoeStation Values (@StationNr, @Navn)";

        private String deleteSql = "delete from RisoeStation where StationNr = @StationNr";

        private String updateSql = "update RisoeStation " +
                                   "set StationNr = @StationNr, Navn = @Navn " +
                                   "where StationNr = @StationNr";

        public List<Station> HentAlleStationer()
        {
            List<Station> stationer = new List<Station>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stationer.Add(ReadStation(reader));
                }
            }
            return stationer;
        }

        public Station HentStationFraId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
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

        public bool IndsætStation(Station station)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);

                TilføjVærdiStation(station, command);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }


        public bool OpdaterStationr(Station station, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);

                TilføjVærdiStation(station, command);
                command.Parameters.AddWithValue("@StationNr", id);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public Station SletStation(int id)
        {
           Station station = HentStationFraId(id);
            if (station == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@StationNr", id);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return station;
                }
                return null;
            }
        }


        //hjælpemetoder
        private Station ReadStation(SqlDataReader reader)
        {
            int stationNr = reader.GetInt32(0);
            String navn = reader.GetString(1);

            return new Station(navn, stationNr);
        }
        private void TilføjVærdiStation(Station station, SqlCommand command)
        {
            command.Parameters.AddWithValue("@StationNr", station.StationsId);
            command.Parameters.AddWithValue("@Navn", station.Navn);
        }
    }
}
