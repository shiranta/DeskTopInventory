using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.BL
{
    class BlCustDeal
    {
        public int CustDealID { get; set; }
        public String Name { get; set; }

        public String Email { get; set; }

        public String ContactNO { get; set; }
        public String Address { get; set; }

        public String Type { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

    }
}
