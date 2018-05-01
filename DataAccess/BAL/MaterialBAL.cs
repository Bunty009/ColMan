using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using System.Data;

namespace BAL
{
    public class MaterialBAL
    {
        MaterialDAL materialDAL = null;

        public MaterialBAL()
        {
            materialDAL = new MaterialDAL();
        }

        public bool addMaterial(Material material)
        {
            return materialDAL.addMaterial(material);
        }

        public bool updateMaterial(Material material)
        {
            return materialDAL.updateMaterial(material);
        }

        public DataSet getMaterials(object[] data)
        {
            return materialDAL.getMaterials(data);
        }

        public DataTable getMaterialsForExcel()
        {
            return materialDAL.getMaterialsForExcel();
        }

        public bool deleteMaterial(int MaterialId)
        {
            return materialDAL.deleteMaterial(MaterialId);
        }

        public DataSet GetProductsForChallan(object[] data)
        {
            return materialDAL.GetProductsForChallan(data);
        }


    }
}
