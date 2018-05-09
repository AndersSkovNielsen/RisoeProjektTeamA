﻿using ModelLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RESTRisoe.Exceptions;
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
                    //Kunne laves som en privat metode ReadOpgave for at undgå DRY
                    int id = reader.GetInt32(0);
                    String beskrivelse = reader.GetString(1);
                    String statusStr = reader.GetString(2);
                    StatusType status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
                    int ventetid = reader.GetInt32(3);

                    opgaver.Add(new Opgave(id, beskrivelse, status, ventetid));
                }

            }
            foreach (var opgave in opgaver)
            {
                if (  !(opgave.Status == StatusType.Fejlet ||
                        opgave.Status == StatusType.IkkeLøst || 
                        opgave.Status== StatusType.Løst))
                {
                    throw new ParseToEnumException(opgave);
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
                    //Kunne laves som en privat metode ReadOpgave for at undgå DRY
                    int id = reader.GetInt32(0);
                    String beskrivelse = reader.GetString(1);
                    String statusStr = reader.GetString(2);
                    StatusType status = (StatusType)Enum.Parse(typeof(StatusType), statusStr);
                    int ventetid = reader.GetInt32(3);

                    return new Opgave(id, beskrivelse, status, ventetid);
                }
            }
            return null;
        }

        public bool IndsætOpgave(Opgave opgave)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
                command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
                command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
                command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);

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
                command.Parameters.AddWithValue("@OpgaveID", opgave.ID);
                command.Parameters.AddWithValue("@Beskrivelse", opgave.Beskrivelse);
                command.Parameters.AddWithValue("@Status", opgave.Status.ToString());
                command.Parameters.AddWithValue("@Ventetid", opgave.VentetidIDage);
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
    }
}