using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
	private readonly IProductService _porductService;
	private readonly ICategoryService _categoryService;

	public ServiceManager(IProductService porductService, ICategoryService categoryService)
	{
		_porductService = porductService;
		_categoryService = categoryService;
	}

	public IProductService PorductService => _porductService;

	public ICategoryService CategoryService => _categoryService;
}