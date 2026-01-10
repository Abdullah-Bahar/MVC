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
				ImageUrl ="/image/1.jpg",
				Price = 17_000,
				CategoryId = 1,
				ShowCase = false
			},
			new Product()
			{
				ProductId = 2,
				ProductName = "Keyboard",
				ImageUrl ="/image/2.jpg",
				Price = 1_000,
				CategoryId = 1,
				ShowCase = false
			},
			new Product()
			{
				ProductId = 3,
				ProductName = "Mouse",
				ImageUrl ="/image/3.jpg",
				Price = 500,
				CategoryId = 1,
				ShowCase = false
			},
			new Product()
			{
				ProductId = 4,
				ProductName = "Monitor",
				ImageUrl ="/image/4.jpg",
				Price = 10_000,
				CategoryId = 2,
				ShowCase = false
			},
			new Product()
			{
				ProductId = 5,
				ProductName = "Deck",
				ImageUrl ="/image/5.jpg",
				Price = 2_000,
				CategoryId = 2,
				ShowCase = false
			},
			new Product()
			{
				ProductId = 6,
				ProductName = "huwai",
				ImageUrl ="/image/6.jpg",
				Price = 32_000,
				CategoryId = 2,
				ShowCase = true
			},
			new Product()
			{
				ProductId = 7,
				ProductName = "Havaryu",
				ImageUrl ="/image/7.jpg",
				Price = 12_000,
				CategoryId = 1,
				ShowCase = true
			},
			new Product()
			{
				ProductId = 8,
				ProductName = "Nevaryu",
				ImageUrl ="/image/8.jpg",
				Price = 22_000,
				CategoryId = 1,
				ShowCase = true
			}
		);
	}
}