using Entities.Models;

namespace Services.Contracts;

public interface IPorductService
{
	IEnumerable<Product> GetAllProducts(bool trackChange);
	Product? GetOneProduct(int id, bool trackChange);	
}