﻿using System;
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
    public partial class Shipments : Form
    {
        public Shipments()
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

                    string query = "SELECT * FROM Shipments";

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
                MySqlCommand comm = new MySqlCommand("INSERT INTO `shipments` (`ShipmentID`, `SupplierID`, `ShipmentDate`, `TotalCost`) VALUES (@sID, @spID, @sD, @tC)", db.getConn());

                comm.Parameters.Add("@sID", MySqlDbType.Int64).Value = numBox.Text;
                comm.Parameters.Add("@spID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@sD", MySqlDbType.DateTime).Value = dateBox.Text;
                comm.Parameters.Add("@tC", MySqlDbType.Decimal).Value = priceBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація додана успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    numBox.Text = null;
                    idBox.Text = null;
                    dateBox.Text = null;
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
                MySqlCommand comm = new MySqlCommand("DELETE FROM `shipments` WHERE `ShipmentID` = @sID AND `SupplierID` = @spID AND `ShipmentDate` = @sD AND `TotalCost` = @tC", db.getConn());

                comm.Parameters.Add("@sID", MySqlDbType.Int64).Value = numBox.Text;
                comm.Parameters.Add("@spID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@sD", MySqlDbType.DateTime).Value = dateBox.Text;
                comm.Parameters.Add("@tC", MySqlDbType.Decimal).Value = priceBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація видалена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    numBox.Text = null;
                    idBox.Text = null;
                    dateBox.Text = null;
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
            catch(Exception){ }
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                MySqlCommand comm = new MySqlCommand("UPDATE `shipments` SET `SupplierID` = @spID, `ShipmentDate` = @sD, `TotalCost` = @tC WHERE `ShipmentID` = @sID", db.getConn());

                comm.Parameters.Add("@sID", MySqlDbType.Int64).Value = numBox.Text;
                comm.Parameters.Add("@spID", MySqlDbType.Int64).Value = idBox.Text;
                comm.Parameters.Add("@sD", MySqlDbType.VarChar).Value = dateBox.Text;
                comm.Parameters.Add("@tC", MySqlDbType.Decimal).Value = priceBox.Text;

                db.openConn();

                if (comm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Інформація змінена успішно!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesData();
                    numBox.Text = null;
                    idBox.Text = null;
                    dateBox.Text = null;
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

        private void checkBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Suppliers();
            formOpen.ShowDialog();
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
