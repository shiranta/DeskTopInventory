using NewStore.BL;
using NewStore.DL;
using NewStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace NewStore.UI
{
    public partial class frmPurchaseAndSales : Form
    {

        DataTable tranDt = new DataTable();
        DalTransctions dalTranHeader = new DalTransctions();
        DalTrnDetail dalTranDetail = new DalTrnDetail();
        BlTransctionDetail trndetail =new BlTransctionDetail();
        /*DalProduct Dprd = new DalProduct();
        BlProduct bprd = new BlProduct();
        DalProduct dalprd = new DalProduct();*/


        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        //DataTable tranDt = new DataTable();




        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();

        }


        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            string type = frmUserAdmin.TrnType;
            lblPanel.Text = type;
            // DataProductGrid.ColumnCount = 4;

            /*DataProductGrid.Columns[0].Name = "Product Name";
            DataProductGrid.Columns[1].Name = "Quantity";
            DataProductGrid.Columns[2].Name = "Rate";
            DataProductGrid.Columns[3].Name = "Total";*/

            tranDt.Columns.Add("Product Name", typeof(string));
            tranDt.Columns.Add("Quantity", typeof(decimal));
            tranDt.Columns.Add("Rate", typeof(decimal));
            tranDt.Columns.Add("LineTot", typeof(decimal));
            txtVat.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtGrandTotal.Text = "0.00";
            txtPaidAmount.Text = "0.00";

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string schword = txtSearch.Text;
            if (txtSearch.Text == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";


            }
            DalCustDeal dl = new DalCustDeal();
            BlCustDeal cs = dl.SearchDelCusforTransction(schword);
            txtName.Text = cs.Name;
            txtContact.Text = cs.ContactNO;
            txtAddress.Text = cs.Address;
            txtEmail.Text = cs.Email;

            
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            string SerchPrd = txtProduct.Text;
            if (txtProduct.Text == "")
            {
                txtPrdName.Text = "";
                txtRate.Text = "";
                txtQty.Text = "1.00";
                return;
            }

            BlProducts p = new BlProducts();
            DalProduct dp = new DalProduct();
            p = dp.GetProductDetail(SerchPrd);
            txtPrdName.Text = p.Name;
            txtRate.Text = p.Rate.ToString();
            txtInventory.Text = p.Quantity.ToString();

        }

        private void txtPrdName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ProductName = txtPrdName.Text;
            decimal Quantity = Decimal.Parse(txtQty.Text);
            decimal rate = decimal.Parse(txtRate.Text);
            decimal LineTotal =Math.Round(Quantity * rate,2);



            decimal SubTotal = Math.Round(Decimal.Parse(txtSubTotal.Text),2);
            SubTotal = SubTotal + LineTotal;

            if (ProductName == "")
            {
                MessageBox.Show("Product name can not be empty");
                txtProduct.Focus();

            }
            else
            {

                tranDt.Rows.Add(ProductName, Quantity, rate, LineTotal);

                //tranDt.Rows.Add(ProductName);

                txtSubTotal.Text = Math.Round(SubTotal,2).ToString();

                DataProductGrid.DataSource = tranDt;

                txtProduct.Text = "";
                txtPrdName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "";
                txtQty.Text = "1.00";
                txtProduct.Focus();



            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string value = txtDiscount.Text;
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            if (value == "")
            {
                MessageBox.Show("Please enter the Discount Rate");

            }
            else
            {
               decimal Discount = decimal.Parse(txtDiscount.Text);
                decimal grandtotal = ((100 - Discount) / 100) * subTotal;
                //txtGrandTotal.Text =Math.Round(grandtotal.ToString(),2);
                txtGrandTotal.Text = Math.Round(grandtotal,2).ToString();

            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string GrandTotCheck = string.IsNullOrWhiteSpace(txtGrandTotal.Text) ? "0.00" : txtGrandTotal.Text;
            txtGrandTotal.Text = GrandTotCheck;
            if (GrandTotCheck == "")
            {
                MessageBox.Show("Calculate Discount and set the Grand Total");
            }
            else
            {
                decimal prvGtot = decimal.Parse(txtGrandTotal.Text);
                decimal vat = string.IsNullOrWhiteSpace(txtVat.Text) ? 0 : decimal.Parse(txtVat.Text);
                decimal GtotwithVat = ((100 + vat) / 100) * prvGtot;
                txtGrandTotal.Text = Math.Round(GtotwithVat,2).ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            string paidamount = txtPaidAmount.Text;
            if (paidamount == "")
            {
                MessageBox.Show("Paid Amount should be positive");
            }

            else
            {
                decimal Gtot = decimal.Parse(txtGrandTotal.Text);
                decimal payAmount = decimal.Parse(txtPaidAmount.Text);
                decimal retAmt = payAmount - Gtot;
                txtReturnAmount.Text = Math.Round(retAmt,2).ToString();
            }
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //Get the data from Transction Sales form
            BlTransction tran = new BlTransction();
            //tran.Type = lblPanel.Text;
            tran.Type = 99;
            //Get the Customer or Dealer ID
            string Dea_Cust = txtName.Text;
            DalCustDeal dc = new DalCustDeal();
            BlCustDeal bc = dc.GetCustIDfromName(Dea_Cust);
            tran.CustomerID = bc.CustDealID;
            tran.GrandTotal = Math.Round(Decimal.Parse(txtGrandTotal.Text),2);

            tran.TransctionDate = DateTime.Now;
            tran.Tax = Math.Round(decimal.Parse(txtVat.Text),2);
            tran.Discount = Math.Round(decimal.Parse(txtDiscount.Text),2);

            //Get the Named of Logged User 
            string userName = frmLogins.loggIned;
            DataAccess dal = new DataAccess();

            BlUser b = dal.getIDfromUserName(userName);
            

            try
            {
                tran.AddedBy = b.Id;

                tran.TransctionDetail = tranDt;
                //Letus create a boolean variable and set it to false
                bool sucess = false;
                //Actual code to insert Transction and transction Detail

                using (TransactionScope scope = new TransactionScope())
                {
                    int transctionId = -1;
                    bool w = dalTranHeader.InsTran(tran, out transctionId);

                    MessageBox.Show(transctionId.ToString());
                    trndetail.HeaderID = transctionId;

                    for (int i = 0; i < tranDt.Rows.Count; i++)

                    {
                        //Get the all detail of  the products
                        DalProduct dalp = new DalProduct();
                        BlProducts pdt = new BlProducts();
                        
                        string prdname = tranDt.Rows[i][0].ToString();
                        pdt = dalp.GetProdIdfromName(prdname);

                        trndetail.ProductId = pdt.ProductId;
                        trndetail.Quantity = Math.Round(decimal.Parse(tranDt.Rows[i][1].ToString()), 2);
                        trndetail.Rate = Math.Round(decimal.Parse(tranDt.Rows[i][2].ToString()), 2);
                        trndetail.Total = Math.Round(decimal.Parse(tranDt.Rows[i][3].ToString()), 2);
                        trndetail.CustomerID = bc.CustDealID;
                        trndetail.AddedDate = DateTime.Now;
                        trndetail.AddedBy = b.Id;
                        

                        bool x = dalTranDetail.InsTrnDetail(trndetail);
                    }
                    scope.Complete();
                }   
            } 
            catch (Exception err) {
                
            }
        }

    }
}

