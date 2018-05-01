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
    public partial class AddSuppliers : Form
    {
        BAL.SupplierBAL supplierBAL = new BAL.SupplierBAL();

        public AddSuppliers()
        {
            InitializeComponent();
            supplierBAL = new BAL.SupplierBAL();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnSaveSupplier_Click(object sender, EventArgs e)
        {
            Entity.Supplier supplier = new Entity.Supplier();
            try
            {
                supplier.Name = txtSupplierName.Text.ToString();
                supplier.EmailId = txtSupplierEmailId.Text.ToString();
                supplier.PhoneNo = txtSupplierMobileNo.Text.ToString();
                supplier.GSTN = txtSupplierGSTN.Text.ToString();

                supplier.AddressLine1 = txtSupplierLine1.Text.ToString();
                supplier.AddressLine2 = txtSupplierLine2.Text.ToString();
                supplier.City = txtCity.Text.ToString();
                supplier.State = txtState.Text.ToString();
                supplier.ZipCode = Convert.ToInt32(txtPinCode.Text.ToString());
                supplier.IsActive = true;
                supplier.CreatedBy = "Admin";

                if (supplierBAL.addSupplier(supplier))
                {
                    MessageBox.Show("Supplier added Successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something Went Wrong.");
                }
            }
            catch (Exception ex) { }
        }


    }
}
