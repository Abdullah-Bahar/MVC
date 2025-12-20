using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
	private readonly IPorductService _porductService;
	private readonly ICategoryService _categoryService;

	public ServiceManager(IPorductService porductService, ICategoryService categoryService)
	{
		_porductService = porductService;
		_categoryService = categoryService;
	}

	public IPorductService PorductService => _porductService;

	public ICategoryService CategoryService => _categoryService;
}