using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ProductManager : IPorductService
{
	private readonly IRepositoryManager _manager;

	public ProductManager(IRepositoryManager manager)
	{
		_manager = manager;
	}

	public IEnumerable<Product> GetAllProducts(bool trackChange)
	{
		return _manager.Product.GetAllProducts(trackChange);
	}

	public Product? GetOneProduct(int id, bool trackChange)
	{
		var product = _manager.Product.GetOneProduct(id, trackChange);

		if (product is null)
		{
			throw new Exception("Product Not Found");
		}

		return product;
	}
}