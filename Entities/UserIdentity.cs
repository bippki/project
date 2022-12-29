using projectus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace projectus.Entities
{
	public class UserIdentity : ClaimsIdentity
	{
		public int Id { get; set; }

		public UserIdentity(User user, string authenticationType = "Cookie") : base(GetUserClaims(user), authenticationType)
		{
			Id = user.Id;
		}

		private static List<Claim> GetUserClaims(User user)
		{
			var result = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login),
				new Claim(ClaimTypes.Role, "DefaultRole"),
			};

			return result;
		}
	}
}
