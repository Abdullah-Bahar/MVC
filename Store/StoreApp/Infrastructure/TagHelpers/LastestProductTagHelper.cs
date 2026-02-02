using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "products")]
public class LastestProductTagHelper : TagHelper
{
	private readonly IServiceManager _manager;
	
	[HtmlAttributeName("number-product")]
	public int NumberProduct { get; set; }

	public LastestProductTagHelper(IServiceManager manager)
	{
		_manager = manager;
	}

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		// Kapsayıcı div
		TagBuilder div = new TagBuilder("div");
		div.Attributes.Add("class", "my-3");

		// h6
		TagBuilder h6 = new TagBuilder("h6");
		h6.Attributes.Add("class", "lead");

		// icon
		TagBuilder i = new TagBuilder("i");
		i.Attributes.Add("class", "fa fa-box text-secondary");

		h6.InnerHtml.AppendHtml(i);
		h6.InnerHtml.AppendHtml(" Lastest Products");

		// ul
		TagBuilder ul = new TagBuilder("ul");
		var products = _manager.PorductService.GetLastestProducts(NumberProduct, false);
		
		// li & a
		foreach (var product in products)
		{
			TagBuilder li = new TagBuilder("li");
			TagBuilder a = new TagBuilder("a");

			a.InnerHtml.AppendHtml(product.ProductName);
			a.Attributes.Add("href", $"/product/get/{product.ProductId}");
			
			/* 
				- Bu şekilde link çalışmaz
				NOT: Built-in TagHelper’lar, başka bir TagHelper tarafından runtime’da üretilen 
			 	HTML üzerinde çalışmaz.
			*/
			// a.Attributes.Add("asp-controller", "Product");
			// a.Attributes.Add("asp-action", "Get");
			// a.Attributes.Add("asp-route-id", product.ProductId.ToString());
			
			li.InnerHtml.AppendHtml(a);
			ul.InnerHtml.AppendHtml(li);
		}

		div.InnerHtml.AppendHtml(h6);
		div.InnerHtml.AppendHtml(ul);
		output.Content.AppendHtml(div);
	}
}