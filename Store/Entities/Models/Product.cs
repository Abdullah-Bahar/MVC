using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Product
{
	public int ProductId { get; set; }
	public String ProductName { get; set; } = String.Empty;
	public decimal Price { get; set; }
	public String? Summary { get; set; } = String.Empty;
	public String? ImageUrl { get; set; } = String.Empty;
	public int? CategoryId { get; set; }	// Foreing Key
	public Category? Category { get; set; }	// Navigation Proporty
}