using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.BL
{
    class BlTransctionDetail
    {
        public int ID { get; set; }
        public int ProductId { get; set; }

        public Decimal Rate { get; set; }

        public decimal Quantity { get; set; }
        public decimal Total { get; set; }

        public string DealerCustomer { get; set; }
        public DateTime AddedDate { get; set; }

        public int AddedBy { get; set; }
        public int CustomerID { get; set; }

        public int HeaderID { get; set; }


    }
}
