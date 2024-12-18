using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class ShipmentDetails : Form
    {
        public ShipmentDetails()
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

                    string query = "SELECT * FROM ShipmentDetails";

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
                MySqlCommand comm = new MySqlCommand("INSERT INTO `shipmentdetails` (`ShipmentDetailID`, `ShipmentID`, `ProductID`, `Quantity`, `CostPerUnit`) VALUES (@sdID, @sID, @pID, @qT, @CPU)", db.getConn());

                comm.Parameters.Add("@sdID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@sID", MySqlDbType.Int64).Value = idShipmtBox.Text;
                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = idProdBox.Text;
                comm.Parameters.Add("@qT", MySqlDbType.Int64).Value = quantityBox.Text;
                comm.Parameters.Add("@CPU", MySqlDbType.Decimal).Value = costBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація додана успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    idShipmtBox.Text = null;
                    idProdBox.Text = null;
                    quantityBox.Text = null;
                    costBox.Text = null;
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
                MySqlCommand comm = new MySqlCommand("DELETE FROM `shipmentdetails` WHERE `ShipmentDetailID` = @sdID AND `ShipmentID` = @sID AND `ProductID` = @pID AND `Quantity` = @qT AND `CostPerUnit` = @CPU", db.getConn());

                comm.Parameters.Add("@sdID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@sID", MySqlDbType.Int64).Value = idShipmtBox.Text;
                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = idProdBox.Text;
                comm.Parameters.Add("@qT", MySqlDbType.Int64).Value = quantityBox.Text;
                comm.Parameters.Add("@CPU", MySqlDbType.Decimal).Value = costBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація видалена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    idShipmtBox.Text = null;
                    idProdBox.Text = null;
                    quantityBox.Text = null;
                    costBox.Text = null;
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
                MySqlCommand comm = new MySqlCommand("UPDATE `shipmentdetails` SET `ShipmentID` = @sID, `ProductID` = @pID, `Quantity` = @qT, `CostPerUnit` = @CPU WHERE `ShipmentDetailID` = @sdID", db.getConn());

                comm.Parameters.Add("@sdID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@sID", MySqlDbType.Int64).Value = idShipmtBox.Text;
                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = idProdBox.Text;
                comm.Parameters.Add("@qT", MySqlDbType.Int64).Value = quantityBox.Text;
                comm.Parameters.Add("@CPU", MySqlDbType.Decimal).Value = costBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація змінена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    idBox.Text = null;
                    idShipmtBox.Text = null;
                    idProdBox.Text = null;
                    quantityBox.Text = null;
                    costBox.Text = null;
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
            var formOpen = new Shipments();
            formOpen.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var formOpen = new Products();
            formOpen.ShowDialog();
        }
    }
}
