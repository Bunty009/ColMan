using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BAL
{
    public class ReportBAL
    {
        ReportDAL reportDAL = null;

        public ReportBAL()
        {
            reportDAL = new ReportDAL();
        }

        public DataTable getLastWeekCollection()
        {
            return reportDAL.getLastWeekCollection();
        }
        
        public DataTable getInvoiceStatusCount()
        {
            return reportDAL.getInvoiceStatusCount();
        }
    }
}
