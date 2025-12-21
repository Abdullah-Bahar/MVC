using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		// Fluent API (EF Core Mapping)
		builder.HasKey(p => p.ProductId);
		builder.Property(p => p.ProductName).IsRequired();
		builder.Property(p => p.Price).IsRequired();

		builder.HasData(
			new Product()
			{
				ProductId = 1,
				ProductName = "Computer",
				Price = 17_000,
				CategoryId = 1
			},
			new Product()
			{
				ProductId = 2,
				ProductName = "Keyboard",
				Price = 1_000,
				CategoryId = 1
			},
			new Product()
			{
				ProductId = 3,
				ProductName = "Mouse",
				Price = 500,
				CategoryId = 1
			},
			new Product()
			{
				ProductId = 4,
				ProductName = "Monitor",
				Price = 10_000,
				CategoryId = 2
			},
			new Product()
			{
				ProductId = 5,
				ProductName = "Deck",
				Price = 2_000,
				CategoryId = 2
			}
		);
	}
}