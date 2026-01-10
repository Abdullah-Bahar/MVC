using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace StoreApp.Infrastructure.Extensions;

/*
		startup-time migration

	--------------------------------

	* app.ApplicationServices
		- Uygulamanın en üst seviye DI container’ı (Root Container)
 		- Uygulama ayağa kalkarken bir kere oluşur
 		- Application kapanana kadar yaşar

		NOT : Root container, scoped servisleri doğrudan veremez. 
			> Çünkü scoped servislerin yaşam süresi bir HTTP isteği ile sınırlıdır. 
			> Root container ise uygulama kapanana kadar yaşar.
*/

public static class ApplicationExtension
{
	public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
	{
		RepositoryContext context = app
			.ApplicationServices 	// Uygulamanın Root DI Container'ı
			.CreateScope()			// Scoped bir servisi kullanabilmek için manuel scope açmak gerekir
			.ServiceProvider		// Açılan scope'un DI Konteynırı
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
}