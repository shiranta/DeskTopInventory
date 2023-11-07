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
    public partial class frmAdminDashBoard : Form
    {
        //public object Form1 { get; private set; }

        //private string loggIned;

        public frmAdminDashBoard()
        {
            InitializeComponent();
        }

        
        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.frmUsers user = new frmUsers();
                user.Show();



        }

        private void frmAdminDashBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogins f = new frmLogins();

            f.Show();

            this.Hide();
                      
        }

        private void llbEnterUser_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void frmAdminDashBoard_Load(object sender, EventArgs e)
        {

            lblLogIn.Text = frmLogins.loggIned;
            lblLogIn.Visible = true;


        }

        private void llbEnterUser_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategories category = new frmCategories();
            category.Show();

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.Show();

        }

        private void dealerCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContactNo custdeal = new txtContactNo();
            custdeal.Show();

        }

        private void purchaseAndSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();

        }
    }
}
