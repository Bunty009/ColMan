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
    public partial class AddMaterial : Form
    {
        BAL.MaterialBAL materialBAL = new BAL.MaterialBAL();
        Entity.Material material = new Entity.Material();
        
        public AddMaterial()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control txt in groupBox1.Controls)
            {
                if(txt.GetType() == typeof(TextBox))
                    ((TextBox)(txt)).Text = string.Empty;
            }
            foreach (Control txt in groupBox2.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
                    ((TextBox)(txt)).Text = string.Empty;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaterialCode.Text) && string.IsNullOrEmpty(txtmaterialName.Text))
            {
                MessageBox.Show("You must enter Material Code & Name.");
                return;
            }
            if (string.IsNullOrEmpty(txtSize1.Text) && string.IsNullOrEmpty(txtPrice1.Text)) 
            {
                MessageBox.Show("You must enter price for size1.");
                return;
            }
            AddMaterials();
        }

        private void AddMaterials() 
        {
            materialBAL = new BAL.MaterialBAL();
            material = new Entity.Material();
            material.Prices = new Entity.Price();
            try
            {
                // Material
                try
                {
                    material.MaterialCode = txtMaterialCode.Text.ToString();
                }
                catch { }
                try
                {
                    material.MaterialName = txtmaterialName.Text.ToString();
                }
                catch { }
                try
                {
                    material.MaterialDescription = txtMatDescription.Text.ToString();
                }
                catch { }
                try
                {
                    material.HSNCode = txtHSNCode.Text.ToString();
                }
                catch { }
                //Prices
                try
                {
                    material.Prices.MaterialCode = txtMaterialCode.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.OneKGSize = txtSize1.Text.ToString();
                }
                catch { }
                try
                { 
                    material.Prices.OneKGPrice = Convert.ToDouble(txtPrice1.Text);
                }
                catch { }
                try
                {
                    material.Prices.HalfKGSize = txtSize2.Text.ToString();
                }
                catch { }
                try
                { 
                    material.Prices.HalfKGPrice = Convert.ToDouble(txtPrice2.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size1 = txtSize3.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price1 = Convert.ToDouble(txtPrice3.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size2 = txtSize4.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price2 = Convert.ToDouble(txtPrice4.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size3 = txtSize5.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price3 = Convert.ToDouble(txtPrice5.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size4 = txtSize6.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price4 = Convert.ToDouble(txtPrice6.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size5 = txtSize7.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price5 = Convert.ToDouble(txtPrice7.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size6 = txtSize8.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price6 = Convert.ToDouble(txtPrice8.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size7 = txtSize9.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price7 = Convert.ToDouble(txtPrice9.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size8 = txtSize10.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price8 = Convert.ToDouble(txtPrice10.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size9 = txtSize11.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price9 = Convert.ToDouble(txtPrice11.Text);
                }
                catch { }
                // Common
                material.CreatedBy = "admin";
                material.IsActive = true;
                material.IsDeleted = false;

                if (materialBAL.addMaterial(material))
                {
                    MessageBox.Show("Material added Successfully.");
                }
                else
                {
                    MessageBox.Show("Something went Wrong.");
                }
            }
            catch (Exception ex) { Exceptions.WriteExceptionLog(ex); }
        }

        private void AddMaterial_Load(object sender, EventArgs e)
        {

        }

    }
}
