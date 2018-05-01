using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DAL
{
    public class OrderDAL
    {
        SqlParameter[] parameters;

        internal int addOrder(Order order, List<OrderItems> listItems)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@pOrderDate",order.OrderDate ),
                    new SqlParameter("@pBillToContact",order.BillToContact),
                    new SqlParameter("@pBillToGSTN",order.BillToGSTN),
                    new SqlParameter("@pBillToEmail",order.BillToEmail ),
                    new SqlParameter("@pBillToPhone",order.BillToPhone ),
                    new SqlParameter("@pBillToAddress1",order.BillToAddress1 ),
                    new SqlParameter("@pBillToAddress2",order.BillToAddress2 ),
                    new SqlParameter("@pBillToCity",order.BillToCity ),
                    new SqlParameter("@pBillToState", order.BillToState),
                    new SqlParameter("@pBillToZip",order.BillToZip ),
                    new SqlParameter("@pDiscount", order.Discount),
                    new SqlParameter("@pCGSTRate",order.CGSTRate ),
                    new SqlParameter("@pSGSTRate", order.SGSTRate),
                    new SqlParameter("@pIGSTRate",order.IGSTRate ),
                    new SqlParameter("@pTaxablePrice",order.TaxablePrice ),
                    new SqlParameter("@pTotalProductPrice",order.TotalProductPrice ),
                    new SqlParameter("@pGrandtotal", order.Grandtotal),
                    new SqlParameter("@GrandtotalInWords",order.GrandtotalInWords),
                    new SqlParameter("@pCreatedBy", 1)
                };
                order.OrderId = Convert.ToInt32(SqlDBHelper.ExecuteNonQueryReturnData("app_AddOrder", CommandType.StoredProcedure, parameters, "pOrderId"));
                if (order.OrderId != null)
                {
                    if (listItems.Count() > 0)
                    {
                        foreach (OrderItems item in listItems)
                        {
                            SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@pOrderId",order.OrderId),
                                new SqlParameter("@pMaterialId",item.MaterialId ),
                                new SqlParameter("@pMaterialName",item.MaterialName),
                                new SqlParameter("@pHSNCode",item.HSNCode),
                                new SqlParameter("@pQuantity",item.Quantity ),
                                new SqlParameter("@pUnitPrice",item.UnitPrice ),
                                new SqlParameter("@pTotalPrice",item.TotalPrice )
                            };
                            SqlDBHelper.ExecuteNonQuery("app_AddOrderItems", CommandType.StoredProcedure, param);
                        }
                    }
                }
                return order.OrderId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        internal bool updateOrder(Order order)
        {
            return true;
        }

        internal DataTable getOrders(object[] data)
        {
            DataTable table = new DataTable();
            return table;
        }
        
        internal DataSet getInvoiceById(int OrderId)
        {
            string orderid = "";
            try
            {
                orderid = Convert.ToString(OrderId);
            }
            catch { }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pOrderId", orderid)
            };
            DataSet table = new DataSet();
            try
            {
                table = SqlDBHelper.ExecuteParamerizeSelectCommand("app_GetInvoiceById", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }

        internal DataTable getRecentInvoices()
        {
            DataTable table = new DataTable();
            parameters = new SqlParameter[]
            {
            };
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetRecentInvoices", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }

    }
}
