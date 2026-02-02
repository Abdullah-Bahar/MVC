using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Models;

/*
	ViewContext şunları içerir:
	- HttpContext
	- RouteData
	- Action adı
	- Controller adı
	- Request bilgisi
	- ViewData / TempData
*/

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "pagination")]
public class PageLinkTagHelper : TagHelper
{
	// Url üretmek için kullanılıyor
	private readonly IUrlHelperFactory _urlHelperFactory;

	[ViewContext] // Bu prop'u Razor mataru otomatik doldursun
	[HtmlAttributeNotBound] // Bu prop html attribute'larına bağlanmasın
	public ViewContext? ViewContext { get; set; } // URL üretmek için lazım

	[HtmlAttributeName("pagination")]
	public Pagination PageModel { get; set; }
	[HtmlAttributeName("page-action")]
	public String? PageAction { get; set; }

	// aşağıdaki prop'lar isim uyuştuğu için otomatik bind edilir.
	public bool PageClassesEnabled { get; set; } = false;
	public string PageClass { get; set; } = String.Empty;
	public string PageClassNormal { get; set; } = String.Empty;
	public string PageClassSelected { get; set; } = String.Empty;

	public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
	{
		_urlHelperFactory = urlHelperFactory;
	}

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		if (ViewContext is not null && PageModel is not null)
		{
			IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
			TagBuilder result = new TagBuilder("div");

			for (int i = 1; i <= PageModel.TotalPages; i++)
			{
				TagBuilder tag = new TagBuilder("a");

				// ViewContext mevcut action’ı bilse de TagHelper’ın reusable olması için
				// hedef action dışarıdan parametre olarak alınır.
				tag.Attributes["href"] = urlHelper.Action(PageAction, new
				{
					// Query String'e çevriliyor
					PageNumber = i
					
					// PageSize için de değer verilerek sayfa başına kaç ürün listeleneceği belirlenebilir
					// Lakin default bırakıldı (PageSize = 6)
				});

				if (PageClassesEnabled)
				{
					/*
						AddCssClass
					*/
					tag.AddCssClass(PageClass);
					tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
				}

				tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
			}

			// TagBuilder ile oluşturulan <div> etiketi eklenmiş olur.
			// output.Content.AppendHtml(result);

			// TagBuilder ile oluşturulan <div> etiketini değil de altındakiler eklenmiş olur.
			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}