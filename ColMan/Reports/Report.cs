using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entity;
using CrystalDecisions.CrystalReports.Engine;
using BAL;
using CrystalDecisions.Shared;

namespace VighnhartaColors.Reports
{
    public partial class Report : Form
    {
        int OrderId;
        OrderBAL orderBAL = new OrderBAL();
        ReportDocument rprt = new ReportDocument();
        ParameterField paramField = new ParameterField();

        ParameterFields paramFields = new ParameterFields();

        ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

        public Report(int Id)
        {
            InitializeComponent();
            OrderId = Id;
            orderBAL = new OrderBAL();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            paramField.Name = "@pOrderId";
            paramDiscreteValue.Value = OrderId;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            crystalReportViewer1.ParameterFieldInfo = paramFields;
            rprt.Load(@"D:\Vinayak.Ramane\New Projects\VS 2010\New ColMan\ColMan\ColMan\Reports\Invoice.rpt");  
            crystalReportViewer1.ReportSource = rprt;
            rprt.SetDatabaseLogon("codenext", "codenext", "10.0.0.45\\MSSQL", "codenext");            
        }


    }
}
