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
    class DalCustDeal
    {
        static string myconnStrng = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;
        #region Select Data from Category
        public DataTable Selected()
        {
            SqlConnection Conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "select * from tbl_Dea_Cust";
                SqlCommand cmd = new SqlCommand(sql, Conn);
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
                string sql = "select * from tbl_Dea_Cust where Id like '%" + keywords + "%' or Name like '%" + keywords + "%' or Type Like '%" + keywords + "%'";

                //MessageBox.Show(sql);
                SqlCommand cmd = new SqlCommand(sql, Conn);
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
        public bool InsUser(BL.BlCustDeal c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                string strsql = "INSERT INTO tbl_Dea_Cust(Type,Name,eMail,Contact,Address,AddedDate,AddedBy) VALUES(@Type,@name,@eMail,@Contact,@Address,@AddedDate,@AddedBy)";

                SqlCommand cmd = new SqlCommand(strsql, con);
                cmd.Parameters.AddWithValue("@Type", c.Type);
                cmd.Parameters.AddWithValue("@Name", c.Name);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Contact", c.ContactNO);
                cmd.Parameters.AddWithValue("Address", c.Address);
                cmd.Parameters.AddWithValue("@AddedDate", c.AddedDate);
                cmd.Parameters.AddWithValue("@AddedBy", c.AddedBy);
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
        #region Update customer dealer table 
        public bool UpdUser(BL.BlCustDeal b)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {

                string sql = "UPDATE tbl_Dea_Cust set Type=@Type,Name=@Name,eMail=@eMail,Contact=@Contact,Address=@Address,AddedDate=@AddedDate,AddedBy=@AddedBy  where Id=@Id ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Type", b.Type);
                cmd.Parameters.AddWithValue("@Name", b.Name);
                cmd.Parameters.AddWithValue("@eMail", b.Email);
                cmd.Parameters.AddWithValue("@Contact", b.ContactNO);
                cmd.Parameters.AddWithValue("@Address", b.Address);
                cmd.Parameters.AddWithValue("@AddedDate", b.AddedDate);
                cmd.Parameters.AddWithValue("@AddedBy", b.AddedBy);
                cmd.Parameters.AddWithValue("@Id", b.CustDealID);
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
        #region Deletete Customer or Deler from the table
            public bool DeleUser(BL.BlCustDeal c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                string sql = "DELETE FROM tbl_Dea_Cust where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", c.CustDealID);
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
        #region find the Dealer or Customer when text changed event fired
        public BlCustDeal SearchDelCusforTransction(string keyword)
        {
            BlCustDeal dc = new BlCustDeal();
            SqlConnection conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "select Name,eMail,Contact,Address from tbl_Dea_Cust where Id like '%" + keyword + "%' or Name like '%" + keyword + "%'";
                SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
                conn.Open();
                adap.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dc.Name = dt.Rows[0]["Name"].ToString();
                    dc.Email = dt.Rows[0]["eMail"].ToString();
                    dc.ContactNO= dt.Rows[0]["Contact"].ToString();
                    dc.Address = dt.Rows[0]["Address"].ToString();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dc;
        }
        #endregion
        #region Get the ID of Delaer or customer Base on the Name
        public BlCustDeal GetCustIDfromName(string Name)
        {
            //Create a Cust Object 
            BlCustDeal cus = new BlCustDeal();

            SqlConnection Con = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "select Id from tbl_Dea_Cust where Name='"+Name+"'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, Con);
                Con.Open();
                adp.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    cus.CustDealID = int.Parse(dt.Rows[0]["id"].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();

            }
            return cus;

        }


        #endregion







    }
}


