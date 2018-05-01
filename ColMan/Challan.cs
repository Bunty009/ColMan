using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VighnhartaColors.Reports;
using System.Globalization;

namespace VighnhartaColors
{
    public partial class Challan : Form
    {
        BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
        BAL.MaterialBAL materialBAL = new BAL.MaterialBAL();
        BAL.OrderBAL orderBAL = new BAL.OrderBAL();
        DataTable dtNewProduct = new DataTable();
        int qty;

        public Challan()
        {
            InitializeComponent();
            customerBAL = new BAL.CustomerBAL();
            materialBAL = new BAL.MaterialBAL();
            orderBAL = new BAL.OrderBAL();
            BindCustomer();
            BindProducts();            
        }

        private void BindCustomer()
        {
            DataRow dr;
            DataTable dt = new DataTable();
            dt = customerBAL.GetCustomerForChallan(new object[] { "C" });

            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Customer--" };
            dt.Rows.InsertAt(dr, 0);

            cbCustomer.ValueMember = "CustomerId";
            cbCustomer.DisplayMember = "Name";
            cbCustomer.DataSource = dt;
        }

        private void BindProducts()
        {
            DataRow dr;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = materialBAL.GetProductsForChallan(new object[] { "C" });

            dt = ds.Tables[0];

            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Product--" };
            dt.Rows.InsertAt(dr, 0);

            cbProducts.ValueMember = "MaterialId";
            cbProducts.DisplayMember = "Material";
            cbProducts.DataSource = dt;
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string id = cbCustomer.SelectedValue.ToString();
            dt = customerBAL.GetCustomerForChallan(new object[] { "D", id });
            try
            {
                txtEmailId.Text = dt.Rows[0]["EmailId"].ToString();
            }
            catch { }
            try
            {
                txtContactNo.Text = dt.Rows[0]["PhoneNo"].ToString();
            }
            catch { }
            try
            {
                txtGSTN.Text = dt.Rows[0]["GSTN"].ToString();
            }
            catch { }
            try
            {
                txtAddressLine1.Text = dt.Rows[0]["AddressLine1"].ToString();
            }
            catch { }
            try
            {
                txtAddressLine2.Text = dt.Rows[0]["AddressLine2"].ToString();
            }
            catch { }
            try
            {
                txtCity.Text = dt.Rows[0]["City"].ToString();
            }
            catch { }
            try
            {
                txtState.Text = dt.Rows[0]["State"].ToString();
            }
            catch { }
            try
            {
                txtZip.Text = dt.Rows[0]["ZipCode"].ToString();
            }
            catch { }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            int i = 1;

            DataRow dr;
            string materialId = cbProducts.SelectedValue.ToString();
            try
            {
                qty = Convert.ToInt32(txtQuantity.Text.ToString());
            }
            catch { qty = 0; }
            if (materialId == null || materialId == "0")
            {
                MessageBox.Show("Pls. Select Product before adding.");
                return;
            }
            if (qty == null || qty == 0)
            {
                MessageBox.Show("Pls. Enter Quantity before adding product.");
                return;
            }
            if (dtNewProduct.Rows.Count <= 0)
            {
                dtNewProduct.Columns.Add("Id", Type.GetType("System.Int32"));
                dtNewProduct.Columns.Add("Product Code", Type.GetType("System.String"));
                dtNewProduct.Columns.Add("Product Name", Type.GetType("System.String"));
                dtNewProduct.Columns.Add("HSNCode", Type.GetType("System.String"));
                dtNewProduct.Columns.Add("Qty", Type.GetType("System.String"));
                dtNewProduct.Columns.Add("Rate", Type.GetType("System.Double"));
                dtNewProduct.Columns.Add("Total", Type.GetType("System.Double"));
            }

            DataSet ds = new DataSet();
            ds = materialBAL.GetProductsForChallan(new object[] { "D", materialId });
            dr = dtNewProduct.NewRow();

            double oneKGRate = Convert.ToDouble(ds.Tables[1].Rows[0]["OneKGPrice"].ToString());
            double total = oneKGRate * qty;

            dr["Id"] = materialId;
            dr["Product Code"] = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
            dr["Product Name"] = ds.Tables[0].Rows[0]["MaterialName"].ToString();
            dr["HSNCode"] = ds.Tables[0].Rows[0]["HSNCode"].ToString();
            dr["Qty"] = qty;
            dr["Rate"] = oneKGRate;
            dr["Total"] = total;
            dtNewProduct.Rows.Add(dr);

            double totalAmount = 0;
            foreach (DataRow rw in dtNewProduct.Rows)
            {
                totalAmount += Convert.ToDouble(rw["Total"]);
            }
            lblTotalAmount.Text = totalAmount.ToString();
            lblDiscountedPrice.Text = totalAmount.ToString();
            dgvProducts.DataSource = dtNewProduct;
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            decimal? CGSTdiscount = null, SGSTdiscount = null, IGSTdiscount = null;
            decimal discount = Convert.ToInt32(txtDiscount.Text.ToString());
            try
            {
                CGSTdiscount = Convert.ToInt32(txtCGST.Text.ToString());
            }
            catch { CGSTdiscount = null; }
            try
            {
                SGSTdiscount = Convert.ToInt32(txtSGST.Text.ToString());
            }
            catch { SGSTdiscount = null; }
            try
            {
                IGSTdiscount = Convert.ToInt32(txtIGST.Text.ToString());
            }
            catch { IGSTdiscount = null; }

            if (discount != null)
            {
                decimal total = Convert.ToDecimal(lblTotalAmount.Text.ToString());
                var dis = discount / 100;
                var discountPrice = total * dis;
                lblDiscountedPrice.Text = (total - discountPrice).ToString();
                lblGrandTotal.Text = (total - discountPrice).ToString("N");
            }
            else
            {
                lblDiscountedPrice.Text = lblTotalAmount.Text.ToString();
            }

            if (CGSTdiscount != null || SGSTdiscount != null || IGSTdiscount != null)
            {
                if (CGSTdiscount != null)
                {
                    decimal total = Convert.ToDecimal(lblDiscountedPrice.Text.ToString());
                    var dis = CGSTdiscount / 100;
                    var discountPrice = total * dis;
                    decimal res = Convert.ToDecimal(total) + Convert.ToDecimal(discountPrice);
                    lblGrandTotal.Text = res.ToString("N");
                }

                if (SGSTdiscount != null)
                {
                    decimal total = Convert.ToDecimal(lblDiscountedPrice.Text.ToString());
                    var dis = SGSTdiscount / 100;
                    var discountPrice = total * dis;
                    var newPrice = lblGrandTotal.Text;
                    decimal t = Convert.ToDecimal(newPrice) + Convert.ToDecimal(discountPrice);
                    lblGrandTotal.Text = t.ToString("N");
                }

                if (IGSTdiscount != null)
                {
                    decimal total = Convert.ToDecimal(lblDiscountedPrice.Text.ToString());
                    var dis = IGSTdiscount / 100;
                    var discountPrice = total * dis;
                    var newPrice = lblGrandTotal.Text;
                    decimal t = Convert.ToDecimal(newPrice) + Convert.ToDecimal(discountPrice);
                    lblGrandTotal.Text = t.ToString("N");
                }

            }
        }

        private void txtCGST_TextChanged(object sender, EventArgs e)
        {
            decimal discount = Convert.ToInt32(txtCGST.Text.ToString());
            if (discount != null)
            {
                decimal total = Convert.ToDecimal(lblDiscountedPrice.Text.ToString());
                var dis = discount / 100;
                var discountPrice = total * dis;
                decimal res = Convert.ToDecimal(total) + Convert.ToDecimal(discountPrice);
                lblGrandTotal.Text = res.ToString("N");
            }
        }

        private void txtSGST_TextChanged(object sender, EventArgs e)
        {
            decimal discount = Convert.ToInt32(txtCGST.Text.ToString());
            if (discount != null)
            {
                decimal total = Convert.ToDecimal(lblDiscountedPrice.Text.ToString());
                var dis = discount / 100;
                var discountPrice = total * dis;
                var newPrice = lblGrandTotal.Text;
                decimal t = Convert.ToDecimal(newPrice) + Convert.ToDecimal(discountPrice);
                lblGrandTotal.Text = t.ToString("N");
            }
        }

        private void txtIGST_TextChanged(object sender, EventArgs e)
        {
            decimal discount = Convert.ToInt32(txtCGST.Text.ToString());
            if (discount != null)
            {
                decimal total = Convert.ToDecimal(lblDiscountedPrice.Text.ToString());
                var dis = discount / 100;
                var discountPrice = total * dis;
                var newPrice = lblGrandTotal.Text;
                decimal t = Convert.ToDecimal(newPrice) + Convert.ToDecimal(discountPrice);
                lblGrandTotal.Text = t.ToString("N");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Entity.Order order = new Entity.Order();
            List<Entity.OrderItems> listOrderItems = new List<Entity.OrderItems>();

            try
            {
                order.OrderDate = Convert.ToDateTime(dtpInvoiceDate.Text);
            }
            catch { }
            try
            {
                order.BillToContact = cbCustomer.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToEmail = txtEmailId.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToPhone = txtContactNo.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToGSTN = txtGSTN.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToAddress1 = txtAddressLine1.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToAddress2 = txtAddressLine2.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToCity = txtCity.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToState = txtState.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToZip = Convert.ToInt32(txtZip.Text.ToString());
            }
            catch { }
            try
            {
                order.Discount = Convert.ToInt32(txtDiscount.Text.ToString());
            }
            catch { }
            try
            {
                order.CGSTRate = Convert.ToInt32(txtCGST.Text.ToString());
            }
            catch { }
            try
            {
                order.SGSTRate = Convert.ToInt32(txtSGST.Text.ToString());
            }
            catch { }
            try
            {
                order.IGSTRate = Convert.ToInt32(txtIGST.Text.ToString());
            }
            catch { }
            try
            {
                order.TaxablePrice = Convert.ToDouble(lblDiscountedPrice.Text.ToString());
            }
            catch { }
            double _totalProductPrice = 0;
            foreach (DataGridViewRow dr in dgvProducts.Rows)
            {
                Entity.OrderItems orderItem = new Entity.OrderItems();
                try
                {
                    orderItem.MaterialId = Convert.ToInt32(dr.Cells["Id"].Value.ToString());
                }
                catch { }
                try
                {
                    orderItem.MaterialName = dr.Cells["Product Name"].Value.ToString();
                }
                catch { }
                try
                {
                    orderItem.HSNCode = dr.Cells["HSNCode"].Value.ToString();
                }
                catch { }
                try
                {
                    string val = dr.Cells["Qty"].Value.ToString();
                    orderItem.Quantity = Convert.ToInt32(val);
                }
                catch { }
                try
                {
                    orderItem.UnitPrice = Convert.ToDouble(dr.Cells["Rate"].Value.ToString());
                }
                catch { }
                try
                {
                    orderItem.TotalPrice = Convert.ToDouble(dr.Cells["Total"].Value.ToString());
                }
                catch { }
                _totalProductPrice += orderItem.TotalPrice;
                listOrderItems.Add(orderItem);
            }
            try
            {
                order.TotalProductPrice = Convert.ToDouble(_totalProductPrice);
            }
            catch { }
            try
            {
                if (!String.IsNullOrEmpty(lblGrandTotal.Text.ToString()))
                    order.Grandtotal = Convert.ToDouble(lblGrandTotal.Text.ToString());
                else
                    order.Grandtotal = Convert.ToDouble(lblDiscountedPrice.Text.ToString());
            }
            catch { }
            try
            {
                string s = order.Grandtotal.ToString("0.00", CultureInfo.InvariantCulture);
                string[] parts = s.Split('.');
                int i1 = int.Parse(parts[0]);
                int i2 = int.Parse(parts[1]);
                long l1 = (long)i1;
                long l2 = (long)i2;
                string s1 = ConvertNumbertoWords(l1);
                string s2 = ConvertNumbertoWords(l2);
                string s3 = s1 + " And " + s2 + " Paise Only.";
                order.GrandtotalInWords = s3;
            }
            catch { }
            int response = orderBAL.addOrder(order, listOrderItems);
            //using (Report rpt = new Report(response))
            //{
            //    rpt.ShowDialog();
            //}
            if (response != null && response != 0)
            {
                MessageBox.Show("Invoice saved successfully.");
            }
            else
            {
                MessageBox.Show("Something Went Wrong.");
            }
        }

        private void btnSaveandPrint_Click(object sender, EventArgs e)
        {
            Entity.Order order = new Entity.Order();
            List<Entity.OrderItems> listOrderItems = new List<Entity.OrderItems>();

            try
            {
                order.OrderDate = Convert.ToDateTime(dtpInvoiceDate.Text);
            }
            catch { }
            try
            {
                order.BillToContact = cbCustomer.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToEmail = txtEmailId.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToPhone = txtContactNo.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToGSTN = txtGSTN.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToAddress1 = txtAddressLine1.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToAddress2 = txtAddressLine2.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToCity = txtCity.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToState = txtState.Text.ToString();
            }
            catch { }
            try
            {
                order.BillToZip = Convert.ToInt32(txtZip.Text.ToString());
            }
            catch { }
            try
            {
                order.Discount = Convert.ToInt32(txtDiscount.Text.ToString());
            }
            catch { }
            try
            {
                order.CGSTRate = Convert.ToInt32(txtCGST.Text.ToString());
            }
            catch { }
            try
            {
                order.SGSTRate = Convert.ToInt32(txtSGST.Text.ToString());
            }
            catch { }
            try
            {
                order.IGSTRate = Convert.ToInt32(txtIGST.Text.ToString());
            }
            catch { }
            try
            {
                order.TaxablePrice = Convert.ToDouble(lblDiscountedPrice.Text.ToString());
            }
            catch { }
            double _totalProductPrice = 0;
            foreach (DataGridViewRow dr in dgvProducts.Rows)
            {
                Entity.OrderItems orderItem = new Entity.OrderItems();
                try
                {
                    orderItem.MaterialId = Convert.ToInt32(dr.Cells["Id"].Value.ToString());
                }
                catch { }
                try
                {
                    orderItem.MaterialName = dr.Cells["Product Name"].Value.ToString();
                }
                catch { }
                try
                {
                    orderItem.HSNCode = dr.Cells["HSNCode"].Value.ToString();
                }
                catch { }
                try
                {
                    string val = dr.Cells["Qty"].Value.ToString();
                    orderItem.Quantity = Convert.ToInt32(val);
                }
                catch { }
                try
                {
                    orderItem.UnitPrice = Convert.ToDouble(dr.Cells["Rate"].Value.ToString());
                }
                catch { }
                try
                {
                    orderItem.TotalPrice = Convert.ToDouble(dr.Cells["Total"].Value.ToString());
                }
                catch { }
                _totalProductPrice += orderItem.TotalPrice;
                listOrderItems.Add(orderItem);
            }
            try
            {
                order.TotalProductPrice = Convert.ToDouble(_totalProductPrice);
            }
            catch { }
            try
            {
                if (!String.IsNullOrEmpty(lblGrandTotal.Text.ToString()))
                    order.Grandtotal = Convert.ToDouble(lblGrandTotal.Text.ToString());
                else
                    order.Grandtotal = Convert.ToDouble(lblDiscountedPrice.Text.ToString());
            }
            catch { }

            int response = orderBAL.addOrder(order, listOrderItems);
            using (Report rpt = new Report(response))
            {
                rpt.ShowDialog();
            }

            if (response != null && response != 0)
            {
                MessageBox.Show("Invoice saved successfully.");
            }
            else
            {
                MessageBox.Show("Something Went Wrong.");
            }
        }

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "Zero";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " Lakhes ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "";
                var unitsMap = new[]   
                {  
                    "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"  
                };
                var tensMap = new[]   
                {  
                    "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"  
                };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        

    }
}
