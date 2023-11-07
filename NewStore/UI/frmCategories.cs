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
    public partial class frmCategories : Form
    {
        BlCategory b = new BlCategory();
        DalCategory dal =new DalCategory();
        DataAccess d = new DataAccess();


        public frmCategories()
        {
            InitializeComponent();
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Selected();
            DbCatGrid.DataSource = dt;


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{

        //}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            b.Title = txtTitle.Text;
            b.Description = txtDescription.Text;
            b.AddedDate = DateTime.Now;
            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;
            BlUser usr = d.getIDfromUserName(loggedUser);
            b.AddedBy = usr.Id;


            bool Success = dal.InsUser(b);

            if (Success == true)
            {
                MessageBox.Show("Record added successfully");
            }
            else
            {
                MessageBox.Show("Recoard added Failed");

            }
            DataTable dt = dal.Selected();
            DbCatGrid.DataSource = dt;
            Cleantxt();
            }
        private void Cleantxt()
        {
            txtCategory.Text = "";
            txtDescription.Text = "";
            txtTitle.Text = "";
            txtSearch.Text = "";
        }

        private void DbCatGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DbCatGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void DbCatGrid_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtCategory.Text = DbCatGrid.Rows[rowIndex].Cells[0].Value.ToString();
            txtTitle.Text = DbCatGrid.Rows[rowIndex].Cells[1].Value.ToString();
            txtDescription.Text = DbCatGrid.Rows[rowIndex].Cells[2].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            b.CatId = Convert.ToInt32(txtCategory.Text);
            b.Title = txtTitle.Text;
            b.Description = txtDescription.Text;
            b.AddedDate = DateTime.Now;
            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;
            BlUser usr = d.getIDfromUserName(loggedUser);
            b.AddedBy = usr.Id;
            
            bool Success = dal.UpdUser(b);
            if (Success == true)
            {
                MessageBox.Show("Update Successfully");
                Cleantxt();
                DataTable dt = dal.Selected();
                DbCatGrid.DataSource = dt;
                Cleantxt();


            }
            else
            {
                MessageBox.Show("Update Failed");
            }

        }

        private void btnDele_Click(object sender, EventArgs e)
        {
            b.CatId = Convert.ToInt32(txtCategory.Text);
            bool Success = dal.DeleUser(b);
            if (Success == true)
            {
                MessageBox.Show("Delete Successfully");
                Cleantxt();
                DataTable dt = dal.Selected();
                DbCatGrid.DataSource = dt;
                Cleantxt();

            }
            else
            {
                MessageBox.Show("Delete Failed");
            }




        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                DbCatGrid.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Selected();
                DbCatGrid.DataSource = dt;
            }

        }
    }
}
