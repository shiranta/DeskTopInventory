using NewStore.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewStore.UI
{
    public partial class txtContactNo : Form
    {
        BL.BlCustDeal c = new BL.BlCustDeal();
        DL.DalCustDeal d = new DL.DalCustDeal();
        DL.DataAccess a = new DL.DataAccess();
        public txtContactNo()
        {
            InitializeComponent();
        }

       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.Name = txtName.Text;
            c.Type = cmBCustDeal.Text;
            c.Email = txtEmail.Text;
            c.ContactNO = txtConNo.Text;
            c.AddedDate = DateTime.Now;
            c.Address = txtAddress.Text;

            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;
            BlUser usr = a.getIDfromUserName(loggedUser);
            c.AddedBy = usr.Id;


            bool Success = d.InsUser(c);

            if (Success == true)
            {
                MessageBox.Show("Record added successfully");
            }
            else
            {
                MessageBox.Show("Recoard added Failed");

            }
            DataTable dt = d.Selected();
            dbGridCustDeal.DataSource = dt;
            Cleantxt();

        }

        private void Cleantxt()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtConNo.Text = "";

        }

        private void dbGridCustDeal_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtCustDealId.Text =dbGridCustDeal.Rows[rowIndex].Cells[0].Value.ToString();
            cmBCustDeal.Text= dbGridCustDeal.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dbGridCustDeal.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dbGridCustDeal.Rows[rowIndex].Cells[3].Value.ToString();
            txtConNo.Text= dbGridCustDeal.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text= dbGridCustDeal.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void txtContactNo_Load(object sender, EventArgs e)
        {
            DataTable dt = d.Selected();
            dbGridCustDeal.DataSource = dt;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.CustDealID = Convert.ToInt32(txtCustDealId.Text);
            c.Name = txtName.Text;
            c.Type = cmBCustDeal.Text;
            c.Email = txtEmail.Text;
            c.ContactNO = txtConNo.Text;
            c.AddedDate = DateTime.Now;
            c.Address = txtAddress.Text;

            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;
            BlUser usr = a.getIDfromUserName(loggedUser);
            c.AddedBy = usr.Id;
            bool Success = d.UpdUser(c);
            if (Success == true)
            {
                MessageBox.Show("Update Successfully");
                Cleantxt();
                DataTable dt = d.Selected();
                dbGridCustDeal.DataSource = dt;
                Cleantxt();
            }
            else
            {
                MessageBox.Show("Update Failed");
            }
     }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = d.Search(keywords);
                dbGridCustDeal.DataSource = dt;
            }
            else
            {
                DataTable dt = d.Selected();
                dbGridCustDeal.DataSource = dt;
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            c.CustDealID = Convert.ToInt32(txtCustDealId.Text);
            bool Success = d.DeleUser(c);
            if (Success == true)
            {
                MessageBox.Show("Delete Successfully");
                Cleantxt();
                DataTable dt = d.Selected();
                dbGridCustDeal.DataSource = dt;
                Cleantxt();

            }
            else
            {
                MessageBox.Show("Delete Failed");
            }

            }
    }
}
