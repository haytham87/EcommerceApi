using AutoMapper;
using Commerceee.DTOS;
using Core.Entities;
using Core.Idenrity;

namespace Commerceee.Helper
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductToReturnDTO>()
                .ForMember(d=>d.ProductType,s=>s.MapFrom(o=>o.ProductType.TypeName))
                .ForMember(d => d.BrandType, s => s.MapFrom(o => o.BrandType.BrandName));
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
