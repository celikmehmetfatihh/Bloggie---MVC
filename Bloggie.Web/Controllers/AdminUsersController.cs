using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	// Admins and superadmins are the ones can view this page
	[Authorize(Roles = "Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly UserManager<IdentityUser> _userManager;

		public AdminUsersController(IUserRepository userRepository,
			UserManager<IdentityUser> userManager)
        {
			_userRepository = userRepository;
			_userManager = userManager;
		}

		[HttpGet]
        public async Task<IActionResult> List()
		{
			var users = await _userRepository.GetAllAsync();
			var usersViewModel = new UserViewModel();
			usersViewModel.Users = new List<User>();

			foreach (var user in users)
			{
				usersViewModel.Users.Add(new User
				{
					Id = Guid.Parse(user.Id),
					Username = user.UserName,
					EmailAddress = user.Email
				});
			}

			return View(usersViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> List(UserViewModel userViewModel)
		{
			var identityUser = new IdentityUser()
			{
				UserName = userViewModel.Username,
				Email = userViewModel.Email,
			};

			var identityResult = await _userManager.CreateAsync(identityUser, userViewModel.Password);

			if (identityResult is not null)
			{
				if (identityResult.Succeeded)
				{
					// Assign roles to this user
					var roles = new List<string> { "User" };

					if (userViewModel.isAdmin)
					{
						roles.Add("Admin");
					}

					identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

					if (identityResult is not null)
					{
						if (identityResult.Succeeded)
						{
							return RedirectToAction("List", "AdminUsers");
						}
					}

				}
			}

			return View();
		}

		[HttpPost]
		// We are getting the id from asp-route-id
		public async Task<IActionResult> Delete(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());

			if (user != null)
			{
				var identityResult = await _userManager.DeleteAsync(user);

				if (identityResult is not null)
				{
					if (identityResult.Succeeded)
					{
						return RedirectToAction("List", "AdminUsers");
					}
				}
			}

			return View();
		}
	}
}
