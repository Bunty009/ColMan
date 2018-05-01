using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace VighnhartaColors
{
    public partial class Login : Form
    {
        BAL.UserBAL userBAL = new BAL.UserBAL();

        public Login()
        {
            InitializeComponent();
            userBAL = new BAL.UserBAL();
        }
        
        private void lblForgetPassword_Click(object sender, EventArgs e)
        {
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUserName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string uName = Convert.ToString(txtUserName.Text);
                string password = Convert.ToString(txtPassword.Text);
                if (uName != "" && password != "")
                {
                    DataTable dt = userBAL.getUsers(new object[]{ 0, uName });
                    if (dt.Rows.Count > 0)
                    {
                        string passs = dt.Rows[0]["Password"].ToString();
                        byte[] bytes = Convert.FromBase64String(passs);
                        string str = Encoding.UTF8.GetString(bytes);
                        if (password == str)
                        {
                            this.Hide();
                            Home home = new Home();
                            home.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Credentails.");
                            txtUserName.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                        }
                    }
                    else 
                    {
                        MessageBox.Show("User does not exists.");
                        txtUserName.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Credentails.");
                    txtUserName.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }
            }
            catch (Exception ex) { Exceptions.WriteExceptionLog(ex); }
        }
    
    }
}
