using NewStore.BL;
using NewStore.DL;
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
    public partial class frmProducts : Form
    {
        BlProducts pl = new BlProduct();
        DalProduct da = new DalProduct();
        DataAccess d = new DataAccess();
        DalCategory c = new DalCategory();




        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
            {
            pl.Name = txtName.Text;
            pl.Category = cmbCat.Text;
            pl.Description = txtDesc.Text;
            pl.Rate = decimal.Parse(txtRate.Text);
            pl.AddedDate = DateTime.Now;

            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;
            BlUser usr = d.getIDfromUserName(loggedUser);
            pl.AddedBy = usr.Id;
            bool success = da.InsUser(pl);
            if (success == true)
            {
                MessageBox.Show("Product added successfully");
            }
            else
            {
                MessageBox.Show("Product  added Failed");

            }
            DataTable dt = da.Select();
            dbProductGrid.DataSource = dt;
            Cleantxt();
            
        }
        private void Cleantxt()
        {
            txtPrdId.Text = "";
            txtName.Text = "";
            txtDesc.Text = "";
            txtRate.Text = "";
            txtFind.Text = "";
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           pl.ProductId = Convert.ToInt32(txtPrdId.Text);
            pl.Name = txtName.Text;
            pl.Category = cmbCat.Text;
            pl.Description = txtDesc.Text;
            pl.Rate = decimal.Parse(txtRate.Text);
            pl.AddedDate = DateTime.Now;

            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;
            BlUser usr = d.getIDfromUserName(loggedUser);
            pl.AddedBy = usr.Id;
            bool success = da.UpdUser(pl);
            if (success == true)
            {
                MessageBox.Show("Product Update Successfully");
                Cleantxt();
                DataTable dt = da.Select();
                dbProductGrid.DataSource = dt;
                Cleantxt();


            }
            else
            {
                MessageBox.Show("Product Update Failed");
            }

        }

        private void dbProductGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtPrdId.Text = dbProductGrid.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dbProductGrid.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCat.Text = dbProductGrid.Rows[rowIndex].Cells[2].Value.ToString();
            txtDesc.Text = dbProductGrid.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dbProductGrid.Rows[rowIndex].Cells[4].Value.ToString();


        }

        private void dbProductGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           /* int rowIndex = e.RowIndex;
            txtPrdId.Text  =dbProductGrid.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text= dbProductGrid.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCat.Text= dbProductGrid.Rows[rowIndex].Cells[2].Value.ToString();
            txtDesc.Text= dbProductGrid.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text= dbProductGrid.Rows[rowIndex].Cells[4].Value.ToString();*/
            
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable dt = da.Select();
            dbProductGrid.DataSource = dt;
            Cleantxt();

            DataTable catTd = c.Selected();
            cmbCat.DataSource = catTd;
            cmbCat.DisplayMember = "Title";
            cmbCat.ValueMember = "id";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            pl.ProductId = Convert.ToInt32(txtPrdId.Text);
            bool Success = da.DeleUser(pl);
            if (Success == true)
            {
                MessageBox.Show("Delete Successfully");
                Cleantxt();
                DataTable dt = da.Select();
                dbProductGrid.DataSource = dt;
                Cleantxt();

            }
            else
            {
                MessageBox.Show("Delete Failed");
            }




        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtFind.Text;
            if (keywords != null)
            {
                DataTable dt = da.Search(keywords);
                dbProductGrid.DataSource = dt;
            }
            else
            {
                DataTable dt = da.Select();
                dbProductGrid.DataSource = dt;
            }

        }
    }
}
