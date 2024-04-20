using JakubWiesniakLab3.Models;
using JakubWiesniakLab3.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace JakubWiesniakLab3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountService;

        public AccountController(IAccountRepository accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid || !string.Equals(registerViewModel.Password, registerViewModel.ConfirmPassword))
            {
                return View(registerViewModel);
            }

            _accountService.RegisterUser(registerViewModel);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = _accountService.GetUser(loginViewModel);
            if (user is null)
            {
                ModelState.AddModelError("Login/Haslo", "Niepoprawne dane logowania");
                return View(loginViewModel);
            }

            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(10)
            };
            Response.Cookies.Append("username", user.UserName, option);

            return RedirectToAction("Index", "Home");
        }
    }
}