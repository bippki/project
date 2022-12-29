
using projectus.DAL.Interfaces;
using projectus.Entities;
using projectus.ORMDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace projectus.DAL
{
	public class UserDBRepo : IUserDAL
	{
		public User GetById(int id)
		{
			using (var ctx = new DefaultDBContext())
			{
				return ctx.Users.Find(id);
			}
		}
		public User GetByLogin(string login)
		{
			using (var ctx = new DefaultDBContext())
			{
				return ctx.Users.FirstOrDefault(item => item.Login == login);
			}
		}

		public void Add(RegisterModel regModel)
		{
			using (var ctx = new DefaultDBContext())
			{
				ctx.Users.Add(new User { Name = regModel.Name, Login = regModel.Login, Password = regModel.Password, RegisterDate = DateTime.Now });
				ctx.SaveChanges();
			}
		}
	}
}
