using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.Contracts;

namespace StoreApp.Controllers;

public class ProductController : Controller
{
	private readonly IRepositoryManager _manager;

	public ProductController(IRepositoryManager manager)
	{
		_manager = manager;
	}


	public IActionResult Index()
	{
		var model = _manager.Product.GetAllProducts(false).ToList();
		// var model = _manager.Product.FindAll(false).ToList();

		return View(model);
	}

	public IActionResult Get(int id)
	{
		// Product product = _context.Products.First(p => p.ProductId.Equals(id));

		throw new NotImplementedException();
	}
}
