using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class CategoryManager : ICategoryService
{
	private readonly IRepositoryManager _manager;

	public CategoryManager(IRepositoryManager manager)
	{
		_manager = manager;
	}

	public IEnumerable<Category> GetAllCategories(bool trackChange)
	{
		return _manager.Category.FindAll(trackChange);
	}
}