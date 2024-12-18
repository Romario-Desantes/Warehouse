using MySql.Data.MySqlClient; 
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Warehouse
{
                
    public partial class Categories : Form
    {
        public Categories()
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

                    string query = "SELECT * FROM Categories";

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
                MySqlCommand comm = new MySqlCommand("INSERT INTO `categories` (`CategoryName`, `Description`) VALUES (@cN, @cD)", db.getConn());

                comm.Parameters.Add("@cN", MySqlDbType.VarChar).Value = categoryBox.Text;
                comm.Parameters.Add("@cD", MySqlDbType.VarChar).Value = descrBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація додана успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    categoryBox.Text = null;
                    descrBox.Text = null;
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
                string ctID = idBox.Text;

                DB db = new DB();
                MySqlCommand comm = new MySqlCommand("DELETE FROM `categories` WHERE `CategoryID` = @cID", db.getConn());

                comm.Parameters.Add("@cID", MySqlDbType.Int64).Value = ctID;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація видалена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
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

        private void button1_Click(object sender, EventArgs e)
        {
            var formOpen = new Menu();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }
    }
}
