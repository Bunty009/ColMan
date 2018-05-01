using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Price
    {
        public int PriceId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }

        public string OneKGSize { get; set; }
        public double OneKGPrice { get; set; }
        public string HalfKGSize { get; set; }
        public double HalfKGPrice { get; set; }
        public string Size1 { get; set; }
        public string Size2 { get; set; }
        public string Size3 { get; set; }
        public string Size4 { get; set; }
        public string Size5 { get; set; }
        public string Size6 { get; set; }
        public string Size7 { get; set; }
        public string Size8 { get; set; }
        public string Size9 { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public double Price3 { get; set; }
        public double Price4 { get; set; }
        public double Price5 { get; set; }
        public double Price6 { get; set; }
        public double Price7 { get; set; }
        public double Price8 { get; set; }
        public double Price9 { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
