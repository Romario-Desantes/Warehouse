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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void categoryBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Categories();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void productBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Products();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void suplBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Suppliers();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void shipmentBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Shipments();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void shipmentDetailBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new ShipmentDetails();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void orderBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Order();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void orderDetailBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new OrderDetails();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void transactionsBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Transactions();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }

        private void customerBtn_Click(object sender, EventArgs e)
        {
            var formOpen = new Customers();
            formOpen.Show();
            this.Hide();
            formOpen.FormClosed += (s, arg) => this.Close();
        }
    }
}
