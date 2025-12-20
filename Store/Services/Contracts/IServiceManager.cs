namespace Services.Contracts;

public interface IServiceManager
{
	IPorductService PorductService { get; }
	ICategoryService CategoryService { get; }
}