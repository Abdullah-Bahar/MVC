using System.Net.Http.Headers;
using Entities.Models;

namespace Repository.Extensions;

public static class ProductRepositoryExtension
{
	// Aşğıdaki method ile LINQ zincirinin kendisi extension edilmiş oldu.
	// IQueryable<Product> dönen her şey extension edeilir
	public static IQueryable<Product> FilterByCategoryId(this IQueryable<Product> products, int? categoryId)
	{
		if (categoryId is null)
		{
			return products;
		}
		else
		{
			return products.Where(prd => prd.CategoryId.Equals(categoryId));
		}
	}

	public static IQueryable<Product> FilterBySearchTerm(this IQueryable<Product> products, String? searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
		{
			return products;
		}
		else
		{
			return products.Where(prd => prd.ProductName
									.ToLower()
									.Contains(searchTerm.ToLower()));
		}
	}

	public static IQueryable<Product> FilterByPrice(this IQueryable<Product> products, int minPrice, int maxPrice, bool isValidPrice)
	{
		if (isValidPrice)
		{
			return products.Where(prd => prd.Price <= maxPrice && prd.Price >= minPrice);
		}
		else
		{
			return products;
		}
	}
}