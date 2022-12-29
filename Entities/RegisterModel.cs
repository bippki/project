using System.ComponentModel.DataAnnotations;

namespace projectus.Entities
{
	public class RegisterModel
	{
		[MinLength(3, ErrorMessage = "Name should have at least three chars!")]
		public string Name { get; set; }
		[MinLength(1, ErrorMessage = "Login should have at least one char!")]
		public string Login { get; set; }
		[MinLength(6, ErrorMessage = "Password should have at least six chars!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
