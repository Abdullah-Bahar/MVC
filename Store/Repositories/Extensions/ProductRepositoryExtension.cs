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
}