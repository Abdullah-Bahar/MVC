using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ProductManager : IProductService
{
	private readonly IRepositoryManager _manager;
	private readonly IMapper _mapper;

	public ProductManager(IRepositoryManager manager, IMapper mapper)
	{
		_manager = manager;
		_mapper = mapper;
	}

	public void CreateOneProduct(ProductDtoForInsertion productDto)
	{
		Product product = _mapper.Map<Product>(productDto);

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

	public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChange)
	{
		var product = GetOneProduct(id, trackChange);
		var productDto = _mapper.Map<ProductDtoForUpdate>(product);
		return productDto;
	}

	public void UpdateOneProduct(ProductDtoForUpdate productDto)
	{
		// Yeni referans atanacağı için EF Core bu nesneyi izlemeyi bırakır ve ilgili değişiklikler yakalanamaz
		// Dolayısıyla EF Core'dan update işlemi kullanılacak  
		var product = _mapper.Map<Product>(productDto);
		_manager.Product.GetOneUpdate(product);
		_manager.Save();
	}
}