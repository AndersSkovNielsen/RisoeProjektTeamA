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
                    udstyr.Add(ReadUdstyr(reader));
                }
            }
            return udstyr;
        }

        public List<Opgave> HentAltUdstyrForStation(int udstyrId)
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
                    //opgaver.Add(ReadOpgave(reader));
                }
            }
            return opgaver;
        }

        public Udstyr HentUdstyrFraId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@ID", id);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return ReadUdstyr(reader);
                }
            }
            return null; //Kan vi skrive dette?
        }

        private void CheckEnumParseU(uType checkType, int checkId)
        {
            if (!(checkType == uType.type1 ||
                  checkType == uType.type2 ||
                  checkType == uType.type3 ||
                  checkType == uType.type4))
            {
                int exId = checkId;
                throw new ParseToEnumException(exId);
            }
        }

        private Udstyr ReadUdstyr(SqlDataReader reader) //denne metode skal justeres så den tager fat de rigtige steder i DB
        {
            int udstyrId = reader.GetInt32(0);
            int stationId = reader.GetInt32(1);
            
            uType type = uType.type1;
            try
            {
                string typeStr = reader.GetString(2);
                type = (uType) Enum.Parse(typeof(uType), typeStr);
                
                CheckEnumParseU(type, udstyrId);
            }
            catch (ParseToEnumException)
            {
                ParseToEnumException parseFailEx = new ParseToEnumException(udstyrId);
                string log = parseFailEx.ToString();

            }

            DateTime instDato = reader.GetDateTime(3);
            string beskrivelse = reader.GetString(4);
            //Sidstetjek og næstetjek skal slettes fra udstyrklassen
            return new Udstyr(udstyrId, instDato, /*new DateTime(2018, 2,1), new DateTime(2018, 3, 3),*/ beskrivelse, type);
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

        public bool OpdaterUdstyr(Udstyr udstyr, int id)
        {
            throw new NotImplementedException();
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
        private void TilføjVærdiUdstyr(Udstyr udstyr, SqlCommand command)
        {
            command.Parameters.AddWithValue("@UdstyrID", udstyr.UdstyrId);
            command.Parameters.AddWithValue("@Beskrivelse", udstyr.Beskrivelse);
            command.Parameters.AddWithValue("@Status", udstyr.Type.ToString());
            command.Parameters.AddWithValue("@Installationsdato", udstyr.Installationsdato);
            //command.Parameters.AddWithValue("@SidsteTjekDato", udstyr.SidsteTjekDato);
            //command.Parameters.AddWithValue("@NæsteTjekDato", udstyr.NæsteTjekDato);
        }
    }
}