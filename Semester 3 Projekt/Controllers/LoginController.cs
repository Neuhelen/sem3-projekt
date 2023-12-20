using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Semester_3_Projekt.Classes;
using System.Text;

namespace Semester_3_Projekt.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBget _dbGet;
        public DBInsert dBInsert;

        private const string HardcodedUsername = "admin";
        private const string HardcodedPassword = "admin";

        public LoginController()
        {
            dBInsert = new DBInsert();
            _dbGet = new DBget();
        }

        //Login
        [HttpGet]
        public IActionResult Index()
        {
            //dBInsert.addUser(HardcodedUsername, HashPassword(HardcodedPassword), "Admin");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _dbGet.GetUserByUsername(username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
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

                return RedirectToAction("Index", "Monitor"); // Redirect to the Monitor page if credentials are correct
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View("Index"); // Stay on the login page if credentials are wrong
        }

        //Verify the password with the stored hash in the database 
        private bool VerifyPassword(string password, string storedHash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == storedHash;
        }

        //Hash the password
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        //Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login"); // Redirect to the Login page after logout
        }
    }
}
