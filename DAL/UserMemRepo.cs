using projectus.DAL.Interfaces;
using projectus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectus.DAL
{
	public class UserMemRepo : IUserDAL
	{
		private int id = 4;
		private List<User> users;
		public UserMemRepo()
		{
			users = new List<User>() 
			{
				new User() { Id = 0, Name = "Ivan", Login = "ivan42", Password = "mypass", RegisterDate = new DateTime(100000) },
				new User() { Id = 1, Name = "Ioan", Login = "aytep", Password = "pass123", RegisterDate = new DateTime(2100000) },
				new User() { Id = 2, Name = "Ion", Login = "copp_is_my_life", Password = "contest", RegisterDate = new DateTime(22100000) },
				new User() { Id = 3, Name = "P", Login = "koshi", Password = "m>n", RegisterDate = new DateTime(222100000) },
			};
		}

		public User GetById(int id)
		{
			return users.FirstOrDefault(item => item.Id == id);
		}
		public User GetByLogin(string login)
		{
			return users.FirstOrDefault(item => item.Login == login);
		}

		public void Add(RegisterModel regModel)
		{
			users.Add(new User { Id = id++, Name = regModel.Name, Login = regModel.Login, Password = regModel.Password, RegisterDate = DateTime.Now });
		}
	}
}
