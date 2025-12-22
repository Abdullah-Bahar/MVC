using Entities.DTOs;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ProductManager : IProductService
{
	private readonly IRepositoryManager _manager;

	public ProductManager(IRepositoryManager manager)
	{
		_manager = manager;
	}

	public void CreateOneProduct(ProductDtoForInsertion productDto)
	{
		// DTO nesnesi ile Entity nesnesi arasında eşleme yapılıyor
		Product product = new Product()
		{
			ProductName = productDto.ProductName,
			Price = productDto.Price,
			CategoryId = productDto.CategoryId
		};

		_manager.Product.CreateOneProduct(product);
		_manager.Save();
	}

	public void DeleteOneProduct(int id)
	{
		Product? product = GetOneProduct(id, false);
		if (product is not null)
		{
			_manager.Product.DeleteOneProduct(product);
			_manager.Save();
		}
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

	public void UpdateOneProduct(Product product)
	{
		var model = _manager.Product.GetOneProduct(product.ProductId, true);
		model.ProductName = product.ProductName;
		model.Price = product.Price;
		_manager.Save();
	}
}