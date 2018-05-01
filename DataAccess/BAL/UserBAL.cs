using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BAL
{
    public class UserBAL
    {
        UserDAL userDAL = null;  

        public UserBAL()
        {
            userDAL = new UserDAL();
        }

        public bool addUser(User user)
        {
            return userDAL.addUser(user);
        }

        public bool updateUser(User user)
        {
            return userDAL.updateUser(user);
        }

        public DataTable getUsers(object[] data)
        {
            return userDAL.getUsers(data);
        }
        
        public bool deleteUser(int UserId)
        {
            return userDAL.deleteUser(UserId);
        }

    }
}
