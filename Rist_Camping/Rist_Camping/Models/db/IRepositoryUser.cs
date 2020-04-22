using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rist_Camping.Models.db
{
    interface IRepositoryUser : IRepositoryBase
    {
        bool Insert(User user);
        bool Delete(int id);
        bool UpdateUserData(int id, User newUserData);
        User GetUser(int id);
        User Login(UserLogin user);
    }
}
