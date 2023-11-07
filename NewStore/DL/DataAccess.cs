
using NewStore.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewStore.DL
{
    class DataAccess
    {
        static string myconnStrng = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;
        #region Select Data from Database 
        /*
         * In order to get the result from DB we use the 5 SP such as Add,Amend,Delete, Select, Find Record etc
         * We have to write a one SP and separated it @ActionType rather than write 5 sp(s)
         * Intoduce the connection, them command object pass the parameter as Sp and Connection
         * Introduce the commanType as SP, then @ActionType
         * 
         */


        public DataTable Selected()
        {
            SqlConnection Conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();

            try
            {
                //string sql = "select * from tbl_Users";
                SqlCommand cmd = new SqlCommand("spUser", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "FetchData");
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                Conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conn.Close();
            }
            return dt;

        }

        #endregion

        #region capturing the Search String

        public DataTable Search(string keywords)
        {
            SqlConnection Conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();

            try
            {
                //string sql = "select * from tbl_Users where Id like '%" +keywords + "%' or FirstName like '%" + keywords + "%' or Lastname Like '%" + keywords + "%'";

                //MessageBox.Show(sql);
                SqlCommand cmd = new SqlCommand("spUser", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@keywords", keywords);
                cmd.Parameters.AddWithValue("@ActionType", "SearchData");
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                Conn.Open();
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conn.Close();
            }
            return dt;

        }

        #endregion

        #region Insert into user table
        public bool InsUser(BlUser u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                //string strsql = "INSERT INTO tbl_Users(FirstName,LastName,eMail,UserName,Password,Contact,Address,Gender,UserType,AddedDate,AddedBy) VALUES(@FirstName,@LastName,@eMail,@UserName,@Password,@Contact,@Address,@Gender,@UserType,@AddedDate,@AddedBy)";
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", u.FirstName);

                cmd.Parameters.AddWithValue("@LastName", u.LastName);
                cmd.Parameters.AddWithValue("@eMail", u.Email);
                cmd.Parameters.AddWithValue("@UserName", u.UserName);
                cmd.Parameters.AddWithValue("@Password", u.Password);
             
                cmd.Parameters.AddWithValue("@Contact", u.Contact);
                cmd.Parameters.AddWithValue("@Address", u.Address);
                cmd.Parameters.AddWithValue("@Gender", u.Gender);
                cmd.Parameters.AddWithValue("@UserType", u.UserType);
                cmd.Parameters.AddWithValue("@AddedDate", u.Adddate);
                cmd.Parameters.AddWithValue("@AddedBy", u.AddedBy);
                cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                con.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                
            }
            catch(Exception ex)
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

        #region Update tbl_users
        public bool UpdUser(BlUser b)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                //string sql = "UPDATE tbl_Users set FirstName=@FirstName,LastName=@LastName,eMail=@eMail,UserName=@UserName,Password=@Password,Contact=@Contact,Address=@Address,Gender=@Gender,UserType=@UserType,AddedDate=@AddedDate,AddedBy=@AddedBy where Id=@Id ";
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType= CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", b.FirstName);
                cmd.Parameters.AddWithValue("@LastName", b.LastName);
                cmd.Parameters.AddWithValue("@eMail", b.Email);
                cmd.Parameters.AddWithValue("@UserName", b.UserName);
                cmd.Parameters.AddWithValue("@Password", b.Password);
                cmd.Parameters.AddWithValue("@Contact", b.Contact);
                cmd.Parameters.AddWithValue("@Address", b.Address);
                cmd.Parameters.AddWithValue("@Gender", b.Gender);
                cmd.Parameters.AddWithValue("@UserType", b.UserType);
                cmd.Parameters.AddWithValue("@AddedDate", b.Adddate);
                cmd.Parameters.AddWithValue("@AddedBy", b.AddedBy);
                cmd.Parameters.AddWithValue("@Id", b.Id);
                cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                con.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
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
        #region Delete recoards tbl_Users

        public bool DeleUser(BlUser u)
        {
             bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            
            try
            {
                //string sql = "DELETE FROM tbl_Users where Id=@Id";

                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", u.Id);
                cmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                con.Open();

                int row = cmd.ExecuteNonQuery();
                

                if (row>0)
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
        #region Find the User Id for given User name

        public BlUser getIDfromUserName(string username)
        {
            BlUser u = new BlUser();
            SqlConnection conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();
            try
            {
                //string sql = "select ID from tbl_Users where UserName='" + username + "'";
                //MessageBox.Show(sql);

                SqlCommand cmd = new SqlCommand("spUser", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ActionType", "FindUserId");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                

                conn.Open();

                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    u.Id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return u;

        }
        #endregion








    }


}
