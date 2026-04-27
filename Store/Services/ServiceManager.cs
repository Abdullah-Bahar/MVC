using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
	private readonly IProductService _porductService;
	private readonly ICategoryService _categoryService;
	private readonly IOrderService _orderService;
	private readonly IAuthService _authService;

	public ServiceManager(
		IProductService porductService,
		ICategoryService categoryService,
		IOrderService orderService,
		IAuthService authService
		)
	{
		_porductService = porductService;
		_categoryService = categoryService;
		_orderService = orderService;
		_authService = authService;
	}

	public IProductService PorductService => _porductService;

	public ICategoryService CategoryService => _categoryService;

	public IOrderService OrderService => _orderService;

	public IAuthService AuthService => _authService;
}