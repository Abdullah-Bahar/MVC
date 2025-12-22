using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

/*
	record
		- Default olarak immutable bir türdür.
		- Value bazlı karşılaştırma yapar.
		- Class özelliklerini kullanma noktasında esenektir. (ctor gibi)
		- Veri taşıma odaklıdır. (DTO'lar için biçilmez kaftan)

	init
		- Nesne oluşturulurken değer atanmasına izir verir. Lakin değer
		atandıktan sonra değiştirmeye izin vermez.
*/

public record ProductDto
{
	public int ProductId { get; init; }

	[Required(ErrorMessage = "Product Name is Required")]
	public String ProductName { get; init; } = String.Empty;

	[Required(ErrorMessage = "Price is Required")]
	public decimal Price { get; init; }
	public String? Summary { get; init; } = String.Empty;
	public String? ImageUrl { get; set; } = String.Empty;
	public int? CategoryId { get; init; }
}