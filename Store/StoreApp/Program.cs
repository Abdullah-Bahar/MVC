using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers(); // => API ile çalışılacaksa bu kullanılır

// Servis kaydı yapılıyor
// Servis kayıtları Middleware yapılarıyla birlikte kullanılabilir
builder.Services.AddControllersWithViews(); // Controller + View => Servis Kaydı

// DbContext'in servis kaydı yapıldı
builder.Services.AddDbContext<RepositoryContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"));
});

var app = builder.Build();

// Uygulama static dosyalarda kullanacak. (wwwroot altındakiler)
app.UseStaticFiles();

// Rederiction mekanizması eklendi
app.UseHttpsRedirection();

// MapControllerRoute() ile tanımlanan routing işlemlerinin dikkate alınmasını sağlar
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
