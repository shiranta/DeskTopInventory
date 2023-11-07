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
    public partial class frmUsers : Form
    {
        BlUser b = new BlUser();
        DataAccess dal = new DataAccess();

        public frmUsers()
        {
            InitializeComponent();
            
        }
        
               
                
        
        
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            

            b.FirstName = txtFirstName.Text;
            b.LastName = txtLastName.Text;
            b.Email = txtEmail.Text;
            b.UserName = txtUserName.Text;
            b.Password = txtPassword.Text;
            b.Contact = txtContactNo.Text;
            b.Address = txtAddress.Text;
            b.Gender = cmbGender.Text;
            b.UserType = cmbUserCat.Text;
            b.Adddate = DateTime.Now;
            //Getting user Name of the logged user
            string loggedUser = frmLogins.loggIned;

            BlUser usr = dal.getIDfromUserName(loggedUser);
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
            dataGridUser.DataSource = dt;
            Cleantxt();
            
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Selected();
            dataGridUser.DataSource = dt;
            
        }

        private void Cleantxt()
        {
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";

        }

        private void dataGridUser_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtID.Text = dataGridUser.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text= dataGridUser.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text= dataGridUser.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text= dataGridUser.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text= dataGridUser.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text= dataGridUser.Rows[rowIndex].Cells[5].Value.ToString();
            txtContactNo.Text= dataGridUser.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text= dataGridUser.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text= dataGridUser.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserCat.Text= dataGridUser.Rows[rowIndex].Cells[9].Value.ToString();


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void tnUpdate_Click(object sender, EventArgs e)
        {
            b.Id = Convert.ToInt32(txtID.Text);
            b.FirstName = txtFirstName.Text;
            b.LastName = txtLastName.Text;
            b.Email = txtEmail.Text;
            b.UserName = txtUserName.Text;
            b.Password = txtPassword.Text;
            b.Contact = txtContactNo.Text;
            b.Address = txtAddress.Text;
            b.Gender = cmbGender.Text;
            b.UserType = cmbUserCat.Text;
            b.Adddate = DateTime.Now;
            b.AddedBy = 1;

            bool Success = dal.UpdUser(b);
            if (Success == true)
            {
                MessageBox.Show("Update Successfully");
                Cleantxt();
                DataTable dt = dal.Selected();
                dataGridUser.DataSource = dt;


            }
            else
            {
                MessageBox.Show("Update Failed");
            }




        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            b.Id = Convert.ToInt32(txtID.Text);
            bool Success = dal.DeleUser(b);
            if (Success == true)
            {
                MessageBox.Show("Delete Successfully");
                Cleantxt();
                DataTable dt = dal.Selected();
                dataGridUser.DataSource = dt;
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
                dataGridUser.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Selected();
                dataGridUser.DataSource = dt;
            }

        }
    }

    
}
