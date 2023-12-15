using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Semester_3_Projekt.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Semester_3_Projekt.Classes;

namespace Semester_3_Projekt.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //Constructor
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            //Dependency injection
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //Login get
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //Login post
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //Check if model is valid
            if (ModelState.IsValid)
            {
                //Sign in user
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);

                //Check if sign in was successful
                if (result.Succeeded)
                {
                    //Redirect to home page
                    return RedirectToAction("Index", "Home");
                }

                //Add error message
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            //Return view
            return View(model);
        }

        //Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
