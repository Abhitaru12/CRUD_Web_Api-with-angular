using AutoMapper;
using CRUD_Web_Api.Dto;
using CRUD_Web_Api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUD_Web_Api.mapper
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            
            // Other mappings if needed
        }
    }
}
