using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreApp.Pages.Demo;

public class DemoModel : PageModel
{
	public String? FullName  => HttpContext?.Session?.GetString("name") ?? "No Name";

	public void OnGet()
	{

	}

	public void OnPost([FromForm] string name)
	{
		/*
			Session'da tutulabilecek veri tipleri :
				- byte[]	: Set()
				- int		: SetInt32()
				- stirng	: SetString()

			* ilgili bu methodlar key-value ikilisi şeklinde çalışır. 
		*/

		HttpContext.Session.SetString("name", name);
	}
}