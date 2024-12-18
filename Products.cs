using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Warehouse
{
    public partial class Products : Form
    {
        public Products()
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

                    string query = "SELECT * FROM Products";

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
                MySqlCommand comm = new MySqlCommand("INSERT INTO `products` (`ProductID`, `ProductName`, `CategoryID`, `QuantityPerUnit`, `UnitPrice`, `UnitsInStock`, `ReorderLevel`) VALUES (@pID, @pN, @ctID, @Qpu, @uP, @UIS, @Rl)", db.getConn());

                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = prodIdBox.Text;
                comm.Parameters.Add("@pN", MySqlDbType.VarChar).Value = nameBox.Text;
                comm.Parameters.Add("@ctID", MySqlDbType.Int64).Value = catIdBox.Text;
                comm.Parameters.Add("@Qpu", MySqlDbType.VarChar).Value = quantBox.Text;
                comm.Parameters.Add("@uP", MySqlDbType.Decimal).Value = priceBox.Text;
                comm.Parameters.Add("@UIS", MySqlDbType.Int64).Value = allQuanBox.Text;
                comm.Parameters.Add("@Rl", MySqlDbType.Int64).Value = lvlBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація додана успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    prodIdBox.Text = null;
                    nameBox.Text = null;
                    catIdBox.Text = null;
                    quantBox.Text = null;
                    priceBox.Text = null;
                    allQuanBox.Text = null;
                    lvlBox.Text = null;
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
                MySqlCommand comm = new MySqlCommand("DELETE FROM `products` WHERE `ProductID` = @pID AND `ProductName` = @pN AND `CategoryID` = @ctID AND `QuantityPerUnit` = @Qpu AND `UnitPrice` = @uP AND `UnitsInStock` = @UIS AND `ReorderLevel` = @Rl", db.getConn());

                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = prodIdBox.Text;
                comm.Parameters.Add("@pN", MySqlDbType.VarChar).Value = nameBox.Text;
                comm.Parameters.Add("@ctID", MySqlDbType.Int64).Value = catIdBox.Text;
                comm.Parameters.Add("@Qpu", MySqlDbType.VarChar).Value = quantBox.Text;
                comm.Parameters.Add("@uP", MySqlDbType.Decimal).Value = priceBox.Text;
                comm.Parameters.Add("@UIS", MySqlDbType.Int64).Value = allQuanBox.Text;
                comm.Parameters.Add("@Rl", MySqlDbType.Int64).Value = lvlBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація видалена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    prodIdBox.Text = null;
                    nameBox.Text = null;
                    catIdBox.Text = null;
                    quantBox.Text = null;
                    priceBox.Text = null;
                    allQuanBox.Text = null;
                    lvlBox.Text = null;
                }
                else
                    MessageBox.Show("Відхилено!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                db.closeConn();
            }
            catch(FormatException)
            {
                MessageBox.Show("Не вірно введений формат данних!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                MySqlCommand comm = new MySqlCommand("UPDATE `products` SET `ProductName` = @pN, `CategoryID` = @ctID, `QuantityPerUnit` = @Qpu, `UnitPrice` = @uP, `UnitsInStock` = @UIS, `ReorderLevel` = @Rl WHERE `ProductID` = @pID", db.getConn());

                comm.Parameters.Add("@pID", MySqlDbType.Int64).Value = prodIdBox.Text;
                comm.Parameters.Add("@pN", MySqlDbType.VarChar).Value = nameBox.Text;
                comm.Parameters.Add("@ctID", MySqlDbType.Int64).Value = catIdBox.Text;
                comm.Parameters.Add("@Qpu", MySqlDbType.VarChar).Value = quantBox.Text;
                comm.Parameters.Add("@uP", MySqlDbType.Decimal).Value = priceBox.Text;
                comm.Parameters.Add("@UIS", MySqlDbType.Int64).Value = allQuanBox.Text;
                comm.Parameters.Add("@Rl", MySqlDbType.Int64).Value = lvlBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація змінена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    prodIdBox.Text = null;
                    nameBox.Text = null;
                    catIdBox.Text = null;
                    quantBox.Text = null;
                    priceBox.Text = null;
                    allQuanBox.Text = null;
                    lvlBox.Text = null;
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
            var formOpen = new Categories();
            formOpen.ShowDialog();
        }
    }
}
