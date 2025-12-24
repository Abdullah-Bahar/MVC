using Entities.Models;
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

builder.Services.AddRazorPages(); // Uygulamaya Razor Page'ler de eklendi.

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

// Session verileri RAM'de tutulacak. App restart yerse silinir.
builder.Services.AddDistributedMemoryCache();
// Bu uygulama session kullanacak
builder.Services.AddSession( options =>
{
	options.Cookie.Name = "StoreApp.Session";		// Session adını değiştirdik
	options.IdleTimeout = TimeSpan.FromMinutes(10); // İlgili bilgileri 10 dk boyunca tut
});
// Controller/PageModel dışında (ör. Service katmanında) geçerli HTTP isteğine ve Session’a erişebilmemizi sağlar.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Repository IoC kayıtları yapılıyor
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Services IoC kayıtları yapılıyor
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

// Bir tane Cart nesnesi üretilecek ve herkes onu kullanacak
builder.Services.AddSingleton<Cart>();

// AutoMapper DI'a kaydedilir
// Program.cs dosyasının bulunduğu assembly’i referans al ve bu assembly'de 
// Profile'dan türeyen tüm sınıflar bul. (Dinamik yapı)
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Uygulama static dosyalarda kullanacak. (wwwroot altındakiler)
app.UseStaticFiles();

// HTTP pipeline’a session middleware’i ekler
app.UseSession();

// Rederiction mekanizması eklendi
app.UseHttpsRedirection();

// MapControllerRoute() ile tanımlanan routing işlemlerinin dikkate alınmasını sağlar
app.UseRouting();

// Aşağıdaki kullanım net6 öncesi için. Hala çalışır ama önerilmez.
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

app.MapRazorPages(); // Razor page için Route mekanizması eklendi

app.Run();
