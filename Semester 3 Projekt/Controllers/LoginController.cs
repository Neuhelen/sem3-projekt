using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Semester_3_Projekt.Controllers
{
    public class LoginController : Controller
    {
        private bool ValidateUser(string username, string password)
        {
            // Check if the username and password match a stored user, check against a database or other user store
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                   return false;
            }

            // Placeholder logic for demonstration
            return (username == "admin" && password == "admin");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ValidateUser(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    // Add other claims as needed
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
