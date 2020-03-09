using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Checkpoint2
{
    public class Database
    {
        private static Database _instance = null;
        private SqlConnection _connection = null;

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        private Database()
        {
            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=Checkpoint2;Integrated Security=True";

            SqlConnection conn = new SqlConnection(connectionString);

            _connection = conn;

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void Connect(SqlConnectionStringBuilder builder)
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                throw new Exception("Database already connected");
            }
            _connection.ConnectionString = builder.ConnectionString;
            _connection.Open();
        }

        public static List<Event> GetEventsList (string Name, DateTime StartTime, DateTime EndTime)
        {
            SqlCommand cmd = new SqlCommand("sp_GetAllEventBeetweenTwoDatesForAPerson");
            cmd.Connection = Database.Instance._connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@person_name", Name));
            cmd.Parameters.Add(new SqlParameter("@start_date", StartTime));
            cmd.Parameters.Add(new SqlParameter("@end_date", EndTime));
            List<Event> EventList = new List<Event>();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Event newEvent = new Event();
                    newEvent.EventId = reader.GetInt32(reader.GetOrdinal("event_id"));
                    newEvent.EventName = reader.GetString(reader.GetOrdinal("event_name"));
                    newEvent.StartTime = reader.GetDateTime(reader.GetOrdinal("start_name"));
                    newEvent.EndTime = reader.GetDateTime(reader.GetOrdinal("end_name"));
                    newEvent.Description = reader.GetString(reader.GetOrdinal("event_description"));
                    EventList.Add(newEvent);
                }
            }
            else
            {
                throw new Exception("No data has been found with sql reader");
            }
            return EventList;
        }
    }
}

