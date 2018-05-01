using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using System.Data;

namespace BAL
{
    public class CustomerBAL
    {
        CustomerDAL customerDAL = null;

        public CustomerBAL()
        {
            customerDAL = new CustomerDAL();
        }

        public bool addCustomer(Customer customer)
        {
            return customerDAL.addCustomer(customer);
        }

        public bool updateCustomer(Customer customer)
        {
            return customerDAL.updateCustomer(customer);
        }

        public DataTable getCustomers(object[] data)
        {
            return customerDAL.getCustomers(data);
        }

        public bool deleteCustomer(int CustomerId)
        {
            return customerDAL.deleteCustomer(CustomerId);
        }

        public DataTable GetCustomerForChallan(object[] data)
        {
            return customerDAL.GetCustomerForChallan(data);
        }

        public DataTable getCustomersForExcel()
        {
            return customerDAL.getCustomersForExcel();
        }
        
        public bool uploadCustomersForExcel()
        {
            return customerDAL.uploadCustomersForExcel();
        }

    }
}
