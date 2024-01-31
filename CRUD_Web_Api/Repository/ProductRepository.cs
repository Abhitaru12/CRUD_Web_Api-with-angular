using CRUD_Web_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Web_Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) 
            {
                //throw new Exception($"Product not found with the specified id :{id}");
                throw new Exception($"Please Enter a Valid Id: {id}");
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DoesProductExist(string productName)
        {
            return await _context.Products.AnyAsync(p => p.ProductName == productName);
        }
    }
}
