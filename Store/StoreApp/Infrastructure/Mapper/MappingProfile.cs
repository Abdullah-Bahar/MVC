using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace StoreApp.Infrastructure.Mapper;

/*
	Profile dosyası
		- Mapping tanımlarının bulunduğu yer
		- Mapping kurallarını içerir
		- AutoMapper tarafından otomatik keşfedilir (AddAutoMapper sayesinde)
		- Entity ve DTO nesneleri arasındaki eşlemeler ctor içerisinde yapılır.
*/


public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ProductDtoForInsertion, Product>();
	}
}