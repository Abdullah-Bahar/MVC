using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.Models;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers(); // => API ile çalışılacaksa bu kullanılır

// Servis kaydı yapılıyor
// Servis kayıtları Middleware yapılarıyla birlikte kullanılabilir
builder.Services.AddControllersWithViews(); // Controller + View => Servis Kaydı

// DbContext'in servis kaydı yapıldı
builder.Services.AddDbContext<RepositoryContext>(options =>
{
	// appsettings.json içinden gelen connection string
	options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),

	// * EFCore default olarak migration klasörünü DBContext'in olduğu yerde açar.
	// * Aşağıdaki configuration ile Migration/ klasörü DbContext'in olduğu yer yerine 
	// StoreApp projesi içerisinde oluşturulur. 
	b => b.MigrationsAssembly("StoreApp"));
});

// Repository IoC kayıtları yapılıyor
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Services IoC kayıtları yapılıyor
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();


var app = builder.Build();

// Uygulama static dosyalarda kullanacak. (wwwroot altındakiler)
app.UseStaticFiles();

// Rederiction mekanizması eklendi
app.UseHttpsRedirection();

// MapControllerRoute() ile tanımlanan routing işlemlerinin dikkate alınmasını sağlar
app.UseRouting();

// Aşağıdaki kullanım net6 öncesi için. Haka çalışır ama önerilmez.
// app.UseEndpoints( e => { ... });

// Admin Area için route tanımı
app.MapAreaControllerRoute(
	name: "admin",
	areaName: "Admin",
	pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
