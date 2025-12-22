using Entities.DTOs;
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
	public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, 
		[FromForm] IFormFile file) // ilgili input alanındaki name file olduğu için .NET Model Binding işlemini gerçekleştirir.
	{
		if (ModelState.IsValid)
		{
			// file operation
			string path = Path.Combine(				// Path.Combine() işletim sistemlerin kullanılan farklı ayraçlara ("/", "\") göre path'i birleştirir.
				Directory.GetCurrentDirectory(),	// Projenin çalıştığı kök dizin (“Bu uygulama nerede çalışıyor?”)
				"wwwroot", "image", file.FileName); // "wwwroot/image/filename"

			// using => Maliyeti yüksek, kaynak gerektiren işlemler için kullanılır (Kullan ve imha et)
			using (var stream = new FileStream(path, FileMode.Create)) 
			{
				// ilgli path'te bir dosya oluşturulur ve parametre olarak gelen dosyayı oraya kopyalar 
				await file.CopyToAsync(stream);

				/*
					* IFormFile, gelen dosyanın geçici temsilidir; FileStream ile bu geçici veriyi fiziksel diske bilinçli olarak yazdırırız; 
					wwwroot altına yazmamızın sebebi tarayıcıdan erişilebilir kılmaktır.
				*/
			}

			productDto.ImageUrl = String.Concat("/image/", file.FileName);

			_manager.PorductService.CreateOneProduct(productDto);
			return RedirectToAction("Index");
		}

		GetSelectListCategoryItems();
		return View();
	}

	public IActionResult Update([FromRoute(Name = "id")] int id)
	{
		var model = _manager.PorductService.GetOneProductForUpdate(id, false);
		GetSelectListCategoryItems(model.CategoryId);
		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile? file)
	// IFormFile null geçilebilir değilse ve update işlemin dosya seçimi yapılmadan başka bir güncelleme
	// yapıldığı takdirde NullReferenceException fırlatır.
	{
		if (ModelState.IsValid)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(),
				"wwwroot", "image", file.FileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			productDto.ImageUrl = String.Concat("/image/", file.FileName);

			_manager.PorductService.UpdateOneProduct(productDto);
			return RedirectToAction("Index");
		}

		GetSelectListCategoryItems(productDto.CategoryId);
		return View();
	}

	[HttpGet]
	public IActionResult Delete([FromRoute(Name = "id")] int id)
	{
		_manager.PorductService.DeleteOneProduct(id);
		return RedirectToAction("Index");
	}

	private void GetSelectListCategoryItems(int? categoryId = null)
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
			"CategoryId",       // Value olarak belirlenen alan
			"CategoryName",     // Kullanıcıya gösterilecek text
			categoryId          // Default seçili değer (Ama verilmedi)
		);
	}
}