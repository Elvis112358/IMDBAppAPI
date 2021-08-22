using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUserDal
    {
        User GetUser(int id);
        User GetUserByUsername(string username);
        User AddUser(User user);
    }
}
