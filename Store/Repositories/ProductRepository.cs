using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Models;

namespace Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
	public ProductRepository(RepositoryContext context) : base(context)
	{
	}

	public void CreateOneProduct(Product product) => Create(product);

	public void DeleteOneProduct(Product product) => Remove(product);

	public IQueryable<Product> GetAllProducts(bool trackChange) => FindAll(trackChange);

	public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
	{
		return p.CategoryId is null
			? _context
				.Products
				.Include(prd => prd.Category)
			: _context
				.Products
				.Include(prd => prd.Category)
				.Where(prd => prd.CategoryId.Equals(p.CategoryId));
	}

	public Product? GetOneProduct(int id, bool trackChange)
	{
		return FindByCondition(p => p.ProductId.Equals(id), trackChange);
	}

	public void GetOneUpdate(Product product) => Update(product);

	public IQueryable<Product> GetShowcaseProducts(bool trackChange)
	{
		return FindAll(trackChange)
			.Where(p => p.ShowCase.Equals(true));
		
	}
}