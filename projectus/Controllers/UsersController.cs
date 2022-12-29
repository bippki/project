using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using projectus.Entities;
using projectus.BL.Interfaces;
using projectus.Models;

namespace projectus.Controllers
{
    public class UsersController : Controller
    {
        IUserBL _bl;
        public UsersController(IUserBL bl)
        {
            _bl = bl;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = _bl.GetByLogin(loginModel.Login);

            if (user != null && user.Password == loginModel.Password)
            {
                var identity = new UserIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            }

            return RedirectToAction("Index", "Home");
        }

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterModel regModel)
		{
			if (ModelState.IsValid)
            {
                _bl.Register(regModel);
                return RedirectToAction("Login");
            }
            return View(regModel);
		}


		public IActionResult Get(int id)
        {
            var user = _bl.GetById(id);

            if (user != null)
            {
                return Json(user);
            }
            else
            {
                return Json("no user found");
            }

        }

    }
}
