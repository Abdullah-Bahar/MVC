using Entities.Models;
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