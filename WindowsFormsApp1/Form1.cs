using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    // ТУК Е РАЗКОВНИЧЕТО: Връщаме го на Form1, за да спре да свети InitializeComponent
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 1. ЗАРЕЖДАНЕ (LIST)
        private void LoadData()
        {
            try
            {
                string sql = "SELECT ClubId, Name, City FROM clubs ORDER BY Name";
                dgvClubs1.DataSource = Db.GetDataTable(sql);

                if (dgvClubs1.Columns.Contains("ClubId"))
                    dgvClubs1.Columns["ClubId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при свързване с БД: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e) => LoadData();

        // 2. ДОБАВЯНЕ (ADD)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Името на клуба е задължително!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO clubs (Name, City) VALUES (@name, @city)";
                MySqlParameter[] ps = {
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@city", txtCity.Text.Trim())
                };

                Db.ExecuteNonQuery(sql, ps);
                MessageBox.Show("Успешно добавен клуб!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtName.Clear();
                txtCity.Clear();
                LoadData();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("Вече съществува клуб с това име!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("База данни грешка: " + ex.Message);
            }
        }

        // 3. РЕДАКТИРАНЕ (EDIT)
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvClubs1.CurrentRow == null)
            {
                MessageBox.Show("Моля, изберете клуб от списъка за редакция!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Името на клуба не може да бъде празно!", "Внимание");
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvClubs1.CurrentRow.Cells["ClubId"].Value);
                string sql = "UPDATE clubs SET Name=@name, City=@city WHERE ClubId=@id";
                MySqlParameter[] ps = {
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@city", txtCity.Text.Trim()),
                    new MySqlParameter("@id", id)
                };

                Db.ExecuteNonQuery(sql, ps);
                MessageBox.Show("Успешно редактиран клуб!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Грешка: " + ex.Message); }
        }

        // 4. ИЗТРИВАНЕ (DELETE)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClubs1.CurrentRow == null) return;

            var result = MessageBox.Show("Сигурни ли сте, че искате да изтриете този клуб?", "Потвърждение за изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvClubs1.CurrentRow.Cells["ClubId"].Value);
                    string sql = "DELETE FROM clubs WHERE ClubId=@id";
                    Db.ExecuteNonQuery(sql, new MySqlParameter[] { new MySqlParameter("@id", id) });

                    MessageBox.Show("Клубът е изтрит!", "Успех");
                    LoadData();
                    txtName.Clear();
                    txtCity.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при изтриване: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 5. ИЗБОР НА РЕД
        private void dgvClubs1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClubs1.CurrentRow != null && e.RowIndex >= 0)
            {
                txtName.Text = dgvClubs1.CurrentRow.Cells["Name"].Value.ToString();
                txtCity.Text = dgvClubs1.CurrentRow.Cells["City"].Value.ToString();
            }
        }
    }
}