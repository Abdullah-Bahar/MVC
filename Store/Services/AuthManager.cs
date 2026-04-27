using AutoMapper;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services.Contracts;

public class AuthManager : IAuthService
{
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IMapper _mapper;


	public AuthManager(
		RoleManager<IdentityRole> roleManager,
		UserManager<IdentityUser> userManager,
	 	IMapper mapper
		)
	{
		_roleManager = roleManager;
		_userManager = userManager;
		_mapper = mapper;
	}

	public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

	public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
	{
		var user = _mapper.Map<IdentityUser>(userDto);
		var result = await _userManager.CreateAsync(user, userDto.Password);

		if (!result.Succeeded)
		{
			throw new Exception("User creation failed.");
		}

		if (userDto.Roles.Count > 0)
		{
			var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);

			if (!roleResult.Succeeded)
			{
				throw new Exception("Failed to add user to roles.");
			}
		}

		return result;
	}

	public IEnumerable<IdentityUser> GetAllUsers()
	{
		return _userManager.Users.ToList();
	}
}