using Entities.Models;

namespace Services.Contracts;

public interface IProductService
{
	IEnumerable<Product> GetAllProducts(bool trackChange);
	Product? GetOneProduct(int id, bool trackChange);
	void CreateOneProduct(Product product);
	void UpdateOneProduct(Product product);
	void DeleteOneProduct(int id);
}