using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MoviesDBContext context) : base(context) { }
        public override User GetById(object id)
        {
            return _dbSet.SingleOrDefault(user => user.Id == (int)id);
        }
        public User GetByUsername(string username)
        {
            return _dbSet.SingleOrDefault(user => user.Username == username);
        }

        public User AddUser(User user)
        {
            if (_dbSet.Where(u => u.Username.Equals(user.Username)).Any())
                //throw new RepositoryException("Username already taken");
                throw new ArgumentException("Username already taken");
            return _dbSet.Attach(user).Entity;
        }
    }
}
