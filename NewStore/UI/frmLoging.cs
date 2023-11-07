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
    public partial class frmLogins : Form
    {
        BlLoging bl = new BlLoging();
        
        LogingDataAccess dal = new LogingDataAccess();
        public static  string loggIned;

              

        public frmLogins()
        {
            InitializeComponent();
            txtUser.Text = "ANTON";
            txtPassword.Text = "anton";
            cmbUser.SelectedIndex = 0;
        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            bl.UserName = txtUser.Text.Trim();
            bl.Paasword = txtPassword.Text.Trim();
            bl.UserType = cmbUser.Text.Trim();

            bool success = dal.LogingUser(bl);

            if (success == true)
            {
                MessageBox.Show("Login Successful");
                loggIned = bl.UserName;

                switch (bl.UserType)
                {
                    case "Admin":
                        {
                            frmAdminDashBoard admin = new frmAdminDashBoard();
                            admin.Show();
                            this.Hide();

                        }
                        break;
                    case "User":
                        {
                           
                            frmUserAdmin user = new frmUserAdmin();
                            user.Show();
                            this.Hide();
                        }
                        break;
                    default:
                        {
                            MessageBox.Show("Invalid User Type");
                        }
                        break;


                }

            }
            else
            {
                MessageBox.Show("Login Failed, Try Again ");
            }
           }
    }
}
