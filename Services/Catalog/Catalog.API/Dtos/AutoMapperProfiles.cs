using AutoMapper;
using Catalog.API.Models;

namespace Catalog.API.Dtos
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, AddProductDto>().ReverseMap();
        }
    }
}
