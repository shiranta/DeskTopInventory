using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.BL
{
    class BlProducts
    {
        public int ProductId { get; set; }
        public String Name { get; set; }
        public String Category { get; set; }
        public String Description { get; set; }
        public Decimal  Rate { get; set; }
        public Decimal Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }

    }
}
