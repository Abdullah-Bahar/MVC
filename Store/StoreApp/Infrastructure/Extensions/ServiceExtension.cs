using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
			options.UseSqlServer(configuration.GetConnectionString("mssqlconnection"),

			// * EFCore default olarak migration klasörünü DBContext'in olduğu yerde açar.
			// * Aşağıdaki configuration ile Migration/ klasörü DbContext'in olduğu yer yerine 
			// StoreApp projesi içerisinde oluşturulur. 
			b => b.MigrationsAssembly("StoreApp"));

			// EF Core loglarında parametre değerlerini de gösterir
			// dev açamasında logları kontrol etmek için açtık
			// prod'a çıktığında kapatılmalı (hassas bilgiler mevcut)
			options.EnableSensitiveDataLogging(true);
		});
	}

	public static void ConfigureIdentity(this IServiceCollection services)
	{
		services.AddIdentity<IdentityUser, IdentityRole>(options =>
		{
			options.SignIn.RequireConfirmedEmail = false;
			options.User.RequireUniqueEmail = true;
			options.Password.RequireUppercase = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireDigit = false;
			options.Password.RequiredLength = 6;
		})
		// Kullanıcıları veritabanında RepositoryContext üzerinden sakla
		.AddEntityFrameworkStores<RepositoryContext>();
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
		services.AddScoped<IAuthService, AuthManager>();
	}

	public static void ConfigureApplicationCookie(this IServiceCollection services)
	{
		services.ConfigureApplicationCookie(options =>
		{
			options.LoginPath = new PathString("/Account/Login");
			options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
			options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
			options.AccessDeniedPath = new PathString("/Account/AccessDenied");
		});
	}

	public static void ConfigureRouting(this IServiceCollection services)
	{
		services.AddRouting(options =>
		{
			options.LowercaseUrls = true; // Tüm url küçük harf olması durumu
			options.AppendTrailingSlash = false; // Tüm url'lerin sonuna "/" eklenip eklenmemesi
		});
	}
}