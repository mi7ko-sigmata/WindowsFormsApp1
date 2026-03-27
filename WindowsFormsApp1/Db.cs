using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public static class Db
    {
        // ТУК СЛОЖИ ТВОЯТА ПАРОЛА И БАЗА!
        private static string connectionString = "Server=localhost;Database=football_db;Uid=root;Pwd=ТВОЯТА_ПАРОЛА;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // За SELECT заявки (връща таблица)
        public static DataTable GetDataTable(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // За INSERT, UPDATE, DELETE (връща брой засегнати редове)
        public static int ExecuteNonQuery(string query, MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}   