using System.Reflection.Metadata;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                // Assing the user role to newly created user
                var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                {
                    // Show success notification
                    return RedirectToAction("Register");
                }
            }

            // Show error notification
            return View();
        }

        [HttpGet]
        // ReturnUrl returned automatically when you are not authenticated, and try to enter some page, first goes to login method with returnUrl
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = ReturnUrl };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password,
                false, false); // isPersistent and lockOnFailure is false

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl)) 
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                // Go to Home Controller's Index method
                return RedirectToAction("Index", "Home");
            }

            // Show error notification
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
