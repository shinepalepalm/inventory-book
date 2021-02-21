using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common.Enums;
using InventoryBook.Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventoryBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var userViewModel = _accountService.Login(loginViewModel);

                if (userViewModel == null)
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }

                await Authenticate(userViewModel.Login, userViewModel.Role);
                return RedirectToAction("Index", "Item");
            }

            return View(loginViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                _accountService.Register(registerViewModel);
                await Authenticate(registerViewModel.Login, registerViewModel.Role);

                return RedirectToAction("Index", "Item");
            }

            return View(registerViewModel);
        }

        [HttpGet]
        public JsonResult CheckLoginForRegistration(string Login) => Json(!_accountService.IsLoginExists(Login));

        private async Task Authenticate(string login, Role role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
