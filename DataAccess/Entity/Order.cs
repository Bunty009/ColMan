using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Order
    {
        public int OrderId { get; set; }    	
        public DateTime OrderDate { get; set; } 
 
	    public string BillToContact { get; set; }
        public string BillToGSTN { get; set; }
        public string BillToAddress1 { get; set; }
        public string BillToAddress2 { get; set; }
        public string BillToCity { get; set; }	
        public string BillToState { get; set; }	
        public int BillToZip { get; set; }	
        public string BillToPhone { get; set; }	
        public string BillToEmail { get; set; }

        public List<OrderItems> Products { get; set; }

        public int Discount { get; set; }
        public int CGSTRate { get; set; }
        public double CGSTAmount { get; set; }
        public int SGSTRate { get; set; }
        public double SGSTAmount { get; set; }
        public int IGSTRate { get; set; }
        public double IGSTAmount { get; set; }

        public double TotalProductPrice { get; set; }
        public double TaxablePrice { get; set; }
        public double Grandtotal { get; set; }
        public string GrandtotalInWords { get; set; }

	    public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }	
        	
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
                
    }

    public class OrderItems 
    {
        public int OrderItemId { get; set; }
 	    public int OrderId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string HSNCode { get; set; }
        public int Quantity { get; set; }	
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }

}
