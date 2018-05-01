using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MaterialDAL
    {
        SqlParameter[] parameters;

        internal bool addMaterial(Material material)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@pMaterialCode",material.MaterialCode ),
                    new SqlParameter("@pMaterialName",material.MaterialName ),
                    new SqlParameter("@pMaterialDescription",material.MaterialDescription ),
                    new SqlParameter("@pHSNCode",material.HSNCode ),
                    new SqlParameter("@pOneKGSize",material.Prices.OneKGSize ),
                    new SqlParameter("@pOneKGPrice",material.Prices.OneKGPrice ),
                    new SqlParameter("@pHalfKGSize",material.Prices.HalfKGSize ),
                    new SqlParameter("@pHalfKGPrice",material.Prices.HalfKGPrice ),
                    new SqlParameter("@pSize1",material.Prices.Size1 ),
                    new SqlParameter("@pPrice1", material.Prices.Price1),
                    new SqlParameter("@pSize2",material.Prices.Size2 ),
                    new SqlParameter("@pPrice2", material.Prices.Price2),
                    new SqlParameter("@pSize3",material.Prices.Size3 ),
                    new SqlParameter("@pPrice3", material.Prices.Price3),
                    new SqlParameter("@pSize4",material.Prices.Size4 ),
                    new SqlParameter("@pPrice4", material.Prices.Price4),
                    new SqlParameter("@pSize5",material.Prices.Size5 ),
                    new SqlParameter("@pPrice5", material.Prices.Price5),
                    new SqlParameter("@pSize6",material.Prices.Size6 ),
                    new SqlParameter("@pPrice6", material.Prices.Price6),
                    new SqlParameter("@pSize7",material.Prices.Size7 ),
                    new SqlParameter("@pPrice7", material.Prices.Price7),
                    new SqlParameter("@pSize8",material.Prices.Size8 ),
                    new SqlParameter("@pPrice8", material.Prices.Price8),
                    new SqlParameter("@pSize9",material.Prices.Size9 ),
                    new SqlParameter("@pPrice9", material.Prices.Price9),
                    new SqlParameter("@IsActive",material.IsActive ),
                    new SqlParameter("@IsDeleted",material.IsDeleted ),
                    new SqlParameter("@CreatedBy",material.CreatedBy )
                };
                material.MaterialId = Convert.ToInt32(SqlDBHelper.ExecuteNonQueryReturnData("app_AddMaterials", CommandType.StoredProcedure, parameters, "pMaterialId"));
                if (material.MaterialId == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal bool deleteMaterial(int materialId)
        {
            try
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@pMaterialId",materialId)

                };
                return SqlDBHelper.ExecuteNonQuery("app_DeleteMaterial", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

        internal bool updateMaterial(Material material)
        {
            try
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@pMaterialId",material.MaterialId ),
                    new SqlParameter("@pMaterialCode",material.MaterialCode ),
                    new SqlParameter("@pMaterialName",material.MaterialName ),
                    new SqlParameter("@pMaterialDescription",material.MaterialDescription ),
                    new SqlParameter("@pHSNCode",material.HSNCode ),
                    new SqlParameter("@pOneKGSize",material.Prices.OneKGSize ),
                    new SqlParameter("@pOneKGPrice",material.Prices.OneKGPrice ),
                    new SqlParameter("@pHalfKGSize",material.Prices.HalfKGSize ),
                    new SqlParameter("@pHalfKGPrice",material.Prices.HalfKGPrice ),
                    new SqlParameter("@pSize1",material.Prices.Size1 ),
                    new SqlParameter("@pPrice1", material.Prices.Price1),
                    new SqlParameter("@pSize2",material.Prices.Size2 ),
                    new SqlParameter("@pPrice2", material.Prices.Price2),
                    new SqlParameter("@pSize3",material.Prices.Size3 ),
                    new SqlParameter("@pPrice3", material.Prices.Price3),
                    new SqlParameter("@pSize4",material.Prices.Size4 ),
                    new SqlParameter("@pPrice4", material.Prices.Price4),
                    new SqlParameter("@pSize5",material.Prices.Size5 ),
                    new SqlParameter("@pPrice5", material.Prices.Price5),
                    new SqlParameter("@pSize6",material.Prices.Size6 ),
                    new SqlParameter("@pPrice6", material.Prices.Price6),
                    new SqlParameter("@pSize7",material.Prices.Size7 ),
                    new SqlParameter("@pPrice7", material.Prices.Price7),
                    new SqlParameter("@pSize8",material.Prices.Size8 ),
                    new SqlParameter("@pPrice8", material.Prices.Price8),
                    new SqlParameter("@pSize9",material.Prices.Size9 ),
                    new SqlParameter("@pPrice9", material.Prices.Price9),
                    new SqlParameter("@pModifiedBy",material.ModifiedBy )
                };
                return SqlDBHelper.ExecuteNonQuery("app_UpdateMaterials", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

        internal DataSet getMaterials(object[] data)
        {
            string materialId = "", searchKey = "";
            try
            {
                materialId = Convert.ToString(data[0]);
            }
            catch { materialId = ""; }
            try
            {
                searchKey = Convert.ToString(data[1]);
            }
            catch { }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pMaterialId", materialId),
                new SqlParameter("@pSearchKey", searchKey)
            };
            DataSet table = new DataSet();
            try
            {
                table = SqlDBHelper.ExecuteParamerizeSelectCommand("app_GetMaterials", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }
        
        internal DataTable getMaterialsForExcel()
        {
            DataTable table = new DataTable();
            parameters = new SqlParameter[]
            {
            };
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetMaterialsForExcel", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }
        
        internal DataSet GetProductsForChallan(object[] data)
        {
            string action = "", materialId = "";
            try
            {
                action = Convert.ToString(data[0]);
            }
            catch { }
            try
            {
                materialId = Convert.ToString(data[1]);
            }
            catch { materialId = "0"; }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pAction", action),
                new SqlParameter("@pMaterialId", materialId)
            };
            DataSet table = new DataSet();
            try
            {
                table = SqlDBHelper.ExecuteParamerizeSelectCommand("app_GetMaterialsForDdl", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex) { }
            return table;
        }


    }
}
