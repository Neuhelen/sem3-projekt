using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Semester_3_Projekt.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Semester_3_Projekt.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: User/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = new User { Name = username, Role = UserRole.Admin };
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // this redirects to the monitor page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        //Post: User/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
    }
}
