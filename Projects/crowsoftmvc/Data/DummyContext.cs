using crowsoftmvc.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowsoftmvc.Data
{
    public class DummyContext
    {
        public string ConnectionString { get; set; }

        public DummyContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Dummy> GetAllDummys()
        {
            List<Dummy> list = new List<Dummy>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Dummy", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Dummy()
                        {
                            PersonID = Convert.ToInt32(reader["PersonID"]),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
