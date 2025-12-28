using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
	private readonly IProductService _porductService;
	private readonly ICategoryService _categoryService;
	private readonly IOrderService _orderService;

	public ServiceManager(IProductService porductService, ICategoryService categoryService, IOrderService orderService)
	{
		_porductService = porductService;
		_categoryService = categoryService;
		_orderService = orderService;
	}

	public IProductService PorductService => _porductService;

	public ICategoryService CategoryService => _categoryService;

	public IOrderService OrderService => _orderService;
}