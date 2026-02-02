using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreApp.Infrastructure;

/*
	---------------------------------
		Tag Helpers
	---------------------------------
	
	* TagHelper, Razor render edilirken HTML tag’lerini yakalayıp çıktıyı manipüle eden bir mekanizmadır.
*/


[HtmlTargetElement("table")]
public class TableTagHelper : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		// <table> etiketi render edilirken class attribute'una aşağıda derğerler yazılır
		// (var olan class bilgisinin üstüne üstüne yazılır, var olana ekleme yapmaz)
		output.Attributes.SetAttribute("class", "table table-hover"); // ("key", "value")
	}
}