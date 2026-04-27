using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class AccountController : Controller
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;

	public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public IActionResult Login([FromQuery(Name = "returnUrl")] string returnUrl = "/")
	{
		return View(new LoginModel()
		{
			ReturnUrl = returnUrl
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login([FromForm] LoginModel model)
	{
		if (ModelState.IsValid)
		{
			IdentityUser user = await _userManager.FindByNameAsync(model.Name);

			if (user is not null)
			{
				// Hali hazırda bir kullanıcı var ise aktif oturumu sonlandıraccağız

				await _signInManager.SignOutAsync();
				if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
				{
					return Redirect(model.ReturnUrl ?? "/");
				}
			}

			ModelState.AddModelError("Error", "Invalid login or password.");
		}

		return View();
	}

	public async Task<IActionResult> Logout([FromQuery(Name = "returnUrl")] string returnUrl = "/")
	{
		await _signInManager.SignOutAsync();
		return Redirect(returnUrl);
	}

}
