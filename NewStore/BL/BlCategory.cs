using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.BL
{
    class BlCategory
    {
        public int CatId { get; set; }
        public String Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }

        public int AddedBy { get; set; }
    }
}
