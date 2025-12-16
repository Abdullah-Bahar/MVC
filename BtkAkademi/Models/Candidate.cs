using System.ComponentModel.DataAnnotations;

namespace BtkAkademi.Models;

public class Candidate
{
	[Required(ErrorMessage = "E-Mail Zorunlu")]
	[EmailAddress] // Email adresinin formatını kontrol eder.
	public String? Email { get; set; } = String.Empty;

	[Required(ErrorMessage = "Firstname Zorunlu")]
	public String? FirstName { get; set; } = String.Empty;
	
	[Required(ErrorMessage = "Lastname Zorunlu")]
    public String? LastName { get; set; } = String.Empty;
    public String? FullName => $"{FirstName} {LastName?.ToUpper()}";  // expression-bodied member
    public int? Age { get; set; }
    public String? SelectedCourse { get; set; } = String.Empty;
    public DateTime ApplyAt { get; set; }

    public Candidate()
    {
        ApplyAt = DateTime.Now;
    }
}