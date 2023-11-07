using NewStore.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewStore.DL
{
    public class LogingDataAccess
    {
        static string myconnStrng = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;
        #region Loging into the User dashboard or AdminDashBoard 

        public bool LogingUser(BlLoging l)
        {
            bool isSuccess = false;
            System.Data.SqlClient.SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                //string sql = "SELECT * FROM tbl_users where UserName=@userName and Password=@Password and UserType=@UserType";
                //MessageBox.Show(sql);
                //Front end sql statment replace with  store procedure
                //SqlCommand cmd = new SqlCommand(sql, con);
                //In order to get the result frpm store procedure, we input  2 parameters into the command object
                //1.Name of the sp and connection . Then identify the commmand type

                //(1).SqlCommand cmd = new SqlCommand("sp_UserLogin", con);(2).cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlCommand cmd = new SqlCommand("sp_UserLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", l.UserName);
                cmd.Parameters.AddWithValue("@Password", l.Paasword);
                cmd.Parameters.AddWithValue("@UserType", l.UserType);


                con.Open();
                Int32 row = Convert.ToInt32(cmd.ExecuteScalar());

                if (row > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }
            return isSuccess;

        }
        #endregion


    }
}
