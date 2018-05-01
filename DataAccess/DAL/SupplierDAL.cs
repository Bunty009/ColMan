using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SupplierDAL
    {
        SqlParameter[] parameters;

        internal bool addSupplier(Supplier supplier)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@pName",supplier.Name ),
                    new SqlParameter("@pEmailId",supplier.EmailId ),
                    new SqlParameter("@pPhoneNo",supplier.PhoneNo ),
                    new SqlParameter("@pGSTN",supplier.GSTN ),
                    new SqlParameter("@pAddressLine1",supplier.AddressLine1 ),
                    new SqlParameter("@pAddressLine2", supplier.AddressLine2),
                    new SqlParameter("@pCity", supplier.City),
                    new SqlParameter("@pState", supplier.State), 
                    new SqlParameter("@pZipCode", supplier.ZipCode), 
                    new SqlParameter("@pIsActive", supplier.IsActive),
                    new SqlParameter("@pCreatedBy", supplier.CreatedBy),
                };
                supplier.SupplierId = Convert.ToInt32(SqlDBHelper.ExecuteNonQueryReturnData("app_AddSuppliers", CommandType.StoredProcedure, parameters, "pSupplierId"));
                if (supplier.SupplierId == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal bool updateSupplier(Supplier supplier)
        {
            try
            {
                parameters = new SqlParameter[] 
                {
                    new SqlParameter("@pSupplierId",supplier.SupplierId ),
                    new SqlParameter("@pName",supplier.Name ),
                    new SqlParameter("@pEmailId",supplier.EmailId ),
                    new SqlParameter("@pPhoneNo",supplier.PhoneNo ),
                    new SqlParameter("@pGSTN",supplier.GSTN ),
                    new SqlParameter("@pAddressLine1",supplier.AddressLine1 ),
                    new SqlParameter("@pAddressLine2", supplier.AddressLine2),
                    new SqlParameter("@pCity", supplier.City),
                    new SqlParameter("@pState", supplier.State), 
                    new SqlParameter("@pZipCode", supplier.ZipCode),
                    new SqlParameter("@pModifiedBy", supplier.ModifiedBy),
                };
                return SqlDBHelper.ExecuteNonQuery("app_UpdateSuppliers", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

        internal DataTable getSuppliers(object[] data)
        {
            string supplierId = "", searchKey = "";
            try
            {
                supplierId = Convert.ToString(data[0]);
            }
            catch { supplierId = ""; }
            try
            {
                searchKey = Convert.ToString(data[1]);
            }
            catch { }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pSupplierId", supplierId),
                new SqlParameter("@pSearchKey", searchKey)
            };
            DataTable table = new DataTable();
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetSuppliers", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }

        internal bool deleteSupplier(int SupplierId)
        {
            try
            {
                parameters = new SqlParameter[] 
				{
					new SqlParameter("@pSupplierId",SupplierId)     
				};
                return SqlDBHelper.ExecuteNonQuery("app_DeleteSupplier", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

    }
}
