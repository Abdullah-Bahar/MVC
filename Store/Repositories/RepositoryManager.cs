using Repositories.Contracts;
using Repositories.Models;

namespace Repositories;

public class RepositoryManager : IRepositoryManager
{
	private readonly RepositoryContext _context;
	private readonly IProductRepository _productRepository;

	public RepositoryManager(IProductRepository productRepository, RepositoryContext context)
	{
		_context = context;
		_productRepository = productRepository;
	}

	public IProductRepository Product => _productRepository;

	public void Save()
	{
		_context.SaveChanges();

		/* 
			* SaveChange()
		 		- DbContext’in memory (Change Tracker) içinde tuttuğu değişiklikleri 
				veritabanına fiziksel olarak yazan metottur.
				- Add, Update gibi methodlar veritabanına yazmaz sadece işlem yapılacak diye
				işaretler.

			* Transaction
				- Ya hep, ya hiç
				- Bir grup işlemin tamamının başarılı olması ya da tek birinin bile hata alması halinde 
				hepsinin geri alınmasıdır.
			
			* EF Core’da SaveChanges() default olarak transaction kullanır.
			* EF Core'da default yerine manuel bir şekilde de Transaction de kullanılabilir.
			* Dolayısıyla SaveChanges() işlemi sırasında birşeyler patlarsa arka plandaki ROLLBACK
			çalışarak tüm değişşiklikleri geri alır.

			* Change Tracker
				- Change Tracker, Entity Framework Core’un DbContext içinde tuttuğu ve 
				entity’lerin durumlarını izleyen mekanizmadır.
				- Change Tracker, EntityState durumuyle veritabanındaki nesneleri takip ederek
				ilgili verilerin değişip değişmediği vb. durumlarını işaretler.
				- SaveChanges(), Change Tracker’a bakar ve sadece değişen entity’ler için SQL üretir.
		*/
	}
}