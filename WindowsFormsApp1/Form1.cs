using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class dgvClubs : Form
    {
        public dgvClubs()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadClubs()
        {
            string sql = "SELECT ClubId, Name, City FROM clubs ORDER BY Name";
            dgvClubs1.DataSource = Db.GetDataTable(sql);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Името е задължително!"); return;
            }

            try
            {
                string sql = "INSERT INTO clubs (Name, City) VALUES (@name, @city)";
                MySqlParameter[] ps = {
            new MySqlParameter("@name", txtName.Text),
            new MySqlParameter("@city", txtCity.Text)
        };
                Db.ExecuteNonQuery(sql, ps);
                LoadClubs(); // Обновяваме списъка
                MessageBox.Show("Успешно добавен клуб!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка: " + ex.Message);
            }   
            private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClubs1.CurrentRow == null) return;

            var result = MessageBox.Show("Сигурни ли сте?", "Потвърждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvClubs1.CurrentRow.Cells["ClubId"].Value);
                string sql = "DELETE FROM clubs WHERE ClubId = @id";
                MySqlParameter[] ps = { new MySqlParameter("@id", id) };
                Db.ExecuteNonQuery(sql, ps);
                LoadClubs();
            }
        }

    }
}

