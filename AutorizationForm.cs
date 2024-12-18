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

namespace Warehouse
{
    public partial class AutorizationForm : Form
    {
        public AutorizationForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            String login = loginBox.Text;
            String password = passwordBox.Text;
            String role = roleListBox.SelectedItem?.ToString();

            DB db = new DB();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `UserName` = @uN AND `PasswordHash` = @pH AND `Role` = @rL", db.getConn());

            command.Parameters.Add("@uN", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@pH", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@rL", MySqlDbType.Enum).Value = role;

            adapter.SelectCommand = command;
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Успішна авторизація!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var formOpen = new Menu();
                formOpen.Show();
                this.Hide();
                formOpen.FormClosed += (s, arg) => this.Close();
            }
                
            else
                MessageBox.Show("Відхилено!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
