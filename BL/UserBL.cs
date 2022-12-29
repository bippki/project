using projectus.DAL.Interfaces;
using projectus.BL.Interfaces;
using projectus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectus.BL
{
    public class UserBL : IUserBL
    {
        private IUserDAL _dal;
        public UserBL(IUserDAL dal)
        {
            _dal = dal;
        }

        public User GetByLogin(string login)
        {
            return _dal.GetByLogin(login);
        }

        public User GetById(int id)
        {
            return _dal.GetById(id);
        }

        public void Register(RegisterModel regModel)
        {
            _dal.Add(regModel);
        }
    }
}
