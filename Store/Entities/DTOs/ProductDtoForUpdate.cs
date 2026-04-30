namespace Entities.DTOs;

public record ProductDtoForUpdate : ProductDto
{
	public bool ShowCase { get; set; }
}