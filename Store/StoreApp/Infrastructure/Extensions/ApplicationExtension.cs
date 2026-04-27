using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace StoreApp.Infrastructure.Extensions;

/*
	--------------------------------
		startup-time migration
	--------------------------------

	* app.ApplicationServices
		- Uygulamanın en üst seviye DI container’ı (Root Container)
 		- Uygulama ayağa kalkarken bir kere oluşur
 		- Application kapanana kadar yaşar

		NOT : Root container, scoped servisleri doğrudan veremez. 
			> Çünkü scoped servislerin yaşam süresi bir HTTP isteği ile sınırlıdır. 
			> Root container ise uygulama kapanana kadar yaşar.

	--------------------------------
		request localization
	--------------------------------

	* ASP.NET Core’da Localization:
		- Uygulamanın tarih, saat, sayı, para birimi gibi 
		kültüre bağlı formatlarını belirler.
		- MVC validation mesajları ve UI metinleri bu ayarlara göre şekillenir.

	* Culture (SupportedCultures)
		- Veri formatlama kültürüdür.
		- DateTime, decimal, double gibi tiplerin
		  nasıl parse edileceğini ve gösterileceğini belirler.
	
	* UI Culture (SupportedUICultures)
		- Arayüz dilidir.
		- DataAnnotations validation mesajları,
		  framework mesajları ve Resource (.resx) dosyaları bu kültüre göre çalışır.
	
	* RequestLocalization Middleware:
		- Her HTTP isteği için culture bilgisini belirler.
		- Bu middleware eklenmeden önce culture ayarı yapılamaz.
*/

public static class ApplicationExtension
{
	public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
	{
		RepositoryContext context = app
			.ApplicationServices    // Uygulamanın Root DI Container'ı
			.CreateScope()          // Scoped bir servisi kullanabilmek için manuel scope açmak gerekir
			.ServiceProvider        // Açılan scope'un DI Konteynırı
			.GetRequiredService<RepositoryContext>(); // DI Container'ından RepositoryContext istenir.

		if (context.Database.GetPendingMigrations().Any())
		{
			/*
				* Uygulama ayağa kalktığında Migrations klasöründe olup henüz database'e uygulanmammış 
				migrationlar var ise uygulansın.
				
				* Yani "dotnet ef migrations add <isim>" komutu ile oluşturulmuş ama migration dosyaları için
				"dotnet ef database update" komutu çalıştırılmamış ise, uygulama ayağa kalkarken bu migrationlar
				otomatik olarak database'e uygulanacak.

				* GetPendingMigrations()
					=> Henüz database'e uygulanmamış migration'ları listeler.

				* Migrate()
					=> Tüm bekleyen migration'ları database'e uygular.
			*/
			context.Database.Migrate();
		}
	}

	public static void ConfigureLocalization(this WebApplication app)
	{
		app.UseRequestLocalization(options =>
		{
			// Yerel uygulama olduğu için bir tane dil eklendi.
			// İstenildiği takdirde pek çok dil eklenebilir.

			options.AddSupportedCultures("tr-TR")       // Veri formatlama culture
				.AddSupportedUICultures("tr-TR")     // UI culture
				.SetDefaultCulture("tr-TR");         // Default cultureF
		});
	}

	public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
	{
		// Uygulama ayağa kalkarken default admin kullanıcısı oluşturulsun.

		const string adminUser = "admin";
		const string adminPassword = "admin+123456";

		// Usermanager ve RoleManager servislerini kullanabilmek için manuel scope açılır.
		UserManager<IdentityUser> userManager = app
			.ApplicationServices
			.CreateScope()
			.ServiceProvider
			.GetRequiredService<UserManager<IdentityUser>>();

		RoleManager<IdentityRole> roleManager = app
			.ApplicationServices
			.CreateScope()
			.ServiceProvider
			.GetRequiredService<RoleManager<IdentityRole>>();

		IdentityUser user = await userManager.FindByNameAsync(adminUser);

		if (user == null)
		{
			user = new IdentityUser()
			{
				Email = "admin@example.com",
				UserName = adminUser,
				PhoneNumber = "0123456789",
				// Diğer özellikler istenirse eklenebilir
			};

			var result = await userManager.CreateAsync(user, adminPassword);

			if (!result.Succeeded)
			{
				throw new Exception("Admin kullanıcısı oluşturulurken bir hata oluştu. HATA!");
			}

			var roleResult = await userManager.AddToRolesAsync(user,
				roleManager
					.Roles
					.Select(r => r.Name)
					.ToList()
			);

			if (!roleResult.Succeeded)
			{
				throw new Exception("Admin kullanıcısına roller atanırken bir hata oluştu. HATA!");
			}
		}

	}
}