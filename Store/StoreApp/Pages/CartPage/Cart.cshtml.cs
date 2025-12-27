using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace StoreApp.Pages.CartPage;

public class CartModel : PageModel
{
	private readonly IServiceManager _maneger;
	public readonly Cart Cart;

	// Kullanıcının hangi sayfandan geliği bilgisini tutmak için (geri döndüğünde aynı sayfaya gitsin)
	public string ReturnUrl { get; set; } = "/";

	public CartModel(IServiceManager maneger, Cart cart)
	{
		_maneger = maneger;
		Cart = cart;
	}

	public void OnGet(string returnUrl)
	{
		ReturnUrl = returnUrl ?? "/";
	}

	public IActionResult OnPost(int productId, string returnUrl)
	{
		Product? product = _maneger.PorductService.GetOneProduct(productId, false);

		if (product is not null)
		{
			Cart.AddItem(product, 1);
		}

		// Anonymous Object return eder
		return RedirectToPage(new { returnUrl }); // returnUrl
	}

	public IActionResult OnPostRemove(int id, string returnUrl)
	{
		Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId.Equals(id)).Product);
		return Page();
	}
}