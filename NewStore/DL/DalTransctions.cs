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
    class DalTransctions
    {
        static string myConnString = ConfigurationManager.ConnectionStrings["ConnStrng"].ConnectionString;

        #region Insert data into tbl_transction
        public bool InsTran(BlTransction trn,out int trnID)
        {
            bool isSuccess = false;
            trnID = -1;
            SqlConnection con = new SqlConnection(myConnString);
            try
            {
                string sql = "INSERT INTO tbl_transction(Type,Dea_Cust_Id,Grand_Total,TransctionDate,Tax,Discount,AddedBy) VALUES(@Type,@Dea_Cust_Id,@Grand_Total,@Transction_Date,@Tax,@Discount,@AddedBy); SELECT @@IDENTITY;";
              //string sql = "INSERT INTO tbl_transction(Dea_Cust_Id,Grand_Total,TransctionDate,Tax,Discount,AddedBy) VALUES(@Dea_Cust_Id,@Grand_Total,@Transction_Date,@Tax,@Discount,@AddedBy)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Type", trn.Type);
                cmd.Parameters.AddWithValue("@Dea_Cust_Id", trn.CustomerID);
                cmd.Parameters.AddWithValue("@Grand_Total", trn.GrandTotal);
                cmd.Parameters.AddWithValue("@Transction_Date", trn.TransctionDate);
                cmd.Parameters.AddWithValue("@Tax", trn.Tax);
                cmd.Parameters.AddWithValue("@Discount", trn.Discount);
                cmd.Parameters.AddWithValue("@AddedBy", trn.AddedBy);

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
                                
                con.Open();

                //object x = (int)cmd.ExecuteNonQuery();
                object o = cmd.ExecuteScalar();
                object x = cmd.Parameters["@id"].Value;
                //Int32 o = Convert.ToInt32(cmd.ExecuteScalar());
                //Int32 row = Convert.ToInt32(cmd.ExecuteScalar());



                //MessageBox.Show(o.GetType().ToString());


                //MessageBox.Show(o.GetType());

                if (o!=null)
                {
                    trnID = int.Parse(o.ToString());
                    MessageBox.Show(trnID.ToString());
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
