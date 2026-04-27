using StoreApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers(); // => API ile çalışılacaksa bu kullanılır

// Servis kaydı yapılıyor
// Servis kayıtları Middleware yapılarıyla birlikte kullanılabilir
builder.Services.AddControllersWithViews(); // Controller + View => Servis Kaydı

builder.Services.AddRazorPages(); // Uygulamaya Razor Page'ler de eklendi.

// DbContext Extentsion metodu ile kaydedildi
builder.Services.ConfigureDbContext(builder.Configuration);

// Identity Extension metodu ile kaydedildi
builder.Services.ConfigureIdentity();

// Session Extension metodu ile kaydedildi
builder.Services.ConfigureSession();

// Repository Extension metodu ile kaydedildi
builder.Services.ConfigureRepositoryRegistration();

// Service Extension metodu ile kaydedildi
builder.Services.ConfigureServiceRegistration();

// Routing Extension metodu ile kaydedildi
builder.Services.ConfigureRouting();

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

// Aşağıdaki kullanım .net6 öncesi için. Hala çalışır ama önerilmez.
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

// Uygulama başlatılırken migration'lar kontrol edilsin ve varsa uygulansın (Genişletilmiş method)
app.ConfigureAndCheckMigration();

// Uygulama için lokalizasyon ayarları yapılır - Dil ayarları (Genişletilmiş method)
app.ConfigureLocalization();

// Uygulama başlatılırken default admin kullanıcısı oluşturulsun (Genişletilmiş method)
app.ConfigureDefaultAdminUser();

app.Run();
