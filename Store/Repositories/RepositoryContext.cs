using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.Config;
using System.Reflection;

namespace Repositories.Models;

public class RepositoryContext : DbContext
{
	public RepositoryContext(DbContextOptions<RepositoryContext> options)
		: base(options)
	{
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Order> Orders { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// * Model inşa edilirken model üzerinde veri yoksa aşağıdaki verileri ekler,
		// eğer veri varsa eklemez.
		// * Bu yazıldıktan sonra tekrar migration atılması gerekir.

		// modelBuilder.Entity<Product>()
		// 	.HasData(
		// 		- Eklenmesi istenilen veriler buraya yazılıyordu.
		//		- Config klasörü altında artık her bir model için
		// 		ilgili configuration dosyaları tanımlanmış bulunmaktadır.
		//		- Burada ise o ilgili config dosyalarına referans verilmektedir.
		// 	);

		// I. Config dosylarını ekleme yöntemi :
		// İlgili config dosyaları tek tek yazılmadılır.
		// modelBuilder.ApplyConfiguration(new ProductConfig());
		// modelBuilder.ApplyConfiguration(new CategoryConfig());

		// II. Config dosylarını ekleme yöntemi :
		// EF Core, bu projedeki bütün ayar dosyalarını kendisi bulup uygular
		// Ef Core, Config Interface'sini kullanan sınıfları bulup ekler
		modelBuilder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly()
		);
	}
}