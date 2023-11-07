using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.BL
{
    class BlTransction
    {
        public int ID { get; set; }
        public int  Type { get; set; }
        public string  DelerCustomerID { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime TransctionDate { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Discount { get; set; }
        public int AddedBy { get; set; }
        public DataTable  TransctionDetail { get; set; }
        public int CustomerID { get; set; }
    }
}
