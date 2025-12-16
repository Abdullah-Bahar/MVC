using BtkAkademi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BtkAkademi.Controllers;

public class CourseController : Controller
{
	public IActionResult Index()
	{
		var models = Repository.Applications;

		return View(models);
	}

	public IActionResult Apply()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken] // Güvenlik İçin
	public IActionResult Apply([FromForm] Candidate model) // Verinin nereden geleceği yer de ifade edilebilir
	// Örnek : FormForm, FormServices, vb.
	{
		if (Repository.Applications.Any(c => c.Email.Equals(model.Email)))
		{
			ModelState.AddModelError("", "Bu Email zaten kayıtlı");
		}

		if (ModelState.IsValid)
		{
			Repository.Add(model);

			return View("Feedback", model);
			// Aynı url altında farklı bir view göstermiş olduk.
			// Action'ı olmadan bir view görüntüledik
		}

		return View();
	}
}