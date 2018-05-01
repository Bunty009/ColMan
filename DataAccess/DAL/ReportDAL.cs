using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace DAL
{
    public class ReportDAL
    {
        SqlParameter[] parameters;

        internal System.Data.DataTable getLastWeekCollection()
        {
            DataTable table = new DataTable();
            parameters = new SqlParameter[]
            {
            };
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetLastWeekCollection", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }
        
        internal DataTable getInvoiceStatusCount()
        {
            DataTable table = new DataTable();
            parameters = new SqlParameter[]
            {
            };
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetInvoiceStatusCount", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }


    }
}
