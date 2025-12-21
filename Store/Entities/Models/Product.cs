using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Product
{
	public int ProductId { get; set; }

	[Required(ErrorMessage = "Product Name is Required")]
	public String ProductName { get; set; } = String.Empty;
	
	[Required(ErrorMessage = "Price is Required")]
	public decimal Price { get; set; }

	public int? CategoryId { get; set; }	// Foreing Key
	public Category? Category { get; set; }	// Navigation Proporty
}