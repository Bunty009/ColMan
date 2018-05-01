using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace VighnhartaColors
{
    public partial class Test : Form
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        DataTable dt = new DataTable();
        DataTable ds = new DataTable();

        public Test()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
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
                        dataGridView1.DataSource = dt;

                        var result = dt.AsEnumerable().Where(x => x.Field<double>("ActionFlag") != 0);

                        dt = result.CopyToDataTable();
                        
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(System.Configuration.ConfigurationManager.AppSettings["connectionstring"].ToString()))
                        {
                            bulkCopy.DestinationTableName = "tbl_TempMaterials";

                            bulkCopy.ColumnMappings.Add("MaterialId", "MaterialId");
                            bulkCopy.ColumnMappings.Add("MaterialCode", "MaterialCode");
                            bulkCopy.ColumnMappings.Add("MaterialName", "MaterialName");
                            bulkCopy.ColumnMappings.Add("MaterialDescription", "MaterialDescription");
                            bulkCopy.ColumnMappings.Add("HSNCode", "HSNCode");
                            bulkCopy.ColumnMappings.Add("OneKGSize", "OneKGSize");
                            bulkCopy.ColumnMappings.Add("OneKGPrice", "OneKGPrice");
                            bulkCopy.ColumnMappings.Add("HalfKGSize", "HalfKGSize");
                            bulkCopy.ColumnMappings.Add("HalfKGPrice", "HalfKGPrice");
                            bulkCopy.ColumnMappings.Add("Size1", "Size1");
                            bulkCopy.ColumnMappings.Add("Price1", "Price1");
                            bulkCopy.ColumnMappings.Add("Size2", "Size2");
                            bulkCopy.ColumnMappings.Add("Price2", "Price2");
                            bulkCopy.ColumnMappings.Add("Size3", "Size3");
                            bulkCopy.ColumnMappings.Add("Price3", "Price3");
                            bulkCopy.ColumnMappings.Add("Size4", "Size4");
                            bulkCopy.ColumnMappings.Add("Price4", "Price4");
                            bulkCopy.ColumnMappings.Add("Size5", "Size5");
                            bulkCopy.ColumnMappings.Add("Price5", "Price5");
                            bulkCopy.ColumnMappings.Add("Size6", "Size6");
                            bulkCopy.ColumnMappings.Add("Price6", "Price6");
                            bulkCopy.ColumnMappings.Add("Size7", "Size7");
                            bulkCopy.ColumnMappings.Add("Price7", "Price7");
                            bulkCopy.ColumnMappings.Add("Size8", "Size8");
                            bulkCopy.ColumnMappings.Add("Price8", "Price8");
                            bulkCopy.ColumnMappings.Add("Size9", "Size9");
                            bulkCopy.ColumnMappings.Add("Price9", "Price9");
                            bulkCopy.ColumnMappings.Add("ActionFlag", "ActionFlag");

                            bulkCopy.WriteToServer(dt);
                        }
                        //string[] existingcol = { "OneKGSize", "OneKGPrice", "HalfKGSize", "HalfKGPrice", "Size1", "Price1", "Size2", "Price2", "Size3", "Price3", "Size4", "Price4", "Size5", "Price5", "Size6", "Price6", "Size7", "Price7", "Size8", "Price8", "Size9" };
                        //foreach (string colName in existingcol)
                        //{
                        //    ds.Columns.Remove(colName);
                        //}

                        //dt.Columns.Remove("MaterialDescription");
                        //dt.Columns.Remove("HSNCode");
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rbHeaderYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbHeaderNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



    }
}
