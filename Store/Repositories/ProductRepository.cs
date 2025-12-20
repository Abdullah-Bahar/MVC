using Entities.Models;
using Repositories.Contracts;
using Repositories.Models;

namespace Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
	public ProductRepository(RepositoryContext context) : base(context)
	{
	}

	public IQueryable<Product> GetAllProducts(bool trackChange) => FindAll(trackChange);

	public Product? GetOneProduct(int id, bool trackChange)
	{
		return FindByCondition(p => p.ProductId.Equals(id), trackChange);
	}
}