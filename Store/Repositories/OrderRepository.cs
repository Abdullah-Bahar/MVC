using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Models;

namespace Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
	public OrderRepository(RepositoryContext context) : base(context)
	{
	}

	/*
		VARSAYIM :
			- C# nesnesinde property varsa, EF Core onu zaten doldurur.
			> YANLIŞ

		Order nesnei 
			- Çalıştığı zaman Lines tutabilir.
			- Ama bu, veritabanından otomatik yükleneceği anlamına gelmez.
			- Eğer gelmesi istenirse EFCore'a belirtilmeli.
			- Burada da devreye "Include()" gelir.
			- Include içerisinde belirtilen tablo Join edilir.
	*/

	public IQueryable<Order> Orders => _context.Orders
		.Include(o => o.Lines)			// Orders -> CartLines için JOIN üretir
		.ThenInclude(cl => cl.Product) 	// CartLines -> Products için JOIN üretir
		.OrderBy(o => o.Shipped)	// Sipariş durumuna göre sıralar (gönderilmemişler üstte, gönderilenler altta)
		.ThenByDescending(o => o.OrderId);	// OrderId'lere göre azalan biçimde sıralar

	public int NumberOfInProcess => _context.Orders
		.Count(o => o.Shipped.Equals(false));

	public void Complete(int id)
	{
		var order = FindByCondition(o => o.OrderId == id, true);

		if (order is null)
			throw new Exception("Order could not found");

		order.Shipped = true;
	}

	public Order? GetOneOrder(int id)
	{
		return FindByCondition(o => o.OrderId.Equals(id), false);
	}

	public void SaveOrder(Order order)
	{
		/*
			Attach
				- Bu nesne veritabanında ZATEN VAR,
				sadece beni takip et, SAKIN INSERT ATMA.

			Aşağıdaki Senaryo :
				- Product DB'de zaten var.
				- Sadece order'a bağlanıyor
				- Product nesnelerinin state’lerini Unchanged yapar ki INSERT gerçekleşmesin

			Peki Attach ile FK mı set ediliyor ?
				- Attach sadece state ayarlar.
				- FK'lar Navigation üzerinden EF Core tarafından otomatik olarak set edilir.
		*/
		_context.AttachRange(order.Lines.Select(l => l.Product));

		// DB'ye eklenmemiş bir order'ın ID'si 0'dır. O zaman ekleyelim onu
		if (order.OrderId == 0)
			_context.Orders.Add(order);

		// Peki ID'si 0 olmayan orderlar ne yapacak ?
		// Onlar için de update söz konusu ama şimdilik burada yok.
	}
}