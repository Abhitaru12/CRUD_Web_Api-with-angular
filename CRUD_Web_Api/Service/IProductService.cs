using CRUD_Web_Api.Dto;
using CRUD_Web_Api.Models;


namespace CRUD_Web_Api.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task AddProduct(ProductDto product); // Changed method name to AddProduct
        Task UpdateProduct(int id, ProductDto product);
        Task DeleteProduct(int id);
    }
}
