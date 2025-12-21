using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		GetSelectListCategoryItems();
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

		GetSelectListCategoryItems();
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

	private void GetSelectListCategoryItems()
	{
		/*
			Mantıksal Hata :
				- Create formu yanlış doldurulup gönderildiğinde (POST),
				ModelState geçersiz olur ve View tekrar render edilir.
				- Ancak bu sırada kategori dropdown’u (SelectList) kaybolur.
			
			Neden :
				- SelectList sadece GET Create action’ında dolduruluyordu
				- POST Create sırasında ModelState geçersiz olunca `return View()` çalışıyor
				- Ancak ViewBag request bazlıdır
				- POST sırasında ViewBag tekrar doldurulmadığı için SelectList null olur

			Çözüm:
        		- SelectList oluşturma işlemi bu metoda taşındı
        		- GET ve POST action’larında bu metot çağrılarak dropdown’un her durumda 
				dolu gelmesi sağlandı
		*/

		ViewBag.Categories = new SelectList(
			_manager.CategoryService.GetAllCategories(false),
			"CategoryId",		// Value olarak belirlenen alan
			"CategoryName"		// Kullanıcıya gösterilecek text
			// "1"				// Default seçili değer (Ama verilmedi)
		);
	}
}