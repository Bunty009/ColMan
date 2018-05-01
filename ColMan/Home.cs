using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; 

namespace VighnhartaColors
{
    public partial class Home : Form
    {
        BAL.UserBAL userBAL = new BAL.UserBAL();
        BAL.OrderBAL orderBAL = new BAL.OrderBAL();
        BAL.SupplierBAL supplierBAL = new BAL.SupplierBAL();
        BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
        BAL.MaterialBAL materialBAL = new BAL.MaterialBAL();
        BAL.ReportBAL reportBAL = new BAL.ReportBAL();

        public Home()
        {
            InitializeComponent();
            BindChartData();
            //BindMaterialPanel();
            //BindMaterialGrid(null);            
        }

        private void BindMaterialPanel()
        {
            materialBAL = new BAL.MaterialBAL();
            DataSet tables = new DataSet();
            DataTable dt = new DataTable();
            tables = materialBAL.getMaterials(null);
            dt = tables.Tables[0];
            tables.Relations.Add("DetailsPrices",
                tables.Tables[0].Columns["MaterialId"], tables.Tables[1].Columns["MaterialId"]);

            BindingSource bsMaterials = new BindingSource();
            bsMaterials.DataSource = tables;
            bsMaterials.DataMember = "Table";

            BindingSource bsPrices = new BindingSource();
            bsPrices.DataSource = bsMaterials;
            bsPrices.DataMember = "DetailsPrices";
            dataGridMaterial.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridMaterial.DataSource = bsMaterials;
            dataGridPrice.DataSource = bsPrices;

            this.dataGridUser.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridMaterial_RowPostPaint);
            //dataGridMaterial.DataSource = tables.Tables[0];
            this.dataGridMaterial.Columns["MaterialId"].Visible = false;
            this.dataGridPrice.Columns["MaterialId"].Visible = false;
            this.dataGridMaterial.Columns["PriceId"].Visible = false;
            dataGridMaterial.Columns["CreatedOn"].DefaultCellStyle.Format = "MM/dd/yyyy";

            // Auto Resize
            dataGridMaterial.AutoResizeColumns();
            dataGridMaterial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridPrice.AutoResizeColumns();
            dataGridPrice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            BindAutoCompleteToMaterial(dt);                         
        }

        private void BindAutoCompleteToMaterial(DataTable dt)
        {
            txtSearchMaterial.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSearchMaterial.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection dataCollection = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                dataCollection.Add(row[1].ToString());
                dataCollection.Add(row[2].ToString());
                dataCollection.Add(row[5].ToString());
            }

            //getData(DataCollection);
            txtSearchMaterial.AutoCompleteCustomSource = dataCollection;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int tabIndex = e.TabPageIndex;
            string tabName = e.TabPage.Name;
            switch (tabIndex)
            {
                //"tabDashboard":
                case 0:
                    BindChartData();                                        
                    break;
                //"tabMaterial":
                case 1:
                    BindMaterialGrid(null);
                    break;
                //"tabSupplier":
                case 2:
                    BindSupplierGrid(null);
                    break;
                //"tabCustomer":
                case 3:
                    BindCustomerGrid(null);
                    break;
                //"tabOrders":
                case 4: 
                    break;
                //"tabReports":
                case 5:
                    break;
                //"tabSecurity"
                case 6:
                    BindUserDataGrid(null);
                    break;
                default :
                    break;
            }
        }

        private void BindChartData()
        {
            reportBAL = new BAL.ReportBAL();
            chart1.Series[0].Points.Clear();
            chart1.Titles.Clear();
            DataTable dt = reportBAL.getLastWeekCollection();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                chart1.Series["Collection"].Points.AddXY(dr["OrderDate"], dr["Total"]);
                chart1.Series["Collection"].Points[i].ToolTip = "R : "+ dr["Total"].ToString();
                i++;
            }
            chart1.Titles.Add("Collection In Ruppes Chart");
            BindPieChart();
            orderBAL = new BAL.OrderBAL();
            DataTable dtO = orderBAL.getRecentInvoices();
            dgvRecentInvoices.DataSource = dtO;
        }

        private void BindPieChart()
        {
            reportBAL = new BAL.ReportBAL();
            chart2.Series[0].Points.Clear();
            chart2.Titles.Clear();
            DataTable dt = reportBAL.getInvoiceStatusCount();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                chart2.Series["CountCollection"].Points.AddXY(dr["InvoiceStatus"], dr["Total"]);
                chart2.Series["CountCollection"].Points[i].ToolTip = "Count : " + dr["Total"].ToString();
                i++;
            }
            chart2.Titles.Add("Invoice Status Counts");
        }

        private void getData(AutoCompleteStringCollection dataCollection)
        {
            materialBAL = new BAL.MaterialBAL();
            DataSet ds = new DataSet();
            ds = materialBAL.getMaterials(null);
            try
            {
                DataTable dt = new DataTable();
                dt =ds.Tables[0];
                dt.Columns.Remove("MaterialId");
                dt.Columns.Remove("MaterialDescription");
                dt.Columns.Remove("PriceId");
                dt.Columns.Remove("CreatedOn");
                dt.Columns.Remove("ModifiedOn");
                foreach (DataRow row in dt.Rows)
                {
                    dataCollection.Add(row[0].ToString());
                    dataCollection.Add(row[1].ToString());
                    dataCollection.Add(row[2].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }

        // DataGridView Bind Events
        private void BindMaterialGrid(object[] data)
        {
            materialBAL = new BAL.MaterialBAL();
            DataSet tables = new DataSet();

            tables = materialBAL.getMaterials(data);
            try
            {
                tables.Relations.Add("DetailsPrices",
                    tables.Tables[0].Columns["MaterialId"], tables.Tables[1].Columns["MaterialId"]);

                BindingSource bsMaterials = new BindingSource();
                bsMaterials.DataSource = tables;
                bsMaterials.DataMember = "Table";

                BindingSource bsPrices = new BindingSource();
                bsPrices.DataSource = bsMaterials;
                bsPrices.DataMember = "DetailsPrices";
                dataGridMaterial.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dataGridMaterial.DataSource = bsMaterials;
                dataGridPrice.DataSource = bsPrices;
            }
            catch(Exception ex) { }
            this.dataGridUser.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridMaterial_RowPostPaint);
            //dataGridMaterial.DataSource = tables.Tables[0];
            this.dataGridMaterial.Columns["MaterialId"].Visible = false;
            this.dataGridPrice.Columns["MaterialId"].Visible = false;
            this.dataGridMaterial.Columns["PriceId"].Visible = false;
            dataGridMaterial.Columns["CreatedOn"].DefaultCellStyle.Format = "MM/dd/yyyy";

            // Auto Resize
            dataGridMaterial.AutoResizeColumns();
            dataGridMaterial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridPrice.AutoResizeColumns();
            dataGridPrice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }        

        private void BindCustomerGrid(object[] data)
        {
            customerBAL = new BAL.CustomerBAL();
            DataTable table = new DataTable();
            table = customerBAL.getCustomers(data);
            this.dataGridCustomer.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridSupplier_RowPostPaint);
            dataGridCustomer.DataSource = table;
            this.dataGridCustomer.Columns["CustomerId"].Visible = false;
            // Auto Resize
            dataGridCustomer.AutoResizeColumns();
            dataGridCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            BindAutoCompleteToCustomer(table);
        }

        private void BindAutoCompleteToCustomer(DataTable table)
        {
            txtSearchCustomer.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSearchCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection dataCollection = new AutoCompleteStringCollection();
            foreach (DataRow row in table.Rows)
            {
                dataCollection.Add(row[1].ToString());
                dataCollection.Add(row[2].ToString());
                dataCollection.Add(row[3].ToString());
                dataCollection.Add(row[4].ToString());
                dataCollection.Add(row[5].ToString());
            }
            txtSearchCustomer.AutoCompleteCustomSource = dataCollection;
        }

        private void BindSupplierGrid(object[] data)
        {
            supplierBAL = new BAL.SupplierBAL();
            DataTable table = new DataTable();
            table = supplierBAL.getSuppliers(data);
            dataGridSupplier.DataSource = table;
            this.dataGridSupplier.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridSupplier_RowPostPaint);
            //dataGridUser.Columns[5].DefaultCellStyle.Format = "MM/dd/yyyy";
            //dataGridUser.Columns[6].DefaultCellStyle.Format = "MM/dd/yyyy";
            this.dataGridSupplier.Columns["SupplierId"].Visible = false;
            // Auto Resize
            dataGridSupplier.AutoResizeColumns();
            dataGridSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void BindUserDataGrid(object[] data)
        {
            userBAL = new BAL.UserBAL();
            DataTable table = new DataTable();
            table = userBAL.getUsers(data);
            this.dataGridUser.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
            dataGridUser.DataSource = table;
            this.dataGridUser.Columns["UserId"].Visible = false;
            dataGridUser.Columns[5].DefaultCellStyle.Format = "MM/dd/yyyy";
            dataGridUser.Columns[6].DefaultCellStyle.Format = "MM/dd/yyyy";
            // Auto Resize
            dataGridUser.AutoResizeColumns();
            dataGridUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        
        //RowPostPaint Event
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridUser.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridSupplier_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridSupplier.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridMaterial_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridMaterial.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        // Security Tab Events
        private void picAddUser_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }

        private void picEditUser_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridUser.SelectedCells;
            if (cell.Count <= 1)
            {
                DataGridViewRow row = dataGridUser.Rows[cell[0].RowIndex];
                int userId = Convert.ToInt32(row.Cells["UserId"].Value.ToString());
                EditUser editUser = new EditUser(userId);
                editUser.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pls. Select Only one Cell at a Time.");
            }
        }

        private void picDeleteUser_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridUser.SelectedCells;
            DataGridViewRow row = dataGridUser.Rows[cell[0].RowIndex];
            int Id = Convert.ToInt32(row.Cells["UserId"].Value.ToString());
            ConfirmBox deleteUser = new ConfirmBox(Id, "User");
            deleteUser.Show();
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            string searchKey = txtSearchBox.Text;
            if (searchKey != null && searchKey != "")
            {
                BindUserDataGrid(new object[] { "",searchKey});
            }
            else 
            {
                BindUserDataGrid(null);
            }
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            int index = tabControl1.TabIndex;
            txtSearchBox.Text = string.Empty;
            BindUserDataGrid(null);
        }
        
        // Material Tab Events
        private void picSearchMaterial_Click(object sender, EventArgs e)
        {
            string searchKey = txtSearchMaterial.Text;
            if (searchKey != null && searchKey != "")
            {
                BindMaterialGrid(new object[]{"",searchKey});
            }
            else 
            {
                BindMaterialGrid(null);
            }
        }

        private void picExportToExcel_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int i = 0;
            int j = 0;

            DataTable dtMaterial = new DataTable();
            dtMaterial = GetTablesToExport();
            
            //foreach(DataColumn column in dtMaterial.Columns){
            //    int col = 1;
            //        xlWorkSheet.Rows[0].Cells[col+1] = column.Caption;
            //    col++;
            //}

            for (int k = 1; k < dtMaterial.Columns.Count+1; k++)
            {
                if (k == 1)
                {
                    xlWorkSheet.Cells[1, 1] = dtMaterial.Columns[0].Caption;
                    xlWorkSheet.Cells[1, 1].interior.color = Color.IndianRed;
                    xlWorkSheet.Cells[1, 1].Font.color = Color.White;
                    xlWorkSheet.Cells[1, 1].Font.Size = 12;
                    xlWorkSheet.Cells[1, 1].Font.Bold = true;
                }
                else
                {
                    string _colHeader =  dtMaterial.Columns[k - 1].Caption;
                    if(_colHeader =="IsActive")
                        _colHeader="Action";
                    xlWorkSheet.Cells[1, k] = _colHeader;
                    xlWorkSheet.Cells[1, k].interior.color = Color.IndianRed;
                    xlWorkSheet.Cells[1, k].Font.color = Color.White;
                    xlWorkSheet.Cells[1, k].Font.Size = 12;
                    xlWorkSheet.Cells[1, k].Font.Bold = true;
                }
            }

            int p = 1;
            for (i = 0; i <= dtMaterial.Rows.Count - 1; i++)
            {
                for (j = 0; j <= dtMaterial.Columns.Count - 1; j++)
                {
                    //DataGridViewCell cell = (DataGridViewCell)dtMaterial.Rows[i][j];
                    xlWorkSheet.Cells[p + 1, j + 1] = dtMaterial.Rows[i][j];                    
                }
                p++;
            }

            xlWorkBook.SaveAs("AllMaterials.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            MessageBox.Show("Excel file created , you can find the file in Documents Folder.");
        }

        private DataTable GetTablesToExport()
        {
            materialBAL = new BAL.MaterialBAL();
            DataTable table = new DataTable();
            table = materialBAL.getMaterialsForExcel();
            return table;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void picRefreshMaterial_Click(object sender, EventArgs e)
        {
            txtSearchMaterial.Text = string.Empty;
            BindMaterialGrid(null);
        }

        // Orders Tab Events

        // Supplier Tab Events

        // Customer Tab Events
         
        private void picLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Dialog Title", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                this.Close();
            }
            //this.Hide();
            //Login login = new Login();
            //login.ShowDialog();
            //this.Close();
        }

        private void picEditCustomer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridCustomer.SelectedCells;
            if (cell.Count <= 1)
            {
                DataGridViewRow row = dataGridCustomer.Rows[cell[0].RowIndex];
                int customerId = Convert.ToInt32(row.Cells["CustomerId"].Value.ToString());
                EditCustomer editCustomer = new EditCustomer(customerId);
                editCustomer.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pls. Select Only one Cell at a Time.");
            }
        }

        private void picRefreshCustomer_Click(object sender, EventArgs e)
        {
            int index = tabControl1.TabIndex;
            txtSearchCustomer.Text = string.Empty;
            BindCustomerGrid(null);
        }

        private void picAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
        }

        private void picDeleteCustomer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridCustomer.SelectedCells;
            DataGridViewRow row = dataGridCustomer.Rows[cell[0].RowIndex];
            int Id = Convert.ToInt32(row.Cells["CustomerId"].Value.ToString());
            ConfirmBox deleteCustomer = new ConfirmBox(Id, "Customer");
            deleteCustomer.Show();
        }

        private void picSearchCustomer_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearchCustomer.Text.ToString();
            if (searchKeyword != null && searchKeyword != "")
            {
                BindCustomerGrid(new object[] { "", searchKeyword });
            }
            else
            {
                BindCustomerGrid(null);
            }
        }

        private void picAddSupplier_Click(object sender, EventArgs e)
        {
            AddSuppliers addSuppliers = new AddSuppliers();
            addSuppliers.Show();
        }

        private void picEditSupplier_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridSupplier.SelectedCells;
            if (cell.Count <= 1)
            {
                DataGridViewRow row = dataGridSupplier.Rows[cell[0].RowIndex];
                int supplierId = Convert.ToInt32(row.Cells["SupplierId"].Value.ToString());
                EditSupplier editSupplier = new EditSupplier(supplierId);
                editSupplier.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pls. Select Only one Cell at a Time.");
            }
        }

        private void picRefreshSupplier_Click(object sender, EventArgs e)
        {
            txtSearchSupplier.Text = string.Empty;
            BindSupplierGrid(null);
        }

        private void picDeleteSupplier_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridSupplier.SelectedCells;
            DataGridViewRow row = dataGridSupplier.Rows[cell[0].RowIndex];
            int Id = Convert.ToInt32(row.Cells["SupplierId"].Value.ToString());
            ConfirmBox deleteSupplier = new ConfirmBox(Id, "Supplier");
            deleteSupplier.Show();
        }

        private void picSearchSupplier_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearchSupplier.Text.ToString();
            if (searchKeyword != null && searchKeyword != "")
            {
                BindSupplierGrid(new object[] { "", searchKeyword });
            }
            else
            {
                BindCustomerGrid(null);
            }
        }

        private void picAddMaterial_Click(object sender, EventArgs e)
        {
            AddMaterial addMaterial = new AddMaterial();
            addMaterial.Show();
        }

        private void picEditMaterial_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridMaterial.SelectedCells;
            if (cell.Count <= 1)
            {
                DataGridViewRow row = dataGridMaterial.Rows[cell[0].RowIndex];
                int materialId = Convert.ToInt32(row.Cells["MaterialId"].Value.ToString());
                EditMaterial editMaterial = new EditMaterial(materialId);
                editMaterial.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pls. Select Only one Cell at a Time.");
            }
        }

        private void picDeleteMaterial_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cell = dataGridMaterial.SelectedCells;
            DataGridViewRow row = dataGridMaterial.Rows[cell[0].RowIndex];
            int Id = Convert.ToInt32(row.Cells["MaterialId"].Value.ToString());
            ConfirmBox deleteMaterial = new ConfirmBox(Id, "Material");
            deleteMaterial.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Challan ch = new Challan();
            ch.Show();
        }

        private void picExcelCustomer_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int i = 0;
            int j = 0;

            DataTable dtCustomer = new DataTable();
            dtCustomer = GetCustomerTableToExport();

            //foreach(DataColumn column in dtMaterial.Columns){
            //    int col = 1;
            //        xlWorkSheet.Rows[0].Cells[col+1] = column.Caption;
            //    col++;
            //}

            for (int k = 1; k < dtCustomer.Columns.Count + 1; k++)
            {
                if (k == 1)
                {
                    xlWorkSheet.Cells[1, 1] = dtCustomer.Columns[0].Caption;
                    xlWorkSheet.Cells[1, 1].interior.color = Color.IndianRed;
                    xlWorkSheet.Cells[1, 1].Font.color = Color.White;
                    xlWorkSheet.Cells[1, 1].Font.Size = 12;
                    xlWorkSheet.Cells[1, 1].Font.Bold = true;
                }
                else
                {
                    string _colHeader = dtCustomer.Columns[k - 1].Caption;
                    if (_colHeader == "IsActive")
                        _colHeader = "Action";
                    xlWorkSheet.Cells[1, k] = _colHeader;
                    xlWorkSheet.Cells[1, k].interior.color = Color.IndianRed;
                    xlWorkSheet.Cells[1, k].Font.color = Color.White;
                    xlWorkSheet.Cells[1, k].Font.Size = 12;
                    xlWorkSheet.Cells[1, k].Font.Bold = true;
                }
            }

            int p = 1;
            for (i = 0; i <= dtCustomer.Rows.Count - 1; i++)
            {
                for (j = 0; j <= dtCustomer.Columns.Count - 1; j++)
                {
                    //DataGridViewCell cell = (DataGridViewCell)dtMaterial.Rows[i][j];
                    xlWorkSheet.Cells[p + 1, j + 1] = dtCustomer.Rows[i][j];
                }
                p++;
            }

            xlWorkBook.SaveAs("AllCustomers.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            MessageBox.Show("Excel file created , you can find the file in Documents Folder.");
        }

        private DataTable GetCustomerTableToExport()
        {
            customerBAL = new BAL.CustomerBAL();
            DataTable table = new DataTable();
            table = customerBAL.getCustomersForExcel();
            return table;
        }

        private void picImportCustomer_Click(object sender, EventArgs e)
        {
            CutomerImport ci = new CutomerImport();
            ci.ShowDialog();
        }


    }
}
