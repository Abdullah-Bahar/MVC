using Microsoft.EntityFrameworkCore;

namespace StoreApp.Models;

public class RepositoryContext : DbContext
{
	public RepositoryContext(DbContextOptions<RepositoryContext> options)
		: base(options)
	{

	}

	public DbSet<Product> Products { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// * Model inşa edilirken model üzerinde veri yoksa aşağıdaki verileri ekler,
		// eğer veri varsa eklemez.
		// * Bu yazıldıktan sonra tekrar migration atılması gerekir.
		modelBuilder.Entity<Product>()
			.HasData(
				new Product()
				{
					ProductId = 1,
					ProductName = "Computer",
					Price = 17_000
				},
				new Product()
				{
					ProductId = 2,
					ProductName = "Keyboard",
					Price = 1_000
				},
				new Product()
				{
					ProductId = 3,
					ProductName = "Mouse",
					Price = 500
				},
				new Product()
				{
					ProductId = 4,
					ProductName = "Monitor",
					Price = 10_000
				},
				new Product()
				{
					ProductId = 5,
					ProductName = "Deck",
					Price = 2_000
				}
			);
	}
}