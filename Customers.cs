using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Warehouse
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            LoadCategoriesData();
        }
        private void LoadCategoriesData()
        {
            string connectionString = "server=localhost;port=3306;username=root;password=1234;database=warehousedb;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Customers";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка завантаження даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                MySqlCommand comm = new MySqlCommand("INSERT INTO `customers` (`CustomerID`, `CustomerName`, `City`, `Country`, `Phone`) VALUES (@cID, @cN, @cY, @cR, @pH)", db.getConn());

                comm.Parameters.Add("@cID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@cN", MySqlDbType.VarChar).Value = nameBox.Text;
                comm.Parameters.Add("@cY", MySqlDbType.VarChar).Value = cityBox.Text;
                comm.Parameters.Add("@cR", MySqlDbType.VarChar).Value = countryBox.Text;
                comm.Parameters.Add("@pH", MySqlDbType.VarChar).Value = phoneBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація додана успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    nameBox.Text = null;
                    cityBox.Text = null;
                    countryBox.Text = null;
                    phoneBox.Text = null;
                }
                else
                    MessageBox.Show("Відхилено!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                db.closeConn();
            }
            catch (FormatException)
            {
                MessageBox.Show("Не вірно введений формат данних!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                MySqlCommand comm = new MySqlCommand("DELETE FROM `customers` WHERE `CustomerID` = @cID AND `CustomerName` = @cN AND `City` = @cY AND `Country` = @cR AND `Phone` = @pH", db.getConn());

                comm.Parameters.Add("@cID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@cN", MySqlDbType.VarChar).Value = nameBox.Text;
                comm.Parameters.Add("@cY", MySqlDbType.VarChar).Value = cityBox.Text;
                comm.Parameters.Add("@cR", MySqlDbType.VarChar).Value = countryBox.Text;
                comm.Parameters.Add("@pH", MySqlDbType.VarChar).Value = phoneBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація видалена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    nameBox.Text = null;
                    cityBox.Text = null;
                    countryBox.Text = null;
                    phoneBox.Text = null;
                }
                else
                    MessageBox.Show("Відхилено!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                db.closeConn();
            }
            catch (FormatException)
            {
                MessageBox.Show("Не вірно введений формат данних!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { }
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                MySqlCommand comm = new MySqlCommand("UPDATE `customers` SET `CustomerName` = @cN, `City` = @cY, `Country` = @cR, `Phone` = @pH WHERE `CustomerID` = @cID", db.getConn());

                comm.Parameters.Add("@cID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@cN", MySqlDbType.VarChar).Value = nameBox.Text;
                comm.Parameters.Add("@cY", MySqlDbType.VarChar).Value = cityBox.Text;
                comm.Parameters.Add("@cR", MySqlDbType.VarChar).Value = countryBox.Text;
                comm.Parameters.Add("@pH", MySqlDbType.VarChar).Value = phoneBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація змінена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    nameBox.Text = null;
                    cityBox.Text = null;
                    countryBox.Text = null;
                    phoneBox.Text = null;
                }
                else
                    MessageBox.Show("Відхилено!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                db.closeConn();
            }
            catch (FormatException)
            {
                MessageBox.Show("Не вірно введений формат данних!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Menu();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }
    }
}
