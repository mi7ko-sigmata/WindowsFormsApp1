using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class Db
    {
        // Четем стринга за връзка директно от App.config
        private static string connString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;

        // Метод за създаване на връзката
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }

        // Метод за извличане на данни (полезен за SELECT заявки и зареждане на DataGridView)
        public static DataTable GetDataTable(string sql, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при извличане на данни: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // Метод за изпълнение на INSERT, UPDATE и DELETE заявки
        public static void ExecuteNonQuery(string sql, MySqlParameter[] parameters = null)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изпълнение на заявката: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // Прехвърляме грешката нагоре, за да бъде обработена във формата
            }
        }
    }
}