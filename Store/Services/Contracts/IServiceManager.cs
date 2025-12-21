namespace Services.Contracts;

public interface IServiceManager
{
	IProductService PorductService { get; }
	ICategoryService CategoryService { get; }
}