using Entities.Models;

namespace Repositories.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
	IQueryable<Product> GetAllProducts(bool trackChange);
	IQueryable<Product> GetShowcaseProducts(bool trackChange);
	Product? GetOneProduct(int id, bool trackChange);
	void CreateOneProduct(Product product);
	void DeleteOneProduct(Product product);
	void GetOneUpdate(Product product);
}