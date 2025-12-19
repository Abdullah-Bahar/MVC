using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Models;

namespace Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T>
	where T : class, new()
{
	protected readonly RepositoryContext _context;

	protected RepositoryBase(RepositoryContext context)
	{
		_context = context;
	}

	public IQueryable<T> FindAll(bool trackChange)
	{
		return trackChange
			? _context.Set<T>()
			: _context.Set<T>().AsNoTracking();
	}
}