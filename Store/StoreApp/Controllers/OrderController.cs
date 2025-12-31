using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers;

public class OrderController : Controller
{
	private readonly IServiceManager _manager;
	private readonly Cart _cart;

	public OrderController(IServiceManager manager, Cart cart)
	{
		_manager = manager;
		_cart = cart;
	}

	// Order nesnesi gönderilmese de çalışmakta. Lakin gönderilmesi Best Practice
	public ViewResult Checkout() => View(new Order());

	[HttpPost]
	public IActionResult Checkout([FromForm] Order order)
	{
		if (_cart.Lines.Count() == 0)
		{
			ModelState.AddModelError("", "Sorryyy, kartınız boşşş");
		}

		if (ModelState.IsValid)
		{
			// ToArray() ile Referans paylaşımı yapılır. Böylece aynı listede çalışma riskinden kaçılır. 
			order.Lines = _cart.Lines.ToArray();

			_manager.OrderService.SaveOrder(order);
			_cart.Clear();

			return RedirectToPage("/Complete/Default", new { OrderId = order.OrderId });
		}

		return View();
	}
}