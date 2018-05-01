using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;

namespace VighnhartaColors
{
    public partial class CutomerImport : Form
    {
        BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        DataTable dt = new DataTable();
        DataTable ds = new DataTable();

        public CutomerImport()
        {
            InitializeComponent();
            customerBAL = new BAL.CustomerBAL();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            string header = "YES";//rbHeaderYes.Checked ? "YES" : "NO";
            string conStr, sheetName;

            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel07ConString, filePath, header);
                    break;
            }

            //Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        //ds = new DataTable();
                        dt = new DataTable();

                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        //oda.Fill(ds);
                        con.Close();

                        //Populate DataGridView.
                        dgvCustomers.DataSource = dt;

                        var result = dt.AsEnumerable().Where(x => x.Field<double>("ActionFlag") != 0);
                        try
                        {
                            dt = result.CopyToDataTable();
                        }
                        catch { MessageBox.Show("Sorry Theres no row updated. Pls. Check the ActionFlag Column."); }

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(System.Configuration.ConfigurationManager.AppSettings["connectionstring"].ToString()))
                        {
                            bulkCopy.DestinationTableName = "tbl_TempCustomers";
                            //bulkCopy.ColumnMappings.Add("CustomerCode", "CustomerCode");
                            bulkCopy.ColumnMappings.Add("CustomerId", "CustomerId");
                            bulkCopy.ColumnMappings.Add("Name", "Name");
                            bulkCopy.ColumnMappings.Add("EmailId", "EmailId");
                            bulkCopy.ColumnMappings.Add("PhoneNo", "PhoneNo");
                            bulkCopy.ColumnMappings.Add("GSTN", "GSTN");
                            bulkCopy.ColumnMappings.Add("AddressLine1", "AddressLine1");
                            bulkCopy.ColumnMappings.Add("AddressLine2", "AddressLine2");
                            bulkCopy.ColumnMappings.Add("City", "City");
                            bulkCopy.ColumnMappings.Add("State", "State");
                            bulkCopy.ColumnMappings.Add("ZipCode", "ZipCode");
                            bulkCopy.ColumnMappings.Add("ActionFlag", "ActionFlag");
                            bulkCopy.WriteToServer(dt);
                        }
                    }
                }
            }
            bool response = customerBAL.uploadCustomersForExcel();
            if (response)
                MessageBox.Show("Excel file successfully uploaded.");
            else
                MessageBox.Show("Something went wrong. Pls. check your file.");
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

    }
}
