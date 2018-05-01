using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VighnhartaColors
{
    public partial class EditSupplier : Form
    {
        BAL.SupplierBAL supplierBAL = new BAL.SupplierBAL();

        public EditSupplier(int SupplierId)
        {
            InitializeComponent();
            supplierBAL = new BAL.SupplierBAL();
            BindEditForm(new object[] { SupplierId, "" });
        }

        private void BindEditForm(object[] data)
        {
            supplierBAL = new BAL.SupplierBAL();
            DataTable table = new DataTable();
            table = supplierBAL.getSuppliers(data);
            BindFields(table);
        }

        private void BindFields(DataTable table)
        {
            try
            {
                lblSupplierId.Text = table.Rows[0]["SupplierId"].ToString();
            }
            catch { }
            try
            {
                txtEditSupplierName.Text = table.Rows[0]["Name"].ToString();
            }
            catch { }
            try
            {
                txtEditSupplierEmailId.Text = table.Rows[0]["EmailId"].ToString();
            }
            catch { }
            try
            {
                txtEditSupplierMobileNo.Text = table.Rows[0]["PhoneNo"].ToString();
            }
            catch { }
            try
            {
                txtEditSupplierGSTN.Text = table.Rows[0]["GSTN"].ToString();
            }
            catch { }
            try
            {
                txtEditSupplierAddressLine1.Text = table.Rows[0]["AddressLine1"].ToString();
            }
            catch { }
            try
            {
                txtEditSupplierAddressLine2.Text = table.Rows[0]["AddressLine2"].ToString();
            }
            catch { }
            try
            {
                txtEditCity.Text = table.Rows[0]["City"].ToString();
            }
            catch { }
            try
            {
                txtEditState.Text = table.Rows[0]["State"].ToString();
            }
            catch { }
            try
            {
                txtEditPinCode.Text = table.Rows[0]["ZipCode"].ToString();
            }
            catch { }
        }

        private void btnSaveSupplier_Click(object sender, EventArgs e)
        {
            Entity.Supplier supplier = new Entity.Supplier();
            try
            {
                supplier.SupplierId = Convert.ToInt32(lblSupplierId.Text.ToString());
                supplier.Name = txtEditSupplierName.Text.ToString();
                supplier.EmailId = txtEditSupplierEmailId.Text.ToString();
                supplier.PhoneNo = txtEditSupplierMobileNo.Text.ToString();
                supplier.GSTN = txtEditSupplierGSTN.Text.ToString();

                supplier.AddressLine1 = txtEditSupplierAddressLine1.Text.ToString();
                supplier.AddressLine2 = txtEditSupplierAddressLine2.Text.ToString();
                supplier.City = txtEditCity.Text.ToString();
                supplier.State = txtEditState.Text.ToString();
                supplier.ZipCode = Convert.ToInt32(txtEditPinCode.Text.ToString());
                supplier.ModifiedBy = "Admin";

                if (supplierBAL.updateSupplier(supplier))
                {
                    MessageBox.Show("Customer updated Successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something Went Wrong.");
                }
            }
            catch (Exception ex) { }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
