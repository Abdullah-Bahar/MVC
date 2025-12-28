using System.Text.Json;

namespace StoreApp.Infrastructure.Extensions;

/*
		EXTENSION METHOD NEDİR?

	* Extension method’lar, var olan bir tipi (class / interface) değiştirmeden
	  o tipe yeni metotlar eklememizi sağlar.
	* Extension method’lar mutlaka static class içinde tanımlanır.
	* Bu metotlar da static olmak zorundadır.

		NEDEN STATIC?

	* Extension method’lar derleme (compile) zamanında,
	  normal bir static metot çağrısına dönüştürülür.
	* Yani çalışma zamanında (runtime) sınıfa gerçekten bir metot eklenmez.

		THIS ANAHTAR KELİMESİ NE İŞE YARAR?

	* Metot parametresinin başındaki "this", hangi tipin genişletildiğini belirtir.
	* Aşağıdaki örnekte ISession tipi genişletilmektedir.
	* Bu sayede metot, ISession’a aitmiş gibi çağrılabilir.

	* Örnek kullanım:
		- HttpContext.Session.SetJson("cart", cart)

		DEFAULT()
	
	* “Bu tip için güvenli varsayılan değeri ver”
	* Aşağıdaki kullanım, NullReferenceException fırlatmak yerine güvenli dönüş yapmak için

	* SONUÇ :
		- Bu extension sınıfı, ISession’ı JSON serialize/deserialize yeteneği ile genişleterek complex 
		object’leri güvenli ve temiz biçimde Session’da saklamamızı sağlar.


*/
public static class SessionExtension
{
	// Serialize (Object -> JSON)
	public static void SetJson(this ISession session, string key, object value) 
	{
		session.SetString(key, JsonSerializer.Serialize(value));
	}

	// Generic Serialize 
	public static void SetObject<T>(this ISession session, string key, T value)
	{
		session.SetString(key, JsonSerializer.Serialize(value));
	}

	// Deserialize (JSON -> Default)
	public static T? GetJson<T>(this ISession session, string key)
	{
		var data = session.GetString(key);

		return data is null
			? default(T)
			: JsonSerializer.Deserialize<T>(data);
	}
}