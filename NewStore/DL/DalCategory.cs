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
    class DalCategory
    {
        static string myconnStrng = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;
        #region Select Data from Category
        public DataTable Selected()
        {
            SqlConnection Conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();

            try
            {
                //string sql = "select * from tbl_Categories";
                SqlCommand cmd = new SqlCommand("spCategory", Conn);
                //SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "FetchData");
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ActionType", "FetchData");
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
                //string sql = "select * from tbl_Categories where Id like '%" + keywords + "%' or Title like '%" + keywords + "%' or Description Like '%" + keywords + "%'";


                

                //MessageBox.Show(sql);
                SqlCommand cmd = new SqlCommand("spCategory", Conn);
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
        #region Insert data into Category Table
        public bool InsUser(BL.BlCategory u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                //string strsql = "INSERT INTO tbl_Categories(Title,Description,AddedDate,AddedBy) VALUES(@Title,@Description,@AddedDate,@AddedBy)";
                //SqlCommand cmd = new SqlCommand(strsql, con);
                SqlCommand cmd = new SqlCommand("spCategory", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", u.Title);
                cmd.Parameters.AddWithValue("@Description", u.Description);
                cmd.Parameters.AddWithValue("@AddedDate", u.AddedDate);
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

        #region Update Category 
        public bool UpdUser(BL.BlCategory b)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                //string sql = "UPDATE tbl_Categories set Title=@Title,Description=@Description,AddedDate=@AddedDate,AddedBy=@AddedBy  where Id=@Id ";
                //SqlCommand cmd = new SqlCommand(sql, con);
                SqlCommand cmd = new SqlCommand("spCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title",b.Title);
                cmd.Parameters.AddWithValue("@Description", b.Description);
                cmd.Parameters.AddWithValue("@AddedDate", b.AddedDate);
                cmd.Parameters.AddWithValue("@AddedBy", b.AddedBy);
                cmd.Parameters.AddWithValue("@Id", b.CatId);
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

        #region Delete data from the category table
        public bool DeleUser(BL.BlCategory u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                //string sql = "DELETE FROM tbl_Categories where Id=@Id";

                //SqlCommand cmd = new SqlCommand(sql, con);
                SqlCommand cmd = new SqlCommand("spCategory", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", u.CatId);
                cmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                con.Open();

                int row = cmd.ExecuteNonQuery();


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
