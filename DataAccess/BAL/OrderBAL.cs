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
    public class OrderBAL
    {
        OrderDAL orderDAL = null;

        public OrderBAL()
        {
            orderDAL = new OrderDAL();
        }

        public int addOrder(Order order, List<OrderItems> list)
        {
            return orderDAL.addOrder(order, list);
        }

        public bool updateOrder(Order user)
        {
            return orderDAL.updateOrder(user);
        }

        public DataTable getOrders(object[] data)
        {
            return orderDAL.getOrders(data);
        }

        public DataSet getInvoiceById(int OrderId)
        {
            return orderDAL.getInvoiceById(OrderId);
        }

        public DataTable getRecentInvoices()
        {
            return orderDAL.getRecentInvoices();
        }

    }
}
