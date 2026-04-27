using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public record ResetPasswordDto
{
	public String? UserName { get; init; }
	
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Password is required")]
	public String? Password { get; init; }

	[DataType(DataType.Password)]
	[Required(ErrorMessage = "ConfirmPassword is required")]
	[Compare("Password", ErrorMessage = "password and confirm password must match")]
	public String? ConfirmPassword { get; init; }
}