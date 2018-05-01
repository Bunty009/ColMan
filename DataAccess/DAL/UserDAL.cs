using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DataAccess;

namespace DAL
{
    public class UserDAL
    {
        SqlParameter[] parameters;

        internal bool addUser(User user)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@pFirstName",user.FirstName ),
                new SqlParameter("@pLastName",user.LastName ),
                new SqlParameter("@pUserName",user.UserName ),
                new SqlParameter("@pEmailId",user.EmailId ),
                new SqlParameter("@pPassword", user.Password),
                new SqlParameter("@pPhoneNo", user.PhoneNo),
                new SqlParameter("@pCreatedBy", user.CreatedBy)   
            };
                user.UserId = Convert.ToInt32(SqlDBHelper.ExecuteNonQueryReturnData("app_AddUsers", CommandType.StoredProcedure, parameters, "pUserId"));
                if (user.UserId == 0)
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
        
        internal bool updateUser(User user)
        {
            try
            {
                parameters = new SqlParameter[] 
                {
                    new SqlParameter("@pUserId",user.UserId),
                    new SqlParameter("@pFirstName",user.FirstName ),
                    new SqlParameter("@pLastName",user.LastName ),
                    new SqlParameter("@pUserName",user.UserName ),
                    new SqlParameter("@pEmailId",user.EmailId ),
                    //new SqlParameter("@pPhoneNo", user.PhoneNo),
                    new SqlParameter("@pModifiedBy", user.ModifiedBy)
                };
                return SqlDBHelper.ExecuteNonQuery("app_UpdateUser", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

        internal DataTable getUsers(object[] data)
        {
            string userId ="",userName = "", searchKey ="";
            try 
            {
                userId = Convert.ToString(data[0]);
            }
            catch { }
            try
            {
                userName = Convert.ToString(data[1]);
            }
            catch { userName = ""; }
            try
            {
                searchKey = Convert.ToString(data[2]);
            }
            catch { }
            parameters = new SqlParameter[]
            {
                new SqlParameter("@pUserId", userId),
                new SqlParameter("@pUserName", userName),
                new SqlParameter("@pSearchKey", searchKey)
            };
            DataTable table = new DataTable();
            try
            {
                table = SqlDBHelper.ExecuteParamerizedSelectCommand("app_GetUsers", CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex){ }
            return table;
        }
    
        internal bool deleteUser(int UserId)
        {
            try
            {
                parameters = new SqlParameter[] 
				{
					new SqlParameter("@pUserId",UserId)                   
								 
				};
                return SqlDBHelper.ExecuteNonQuery("app_DeleteUser", CommandType.StoredProcedure, parameters);
            }
            catch
            {
                return false;
            }
        }

    }
}
