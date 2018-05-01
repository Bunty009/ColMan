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
    public partial class EditMaterial : Form
    {
        BAL.MaterialBAL materialBAL = new BAL.MaterialBAL();
        Entity.Material material = new Entity.Material();

        public EditMaterial(int MaterialId)
        {
            InitializeComponent();
            materialBAL = new BAL.MaterialBAL();
            BindEditForm(new object[] { MaterialId, "" });
        }

        private void BindEditForm(object[] data)
        {
            materialBAL = new BAL.MaterialBAL();
            DataSet table = new DataSet();
            table = materialBAL.getMaterials(data);
            BindFields(table);
        }

        private void BindFields(DataSet table)
        {
            DataTable dtMaterial = table.Tables[0];
            DataTable dtPrice = table.Tables[2];
            try
            {
                lblMaterialId.Text = dtMaterial.Rows[0]["MaterialId"].ToString();
            }
            catch { }
            try
            {
                txtEditMaterialCode.Text = dtMaterial.Rows[0]["MaterialCode"].ToString();
            }
            catch { }
            try
            {
                txtEditMaterialName.Text = dtMaterial.Rows[0]["MaterialName"].ToString();
            }
            catch { }
            try
            {
                txtEditMatDescription.Text = dtMaterial.Rows[0]["MaterialDescription"].ToString();
            }
            catch { }
            try
            {
                txtEditHSNCode.Text = dtMaterial.Rows[0]["HSNCode"].ToString();
            }
            catch { }
            try
            {
                txtEditSize1.Text = dtPrice.Rows[0]["OneKGSize"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice1.Text = dtPrice.Rows[0]["OneKGPrice"].ToString();
            }
            catch { }
            try
            {
                txtEditSize2.Text = dtPrice.Rows[0]["HalfKGSize"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice2.Text = dtPrice.Rows[0]["HalfKGPrice"].ToString();
            }
            catch { }
            try
            {
                txtEditSize3.Text = dtPrice.Rows[0]["Size1"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice3.Text = dtPrice.Rows[0]["Price1"].ToString();
            }
            catch { }
            try
            {
                txtEditSize4.Text = dtPrice.Rows[0]["Size2"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice4.Text = dtPrice.Rows[0]["Price2"].ToString();
            }
            catch { }
            try
            {
                txtEditSize5.Text = dtPrice.Rows[0]["Size3"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice5.Text = dtPrice.Rows[0]["Price3"].ToString();
            }
            catch { }
            try
            {
                txtEditSize6.Text = dtPrice.Rows[0]["Size4"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice6.Text = dtPrice.Rows[0]["Price4"].ToString();
            }
            catch { }

            try
            {
                txtEditSize7.Text = dtPrice.Rows[0]["Size5"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice7.Text = dtPrice.Rows[0]["Price5"].ToString();
            }
            catch { }

            try
            {
                txtEditSize8.Text = dtPrice.Rows[0]["Size6"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice8.Text = dtPrice.Rows[0]["Price6"].ToString();
            }
            catch { }

            try
            {
                txtEditSize9.Text = dtPrice.Rows[0]["Size7"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice9.Text = dtPrice.Rows[0]["Price7"].ToString();
            }
            catch { }

            try
            {
                txtEditSize10.Text = dtPrice.Rows[0]["Size8"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice10.Text = dtPrice.Rows[0]["Price8"].ToString();
            }
            catch { }

            try
            {
                txtEditSize11.Text = dtPrice.Rows[0]["Size9"].ToString();
            }
            catch { }
            try
            {
                txtEditPrice11.Text = dtPrice.Rows[0]["Price9"].ToString();
            }
            catch { }

            //try
            //{
            //    txtEditSize12.Text = dtPrice.Rows[0][""].ToString();
            //}
            //catch { }
            //try
            //{
            //    txtEditPrice12.Text = dtPrice.Rows[0][""].ToString();
            //}
            //catch { }
            
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control txt in groupBox1.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
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
            materialBAL = new BAL.MaterialBAL();
            material = new Entity.Material();
            material.Prices = new Entity.Price();
            try
            {
                // Material
                try
                {
                    material.MaterialId = Convert.ToInt32(lblMaterialId.Text.ToString());
                }
                catch { }
                try
                {
                    material.MaterialCode = txtEditMaterialCode.Text.ToString();
                }
                catch { }
                try
                {
                    material.MaterialName = txtEditMaterialName.Text.ToString();
                }
                catch { }
                try
                {
                    material.MaterialDescription = txtEditMatDescription.Text.ToString();
                }
                catch { }
                try
                {
                    material.HSNCode = txtEditHSNCode.Text.ToString();
                }
                catch { }
                //Prices
                try
                {
                    material.Prices.MaterialCode = txtEditMaterialCode.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.OneKGSize = txtEditSize1.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.OneKGPrice = Convert.ToDouble(txtEditPrice1.Text);
                }
                catch { }
                try
                {
                    material.Prices.HalfKGSize = txtEditSize2.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.HalfKGPrice = Convert.ToDouble(txtEditPrice2.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size1 = txtEditSize3.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price1 = Convert.ToDouble(txtEditPrice3.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size2 = txtEditSize4.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price2 = Convert.ToDouble(txtEditPrice4.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size3 = txtEditSize5.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price3 = Convert.ToDouble(txtEditPrice5.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size4 = txtEditSize6.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price4 = Convert.ToDouble(txtEditPrice6.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size5 = txtEditSize7.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price5 = Convert.ToDouble(txtEditPrice7.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size6 = txtEditSize8.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price6 = Convert.ToDouble(txtEditPrice8.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size7 = txtEditSize9.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price7 = Convert.ToDouble(txtEditPrice9.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size8 = txtEditSize10.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price8 = Convert.ToDouble(txtEditPrice10.Text);
                }
                catch { }
                try
                {
                    material.Prices.Size9 = txtEditSize11.Text.ToString();
                }
                catch { }
                try
                {
                    material.Prices.Price9 = Convert.ToDouble(txtEditPrice11.Text);
                }
                catch { }
                // Common
                material.ModifiedBy = "admin";

                if (materialBAL.updateMaterial(material))
                {
                    MessageBox.Show("Material updated successfully.");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Something went wrong.");
                }
            }
            catch { }


        }


    }
}
