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
    public partial class ConfirmBox : Form
    {
        public int ToDeleteId { get; set; }
        public string OfModule { get; set; }

        public ConfirmBox(int Id, string module)
        {
            InitializeComponent();
            ToDeleteId = Id;
            OfModule = module;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            switch (OfModule) 
            {
                case "User":
                    BAL.UserBAL userBAL = new BAL.UserBAL();
                    if(userBAL.deleteUser(ToDeleteId))
                    {
                        MessageBox.Show("Deleted Successfully");
                        this.Close();
                    }
                    break;
                case "Customer":
                    BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
                    if (customerBAL.deleteCustomer(ToDeleteId))
                    {
                        MessageBox.Show("Deleted Successfully");
                        this.Close();
                    }
                    break;
                case "Supplier":
                    BAL.SupplierBAL supplierBAL = new BAL.SupplierBAL();
                    if (supplierBAL.deleteSupplier(ToDeleteId))
                    {
                        MessageBox.Show("Deleted Successfully");
                        this.Close();
                    }
                    break;
                case "Material":
                    BAL.MaterialBAL materialBAL = new BAL.MaterialBAL();
                    if (materialBAL.deleteMaterial(ToDeleteId))
                    {
                        MessageBox.Show("Deleted Successfully");
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
