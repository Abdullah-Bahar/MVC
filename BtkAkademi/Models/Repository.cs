using System.Collections;

/*
	Programımız In-Memory olarak çalışır
*/

namespace BtkAkademi.Models
{
	public static class Repository
	{
		// private static List<Candidate> applications = new List<Candidate>();
		private static List<Candidate> applications = new(); // Farklı bir new'leme kullanımı
		
		// Sadece "get" varmış gibi oluyor. Dışarıdan "set" edilemez
		public static IEnumerable<Candidate> Applications => applications;  

		public static void Add(Candidate candidate)
		{
			applications.Add(candidate);
		}
	}
}