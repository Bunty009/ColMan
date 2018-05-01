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
    public partial class AddCustomer : Form
    {
        BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();

        public AddCustomer()
        {
            InitializeComponent();
            customerBAL = new BAL.CustomerBAL();
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
                customer.Name = txtCustomerName.Text.ToString();
                customer.EmailId = txtCustomerEmailId.Text.ToString();
                customer.PhoneNo = txtCustomerMobileNo.Text.ToString();
                customer.GSTN = txtCustomerGSTN.Text.ToString();

                customer.AddressLine1 = txtAddressLine1.Text.ToString();
                customer.AddressLine2 = txtAddressLine2.Text.ToString();
                customer.City = txtCity.Text.ToString();
                customer.State = txtState.Text.ToString();
                customer.ZipCode = Convert.ToInt32(txtPinCode.Text.ToString());
                customer.IsActive = true;
                customer.CreatedBy = "Admin";

                if (customerBAL.addCustomer(customer))
                {
                    MessageBox.Show("Customer added Successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something Went Wrong.");
                }
            }
            catch (Exception ex){ }
        }

    }
}
