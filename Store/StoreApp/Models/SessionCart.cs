using System.Text.Json.Serialization;
using Entities.Models;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Models;


/*
    SESSION NEDİR? (Bu proje özelinde)

    - Session, aynı tarayıcıdan gelen request'leri birbirine bağlayan bir mekanizmadır.
    - Tarayıcı tarafında SADECE SessionId bulunur (cookie).
    - Asıl veriler (örneğin cart JSON'u) SUNUCU tarafında tutulur.

    Akış:
    	* Tarayıcı -> SessionId gönderir
    	* Sunucu   -> Bu SessionId'ye karşılık gelen JSON veriyi RAM'den bulur
*/


public class SessionCart : Cart
{
	/*
		Bu property, Cart nesnesinin hangi Session'a bağlı olduğunu tutar.

		NEDEN JsonIgnore?
			- Session nesnesi serialize edilemez
			- Session'ı JSON'a yazmaya çalışırsak hata alırız
			- Zaten Session kalıcı veri değil, erişim kanalıdır
	*/
	[JsonIgnore]
	public ISession? Session { get; set; }

	/*
        ================================
        FACTORY METHOD (CUSTOM DI FACTORY)
        ================================

        Bu metot şunu söyler:
        "DI Container, Cart üretirken new Cart() yapma,
         bunun yerine bu metodu çağır."

        Ne zaman çalışır?
        - Controller veya PageModel ctor'unda Cart istendiğinde

        Ne üretir?
        - Normal Cart değil
        - Session ile ilişkili SessionCart

        Ama DI dışarıya hâlâ Cart verir
        (Polymorphism)
	*/
	public static Cart GetCart(IServiceProvider serviceProvider)
	{
		ISession? session =
			serviceProvider.                            // IServiceProvider => DI Container (Tüm servisleri bilen yapı)
			GetRequiredService<IHttpContextAccessor>(). // Aktif HttpContext'e erişmek için
			HttpContext?.                               // Şuanki HTTP isteğini
			Session;                                    // Bu isteğe (taryıcıya) ait Session

		// Session içerisinde "cart" anahtarıyla daha önce kayıtlı bir sepet var mı kontrol edilir.
		SessionCart cart =
			session?.GetJson<SessionCart>("cart") // Varsa : JSON veri SessionCart tipine deserialize edilir.
			?? new SessionCart();                 // Yoksa : Yeni bir SessionCart nesnesi oluşturulur.

		// Request'ten gelen session bilgisini buradaki ISession instance'ına atıyoruz.
		cart.Session = session;

		return cart;
	}

	public override void AddItem(Product product, int quantity)
	{
		// Önce liste RAM'de güncellenir.
		base.AddItem(product, quantity);
		// Sonra session'a kaydedilir.
		Session?.SetObject<SessionCart>("cart", this);
		// Sonra her şey uçar, session'daki bilgi hariç 
		// (yani sunucu RAM'inde tutulan sessinId'in altındaki json'a çevrilmiş cart bilgileri)
	}

	public override void Clear()
	{
		base.Clear();
		Session?.Remove("cart");
	}

	public override void RemoveLine(Product product)
	{
		base.RemoveLine(product);
		Session?.SetObject<SessionCart>("cart", this);
	}

}