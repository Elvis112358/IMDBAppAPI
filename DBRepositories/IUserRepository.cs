using EFModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepositories
{
    public interface IUserRepository :IRepository<User>
    {
        User GetByUsername(string username);
        User AddUser(User user);
    }
}
