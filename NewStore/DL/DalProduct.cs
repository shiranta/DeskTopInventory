using NewStore.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace NewStore.DL
{
    class DalProduct
    {
        static string myconnStrng = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;

        #region Select data from tbl_Product
        public DataTable Select()
        {
            SqlConnection Conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();


            try
            {
                string sql = "select * from tbl_Products";
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
        #region Capturing Search String
        public DataTable Search(string keywords)
        {
            SqlConnection Conn = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "select * from tbl_Products where Id like '%" + keywords + "%' or Name like '%" + keywords + "%' or Description Like '%" + keywords + "%'";

                MessageBox.Show(sql);
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
        #region Data insert into tbl_products
        public bool InsUser(BL.BlProducts u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                string strsql = "INSERT INTO tbl_Products(Name,Categories,Description,Rate,AddedDate,AddedBy) VALUES(@Name,@Categories,@Description,@Rate,@AddedDate,@AddedBy)";
                SqlCommand cmd = new SqlCommand(strsql, con);
                cmd.Parameters.AddWithValue("@Name", u.Name);
                cmd.Parameters.AddWithValue("@Categories", u.Category);
                cmd.Parameters.AddWithValue("@Description", u.Description);
                cmd.Parameters.AddWithValue("@Rate", u.Rate);
                cmd.Parameters.AddWithValue("@AddedDate", u.AddedDate);
                cmd.Parameters.AddWithValue("@AddedBy", u.AddedBy);

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
        #region Data update into tbl_Products
        public bool UpdUser(BL.BlProducts b)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                string sql = "UPDATE tbl_Products set Name=@Name,Categories=@Categories,Description=@Description,Rate=@Rate,AddedDate=@AddedDate,AddedBy=@AddedBy  where Id=@Id ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", b.Name);
                cmd.Parameters.AddWithValue("@Categories", b.Category);
                cmd.Parameters.AddWithValue("@Description", b.Description);
                cmd.Parameters.AddWithValue("@Rate", b.Rate);
                cmd.Parameters.AddWithValue("@AddedDate", b.AddedDate);
                cmd.Parameters.AddWithValue("@AddedBy", b.AddedBy);
                cmd.Parameters.AddWithValue("@Id", b.ProductId);
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
        #region Delete Item from Delete Table
        public bool DeleUser(BL.BlProducts u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnStrng);
            try
            {
                string sql = "DELETE FROM tbl_Products where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", u.ProductId);
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
        #region Product Detail Search When Search text changed on Purchase/sales form
        public BlProducts GetProductDetail(string keyword)
        {
            BlProducts pd = new BlProducts();
            DataTable dt = new DataTable();
            SqlConnection Conn = new SqlConnection(myconnStrng);
            //int x = int.TryParse(keyword,  out x);

            try
            {
                string sql = "select name,rate,qty from tbl_Products where Id like '%" + keyword + "%' or Name like '%" + keyword + "%'";
                //MessageBox.Show(sql);

                SqlDataAdapter adp = new SqlDataAdapter(sql, Conn);
                Conn.Open();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                    //MessageBox.Show("Hold");
                    pd.Name = dt.Rows[0]["Name"].ToString();
                /*pd.Rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                pd.Quantity = decimal.Parse(dt.Rows[0]["qty"].ToString()); */

                //pd.Rate = decimal.dt.Rows[0]["rate"].ToString();
                //pd.Quantity = int.Parse(dt.Rows[0]["qty"].ToString());
                if (!object.ReferenceEquals(dt.Rows[0]["rate"], DBNull.Value))
                {
                    pd.Rate = Convert.ToDecimal(dt.Rows[0]["rate"]);
                }
                else
                {
                    MessageBox.Show("Rate with Null Value");
                }

                if (!object.ReferenceEquals(dt.Rows[0]["qty"], DBNull.Value))
                {

                    pd.Quantity = Convert.ToDecimal(dt.Rows[0]["qty"]);
                }
                else
                {
                    MessageBox.Show("Qty with Null Value");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.Source);
            }
            finally
            {
                Conn.Close();

            }
            return pd;
        }

        #endregion
        #region Find Product ID Givng the the Product name
        public BlProducts GetProdIdfromName(string Name)
        {
            //Create a Cust Object 
            BlProducts prd = new BlProducts();
            SqlConnection Con = new SqlConnection(myconnStrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "select Id from tbl_Products where Name='" + Name + "'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, Con);
                Con.Open();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    prd.ProductId = int.Parse(dt.Rows[0]["id"].ToString());

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
            return prd;

            #endregion


        }
    }
}
