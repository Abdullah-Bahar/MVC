namespace Presentation;

public class AssemblyReference
{
	/*
		- Bu sınıf "marker (işaretçi)" olarak kullanılır.
		- Amacı bu assembly’ye type-safe (güvenli) şekilde erişebilmektir: typeof(AssemblyReference).Assembly
		- Genellikle DI (dependency injection), controller scan, AutoMapper, MediatR gibi işlemlerde kullanılır.
		- String ile assembly yükleme yerine compile-time güvenliği sağlar ve refactor işlemlerinde bozulmaz.
	*/

}
