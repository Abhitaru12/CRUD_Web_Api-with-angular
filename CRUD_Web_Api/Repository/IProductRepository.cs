using CRUD_Web_Api.Models;

namespace CRUD_Web_Api.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Add(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(int id);
        Task<bool> SaveChangesAsync();

        Task<bool> DoesProductExist(string product);
    }
}
