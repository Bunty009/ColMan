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
    public partial class EditUser : Form
    {
        BAL.UserBAL userBAL = new BAL.UserBAL();

        public EditUser(int UserId)
        {
            InitializeComponent();
            BindEditForm(new object[]{ UserId, "", ""});
        }

        private void BindEditForm(object[] data)
        {
            userBAL = new BAL.UserBAL();
            DataTable table = new DataTable();
            table = userBAL.getUsers(data);
            BindFields(table);            
        }

        private void BindFields(DataTable table)
        {
            try
            {
                lblUserId.Text = table.Rows[0]["UserId"].ToString();
            }
            catch { }
            try
            {
                txtEditFirstName.Text = table.Rows[0]["FirstName"].ToString();
            }
            catch { }
            try
            {
                txtEditLastName.Text = table.Rows[0]["LastName"].ToString();
            }
            catch { }
            try
            {
                txtEditUserName.Text = table.Rows[0]["UserName"].ToString();
            }
            catch { }
            try
            {
                txtEditEmailId.Text = table.Rows[0]["EmailId"].ToString();
            }
            catch { }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Entity.User user = new Entity.User();
            try
            {
                user.UserId = Convert.ToInt32(lblUserId.Text);
            }
            catch { }
            try
            {
                user.FirstName = txtEditFirstName.Text.ToString();
            }
            catch { }
            try
            {
                user.LastName = txtEditLastName.Text.ToString();
            }
            catch { }
            try
            {
                user.UserName = txtEditUserName.Text.ToString();
            }
            catch { }
            try
            {
                user.EmailId = txtEditEmailId.Text.ToString();
            }
            catch { }
            user.ModifiedBy = "admin";
            userBAL = new BAL.UserBAL();
            try
            {
                if (userBAL.updateUser(user))
                {
                    MessageBox.Show("Updated Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something Went Wrong.");
                }
            }
            catch (Exception ex) { Exceptions.WriteExceptionLog(ex); MessageBox.Show("Something Went Wrong."); }
        }
        



    }
}
