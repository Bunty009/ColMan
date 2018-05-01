using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class SupplierBAL
    {
        SupplierDAL supplierDAL = null;

        public SupplierBAL()
        {
            supplierDAL = new SupplierDAL();
        }

        public bool addSupplier(Supplier supplier)
        {
            return supplierDAL.addSupplier(supplier);
        }

        public bool updateSupplier(Supplier user)
        {
            return supplierDAL.updateSupplier(user);
        }

        public DataTable getSuppliers(object[] data)
        {
            return supplierDAL.getSuppliers(data);
        }

        public bool deleteSupplier(int SupplierId)
        {
            return supplierDAL.deleteSupplier(SupplierId);
        }
    }
}
