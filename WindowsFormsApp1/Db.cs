using System;
using System.Data;
using MySql.Data.MySqlClient; // Увери се, че си инсталирал NuGet пакета MySql.Data

public static class Db
{
    // Промени данните за връзка според твоята база
    private static string connString = "Server=localhost;Database=clubs_db;Uid=root;Pwd=your_password;";

    public static DataTable GetDataTable(string sql)
    {
        using (var conn = new MySqlConnection(connString))
        {
            using (var cmd = new MySqlCommand(sql, conn))
            {
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public static void ExecuteNonQuery(string sql, MySqlParameter[] parameters)
    {
        using (var conn = new MySqlConnection(connString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
            }
        }
    }
}