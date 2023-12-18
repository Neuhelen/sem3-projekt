using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Semester_3_Projekt.Controllers
{
    public class LoginController : Controller
    {
        private const string HardcodedUsername = "admin";
        private const string HardcodedPassword = "admin";

        //Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == HardcodedUsername && password == HardcodedPassword)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Monitor");
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View("Index");
        }

        // Logout
        public IActionResult Logout()
        {
            return View(); // Returns the logout view
        }

        [HttpPost]
        public async Task<IActionResult> PerformLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login"); // Redirect to the Login page
        }
    }
}
