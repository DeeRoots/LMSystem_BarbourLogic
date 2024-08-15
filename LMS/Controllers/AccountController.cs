using LMS.DatabaseModels;
using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(DeleteUserViewModel deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(deleteUser.UserEmail))
                {
                    var user = await _userManager.FindByEmailAsync(deleteUser.UserEmail);

                    if (user != null)
                    {
                        var result = await _userManager.DeleteAsync(user);

                        if (result.Succeeded)
                            return RedirectToAction("Index", "Register");
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Delete Attempt");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Cannot find User");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (registerViewModel.Password == registerViewModel.ConfirmPassword)
                {
                    var user = new ApplicationUser
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email,
                        EmailConfirmed = true,

                    };

                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Register");
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Register Attempt");
                    }


                }
            }

            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(editUser.UserEmail))
                {                  
                    var user = await _userManager.FindByEmailAsync(editUser.UserEmail);

                    if (user != null)
                    {
                        user.UserName = editUser.UserNewName;

                        if (editUser.UserEmail != editUser.UserNewEmail && !string.IsNullOrEmpty(editUser.UserNewEmail))
                        {
                            user.Email = editUser.UserNewEmail;
                        }

                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                            return RedirectToAction("Index", "Register");
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Edit Attempt");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User not found");
                    }
                }
            }

            return View("");
        }
    }
}
