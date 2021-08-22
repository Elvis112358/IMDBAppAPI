using AutoMapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork;
using EF = EFModels;

namespace DAL
{
    public class UserDal : IUserDal
    {
        private IMapper _mapper;
        private IUnitOfWork _uow;

        public UserDal(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        public User GetUser(int id)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.User efUser = _uow.Users.GetById(id);
                User user = _mapper.Map<User>(efUser);
                return user;
            }
        }

        public User GetUserByUsername(string username)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.User efUser = _uow.Users.GetByUsername(username);
                User user = _mapper.Map<User>(efUser);
                return user;
            }
        }

        public User AddUser(User user)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.User efUser = _mapper.Map<EF.User>(user);
                efUser = _uow.Users.AddUser(efUser);
                _uow.Commit();
                return _mapper.Map<User>(efUser);
            }
        }
    }
}
