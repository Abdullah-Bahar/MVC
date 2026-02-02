using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class ProductController : Controller
{
	private readonly IServiceManager _manager;

	public ProductController(IServiceManager manager)
	{
		_manager = manager;
	}

	public IActionResult Index(ProductRequestParameters p)
	{
		var products = _manager.PorductService.GetAllProductsWithDetails(p);
		
		var pagination = new Pagination()
		{
			CurrentPage = p.PageNumber,
			ItemsPerPage = p.PageSize,
			// TotalItems = products.Count(),
			TotalItems = _manager.PorductService.GetAllProducts(false).Count()
		};

		return View(new ProductListViewModel()
		{
			Products = products,
			Pagination = pagination
		});
	}

	// public IActionResult Get([FromForm(Name = "id")] int id)
	public IActionResult Get(int id)
	{
		var model = _manager.PorductService.GetOneProduct(id, false);

		return View(model);
	}
}
