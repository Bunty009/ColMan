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
    public partial class EditCustomer : Form
    {
        BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();

        public EditCustomer(int CustomerId)
        {
            InitializeComponent();
            customerBAL = new BAL.CustomerBAL();
            BindEditForm(new object[] { CustomerId, "" });
        }

        private void BindEditForm(object[] data)
        {
            customerBAL = new BAL.CustomerBAL();
            DataTable table = new DataTable();
            table = customerBAL.getCustomers(data);
            BindFields(table);
        }

        private void BindFields(DataTable table)
        {
            try
            {
                lblCustomerId.Text = table.Rows[0]["CustomerId"].ToString();
            }
            catch { }
            try
            {
                txtEditCustomerName.Text = table.Rows[0]["Name"].ToString();
            }
            catch { }
            try
            {
                txtEditCustomerEmailId.Text = table.Rows[0]["EmailId"].ToString();
            }
            catch { }
            try
            {
                txtEditCustomerMobileNo.Text = table.Rows[0]["PhoneNo"].ToString();
            }
            catch { }
            try
            {
                txtEditCustomerGSTN.Text = table.Rows[0]["GSTN"].ToString();
            }
            catch { }
            try
            {
                txtEditAddressLine1.Text = table.Rows[0]["AddressLine1"].ToString();
            }
            catch { }
            try
            {
                txtEditAddressLine2.Text = table.Rows[0]["AddressLine2"].ToString();
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

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            Entity.Customer customer = new Entity.Customer();
            try
            {
                customer.CustomerId = Convert.ToInt32(lblCustomerId.Text.ToString());
                customer.Name = txtEditCustomerName.Text.ToString();
                customer.EmailId = txtEditCustomerEmailId.Text.ToString();
                customer.PhoneNo = txtEditCustomerMobileNo.Text.ToString();
                customer.GSTN = txtEditCustomerGSTN.Text.ToString();

                customer.AddressLine1 = txtEditAddressLine1.Text.ToString();
                customer.AddressLine2 = txtEditAddressLine2.Text.ToString();
                customer.City = txtEditCity.Text.ToString();
                customer.State = txtEditState.Text.ToString();
                customer.ZipCode = Convert.ToInt32(txtEditPinCode.Text.ToString());
                customer.ModifiedBy = "Admin";

                if (customerBAL.updateCustomer(customer))
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

    }
}
