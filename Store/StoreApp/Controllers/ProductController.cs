using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class ProductController : Controller
{
	public IEnumerable<Product> Index()
	{
		// Propgram.cs dosyasındaki DbContext için yapılan servis kaydı
		// aşağıda BAYPASS edilmiştir

		// Bu metotta DbContext Dependency Injection (DI) üzerinden ALINMIYOR
		// RepositoryContext (DbContext) manuel olarak oluşturuluyor
		var context = new RepositoryContext(

			// DbContextOptionsBuilder:
			// RepositoryContext'in hangi ayarlarla çalışacağını tanımlar
			// (hangi veritabanı, hangi provider, hangi connection string vb.)
			new DbContextOptionsBuilder<RepositoryContext>()

				// EF Core'a bu DbContext'in SQLite kullanacağını bildirir
				// Buradaki connection string SABİT (hard-coded) olarak verilmiştir
				// DI + Configuration yapısı kullanılmamaktadır
				.UseSqlite("Data Source = C:\\sqlite3\\db\\ProductDb.db")

				// Options:
				// Oluşturulan tüm ayarların RepositoryContext'e verilmesini sağlar
				.Options
		);

		// DbContext içindeki DbSet<Product>
		// Yani veritabanındaki Products tablosu
		return context.Products;
	}
}
