using Entities.Models;

namespace Repositories.Contracts;

public interface IOrderRepository : IRepositoryBase<Order>
{
	IQueryable<Order> Orders { get; }
	Order? GetOneOrder(int id);
	void Complete(int id); // sipariş kargoya verildi
	void SaveOrder(Order order); // yeni sipariş oluşturma
	int NumberOfInProcess { get; } // İşlemde olan sipariş sayısı
}