using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Contracts;

namespace StoreApp.Components;

public class ProductSummaryViewComponent : ViewComponent
{
	private readonly IServiceManager _manager;

	public ProductSummaryViewComponent(IServiceManager manager)
	{
		_manager = manager;
	}

	// Render edilmesi intenmeyen bir view component
	public string Invoke()
	{
		return _manager.PorductService.GetAllProducts(false).Count().ToString();
	}
}