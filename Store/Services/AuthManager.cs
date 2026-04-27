using AutoMapper;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services.Contracts;

public class AuthManager : IAuthService
{
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly UserManager<IdentityUser> _userManager;


	public AuthManager(
		RoleManager<IdentityRole> roleManager,
		UserManager<IdentityUser> userManager
		)
	{
		_roleManager = roleManager;
		_userManager = userManager;
	}

	public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

	public IEnumerable<IdentityUser> GetAllUsers()
	{
		return _userManager.Users.ToList();
	}
}