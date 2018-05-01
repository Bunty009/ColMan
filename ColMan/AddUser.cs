using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using Utilities;

namespace VighnhartaColors
{
    public partial class AddUser : Form
    {
        BAL.UserBAL userBAL = new BAL.UserBAL();
        Entity.User user = new Entity.User();

        public AddUser()
        {
            InitializeComponent();
            userBAL = new BAL.UserBAL();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string Pass = "", ConfirPass ="";
            Entity.User user = new Entity.User();
            try
            {
                Pass = txtPassword.Text.ToString();
                ConfirPass = txtConfirmPassword.Text.ToString();
                
                //byte[] bytes = Convert.FromBase64String(base64);
                //string str = Encoding.UTF8.GetString(bytes);
                     
                if (Pass == ConfirPass)
                {
                    user.FirstName = Convert.ToString(txtFirstName.Text);
                    user.LastName = Convert.ToString(txtLastName.Text);
                    user.UserName = Convert.ToString(txtUserName.Text);
                    user.EmailId = Convert.ToString(txtEmailId.Text);
                    user.PhoneNo = Convert.ToInt32("8754516");
                    user.CreatedBy = "admin";
                    byte[] bytes = Encoding.UTF8.GetBytes(Pass);
                    string base64 = Convert.ToBase64String(bytes);
                    user.Password = base64;

                    if (userBAL.addUser(user))
                    {
                        MessageBox.Show("User added Successfully.");
                        this.Close();
                    }
                    else 
                    {
                        MessageBox.Show("Password does not matched.");
                    }
                }
                else 
                {
                    MessageBox.Show("Password does not matched.");
                    txtPassword.Text = string.Empty;
                    txtConfirmPassword.Text = string.Empty;
                }
            }
            catch (Exception ex) { Exceptions.WriteExceptionLog(ex); }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmailId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    
    }
}
