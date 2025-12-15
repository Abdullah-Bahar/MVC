namespace Basics.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = String.Empty;
    public String FullName => $"{FirstName} {LastName.ToUpper()}";
    public int Age { get; set; }
}