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
    class DalTrnDetail
    {
        static string myConnString = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;

        #region Insert data into tbl_Transction Details
        public bool InsTrnDetail(BL.BlTransctionDetail td)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myConnString);
            try
            {
                string sql = "INSERT INTO tbl_transction_Detail(Product_Id,Rate,Qty,Total,Dea_Cust_ID,Added_Date,Added_By,TransHeader_Id) VALUES(@Prod_ID,@Rate,@Qty,@Total,@Dea_Cust_ID,@Added_Date,@Added_By,@TransHeader_Id)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Prod_ID",td.ProductId );
                cmd.Parameters.AddWithValue("@Rate",td.Rate );
                cmd.Parameters.AddWithValue("@Qty",td.Quantity );
                cmd.Parameters.AddWithValue("@Total", td.Total);
                cmd.Parameters.AddWithValue("@Dea_Cust_ID",td.CustomerID);
                cmd.Parameters.AddWithValue("@Added_Date",td.AddedDate);
                cmd.Parameters.AddWithValue("@Added_By", td.AddedBy);
                cmd.Parameters.AddWithValue("@TransHeader_Id", td.HeaderID);

                con.Open();

                int nrow = cmd.ExecuteNonQuery();
                

                if (nrow>0)
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


    }
    #endregion


}


