using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")] // Area olarak eklenen controller'ın başına bu atributte'nin eklenmesi gerekir.
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
	public IActionResult Index()
	{
		TempData["info"] = $"Tekrar Hoşgeldin, {DateTime.Now.ToShortDateString()}";
		return View();
	}
}