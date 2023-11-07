using NewStore;
using NewStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewStore
{
    public partial class frmUserAdmin : Form
    {
        public static string TrnType;

        public frmUserAdmin()
        {
            InitializeComponent();
        }

        
        private void frmUserAdmin_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            frmLogins f = new frmLogins();

            f.Show();

            this.Hide();
        }

        private void frmUserAdmin_Load(object sender, EventArgs e)
        {
            label4.Text = frmLogins.loggIned;

        }

        private void label4_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void customerDealersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContactNo custDeal = new txtContactNo();
            custDeal.Show();

        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrnType = "Purchase";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();

        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrnType = "Sales";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();

        }
    }

    
}
