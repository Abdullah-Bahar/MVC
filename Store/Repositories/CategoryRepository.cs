using Entities.Models;
using Repositories.Contracts;
using Repositories.Models;

namespace Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
	public CategoryRepository(RepositoryContext context) : base(context)
	{
	}
}