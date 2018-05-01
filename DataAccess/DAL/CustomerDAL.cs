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
    public class CustomerDAL
    {
        SqlParameter[] parameters;

        internal bool addCustomer(Customer customer)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@pName",customer.Name ),
                    new SqlParameter("@pEmailId",customer.EmailId ),
                    new SqlParameter("@pPhoneNo",customer.PhoneNo ),
                    new SqlParameter("@pGSTN",customer.GSTN ),
                    new SqlParameter("@pAddressLine1",customer.AddressLine1 ),
                    new SqlParameter("@pAddressLine2", customer.AddressLine2),
                    new SqlParameter("@pCity", customer.City),
                    new SqlParameter("@pState", customer.State), 
                    new SqlParameter("@pZipCode", customer.ZipCode), 
                    new SqlParameter("@pIsActive", customer.IsActive),
                    new SqlParameter("@pCreatedBy", customer.CreatedBy)
                };
                customer.CustomerId = Convert.ToInt32(SqlDBHelper.ExecuteNonQueryReturnData("app_AddCustomers", CommandType.StoredProcedure, parameters, "pCustomerId"));
                if (customer.CustomerId == 0)
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

        internal bool updateCustomer(Customer customer)
        {
            try
            {
                parameters = new SqlParameter[] 
                {
                    new SqlParameter("@pCustomerId",customer.CustomerId),
                    new SqlParameter("@pName",customer.Name ),
                    new SqlParameter("@pEmailId",customer.EmailId ),
                    new SqlParameter("@pPhoneNo",customer.PhoneNo ),
                    new SqlParameter("@pGSTN",customer.GSTN ),
                    new SqlParameter("@pAddressLine1",customer.AddressLine1 ),
                    new SqlParameter("@pAddressLine2", customer.AddressLine2),
                    new SqlParameter("@pCity", customer.City),
                    new SqlParameter("@pState", customer.State), 
                    new SqlParameter("@pZipCode", customer.ZipCode),
                    new SqlParameter("@pModifiedBy", customer.ModifiedBy)
                };
                return SqlDBHelper.ExecuteNonQuery("app_UpdateCustomers", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

        internal DataTable getCustomers(object[] data)
        {
            string customerId = "", searchKey = "";
            try
            {
                customerId = Convert.ToString(data[0]);
            }
            catch { customerId = null; }
            try
            {
                searchKey = Convert.ToString(data[1]);
            }
            catch { }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pCustomerId", customerId),
                new SqlParameter("@pSearchKey", searchKey)
            };
            DataTable table = new DataTable();
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetCustomers", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }

        internal bool deleteCustomer(int CustomerId)
        {
            try
            {
                parameters = new SqlParameter[] 
				{
					new SqlParameter("@pCustomerId",CustomerId)                   
								 
				};
                return SqlDBHelper.ExecuteNonQuery("app_DeleteCustomers", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }
        
        internal DataTable GetCustomerForChallan(object[] data)
        {
            string action = "", customerId = "";
            try
            {
                action = Convert.ToString(data[0]);
            }
            catch { }
            try
            {
                customerId = Convert.ToString(data[1]);
            }
            catch { customerId = "0"; }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pAction", action),
                new SqlParameter("@pCustomerId", customerId)
            };
            DataTable table = new DataTable();
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetCustomersForDdl", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }
        
        internal DataTable getCustomersForExcel()
        {
            DataTable table = new DataTable();
            parameters = new SqlParameter[]
            {
            };
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetCustomersForExcel", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }

        internal bool uploadCustomersForExcel()
        {
            try
            {
                parameters = new SqlParameter[] 
				{		 
				};
                return SqlDBHelper.ExecuteNonQuery("UploadCustomerExcelData", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }


    }
}
