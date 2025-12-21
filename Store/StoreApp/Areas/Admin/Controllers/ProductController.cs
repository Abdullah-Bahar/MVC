using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
	private readonly IServiceManager _manager;

	public ProductController(IServiceManager manager)
	{
		_manager = manager;
	}

	public IActionResult Index()
	{
		var models = _manager.PorductService.GetAllProducts(false);
		return View(models);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create([FromForm] Product product)
	{
		if (ModelState.IsValid)
		{
			_manager.PorductService.CreateOneProduct(product);

			return RedirectToAction("Index");
		}

		return View();
	}

	public IActionResult Update([FromRoute(Name = "id")] int id)
	{
		var model = _manager.PorductService.GetOneProduct(id, false);
		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Update(Product product)
	{
		if (ModelState.IsValid)
		{
			_manager.PorductService.UpdateOneProduct(product);

			return RedirectToAction("Index");
		}

		return View();
	}

	[HttpGet]
	public IActionResult Delete([FromRoute(Name = "id")] int id)
	{
		_manager.PorductService.DeleteOneProduct(id);
		return RedirectToAction("Index");
	}
}