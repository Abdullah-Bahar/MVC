using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers;

public class ProductController : Controller
{
	private readonly IServiceManager _manager;

	public ProductController(IServiceManager manager)
	{
		_manager = manager;
	}


	public IActionResult Index()
	{
		var model = _manager.PorductService.GetAllProducts(false);

		return View(model);
	}

	// public IActionResult Get([FromForm(Name = "id")] int id)
	public IActionResult Get(int id)
	{
		var model = _manager.PorductService.GetOneProduct(id, false);

		return View(model);
	}
}
