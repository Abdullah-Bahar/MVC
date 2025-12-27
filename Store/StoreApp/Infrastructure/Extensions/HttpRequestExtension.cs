namespace StoreApp.Infrastructure.Extensions;

public static class HttpRequestExtension
{
	public static string PathAndQuery(this HttpRequest request)
	{
		return request.QueryString.HasValue
			? $"{request.Path}{request.QueryString}"
			: request.Path.ToString();
	}
}

// HttpRequest için extension method.
// İsteğin Path + QueryString bilgisini tek bir string olarak döndürür.
// Redirect sonrası returnUrl olarak kullanılmak üzere tasarlanmıştır.