using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.Models;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions;

public static class ServiceExtension
{
	public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		// DbContext'in servis kaydı yapıldı
		services.AddDbContext<RepositoryContext>(options =>
		{
			// appsettings.json içinden gelen connection string
			options.UseSqlite(configuration.GetConnectionString("sqlconnection"),

			// * EFCore default olarak migration klasörünü DBContext'in olduğu yerde açar.
			// * Aşağıdaki configuration ile Migration/ klasörü DbContext'in olduğu yer yerine 
			// StoreApp projesi içerisinde oluşturulur. 
			b => b.MigrationsAssembly("StoreApp"));
		});
	}

	public static void ConfigureSession(this IServiceCollection services)
	{
		// Session verileri RAM'de tutulacak. App restart yerse silinir.
		services.AddDistributedMemoryCache();
		// Bu uygulama session kullanacak
		services.AddSession(options =>
		{
			options.Cookie.Name = "StoreApp.Session";       // Session adını değiştirdik
			options.IdleTimeout = TimeSpan.FromMinutes(10); // İlgili bilgileri 10 dk boyunca tut
		});

		// Controller/PageModel dışında (ör. Service katmanında) geçerli HTTP isteğine ve Session’a erişebilmemizi sağlar.
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		// I. Bir tane Cart nesnesi üretilecek ve herkes onu kullanacak
		// builder.Services.AddSingleton<Cart>();

		// II. Üretilecek Cart class'ı SessionCart olaeak gelecek artık
		// Ve artık herkes aynı nesneyi değil, her istek için bu servis kaydı çalışacak
		services.AddScoped<Cart>(c => SessionCart.GetCart(c));
	}

	public static void ConfigureRepositoryRegistration(this IServiceCollection services)
	{
		// Repository IoC kayıtları yapılıyor
		services.AddScoped<IRepositoryManager, RepositoryManager>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
	}

	public static void ConfigureServiceRegistration(this IServiceCollection services)
	{
		// Services IoC kayıtları yapılıyor
		services.AddScoped<IServiceManager, ServiceManager>();
		services.AddScoped<IProductService, ProductManager>();
		services.AddScoped<ICategoryService, CategoryManager>();
		services.AddScoped<IOrderService, OrderManager>();
	}
}