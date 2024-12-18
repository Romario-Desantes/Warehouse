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
    public partial class OrderDetails : Form
    {
        public OrderDetails()
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

                    string query = "SELECT * FROM OrderDetails";

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
                MySqlCommand comm = new MySqlCommand("INSERT INTO `orderdetails` (`OrderDetailID`, `OrderID`, `ProductID`, `Quantity`, `PricePerUnit`) VALUES (@odID, @oID, @pID, @qT, @PPU)", db.getConn());

                comm.Parameters.Add("@odID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@oID", MySqlDbType.Int64).Value = idOrderBox.Text;
                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = idProdBox.Text;
                comm.Parameters.Add("@qT", MySqlDbType.Int64).Value = quantBox.Text;
                comm.Parameters.Add("@PPU", MySqlDbType.Decimal).Value = priceBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація додана успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    idOrderBox.Text = null;
                    idProdBox.Text = null;
                    quantBox.Text = null;
                    priceBox.Text = null;
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
                MySqlCommand comm = new MySqlCommand("DELETE FROM `orderdetails` WHERE `OrderDetailID` = @odID AND `OrderID` = @oID AND `ProductID` = @pID AND `Quantity` = @qT AND `PricePerUnit` = @PPU", db.getConn());

                comm.Parameters.Add("@odID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@oID", MySqlDbType.Int64).Value = idOrderBox.Text;
                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = idProdBox.Text;
                comm.Parameters.Add("@qT", MySqlDbType.Int64).Value = quantBox.Text;
                comm.Parameters.Add("@PPU", MySqlDbType.Decimal).Value = priceBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація видалена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    idOrderBox.Text = null;
                    idProdBox.Text = null;
                    quantBox.Text = null;
                    priceBox.Text = null;
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
                MySqlCommand comm = new MySqlCommand("UPDATE `orderdetails` SET `OrderID` = @oID, `ProductID` = @pID, `Quantity` = @qT, `PricePerUnit` = @PPU WHERE `OrderDetailID` = @odID", db.getConn());

                comm.Parameters.Add("@odID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@oID", MySqlDbType.Int64).Value = idOrderBox.Text;
                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = idProdBox.Text;
                comm.Parameters.Add("@qT", MySqlDbType.Int64).Value = quantBox.Text;
                comm.Parameters.Add("@PPU", MySqlDbType.Decimal).Value = priceBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація змінена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    idOrderBox.Text = null;
                    idProdBox.Text = null;
                    quantBox.Text = null;
                    priceBox.Text = null;
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

        private void checkBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Order();
            formOpen.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var formOpen = new Products();
            formOpen.ShowDialog();
        }
    }
}
