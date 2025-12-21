using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")] // Area olarak eklenen controller'ın başına bu atributte'nin eklenmesi gerekir.
public class DashboardController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}