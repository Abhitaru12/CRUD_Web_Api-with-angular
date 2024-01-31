using AutoMapper;
using CRUD_Web_Api.Dto;
using CRUD_Web_Api.Models;
using CRUD_Web_Api.Repository;

namespace CRUD_Web_Api.Service
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task AddProduct(ProductDto productdto)
        {
            var product = _mapper.Map<Product>(productdto);
            await _productRepository.Add(product);
        }

        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetById(id);
            if (existingProduct == null)
            {
                // Handle not found scenario or return null/error
                return;
            }

            _mapper.Map(productDto, existingProduct);

            await _productRepository.Update(existingProduct);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            return _mapper.Map<ProductDto>(product);
        }

        Task IProductService.DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
        //private readonly IProductRepository _productRepository;
        //private readonly IMapper _mapper;

        //public ProductService(IProductRepository productRepository, IMapper mapper)
        //{
        //    _productRepository = productRepository;
        //    _mapper = mapper;
        //}

        //public async Task<IEnumerable<ProductDto>> GetAllProducts()
        //{
        //    var products = await _productRepository.GetAll();
        //    return _mapper.Map<IEnumerable<ProductDto>>(products);
        //}

        //public async Task AddProduct(ProductDto productdto)
        //{
        //    var product = _mapper.Map<Product>(productdto);
        //    await _productRepository.Add(product);
        //}

        //public async Task UpdateProduct(int id, ProductDto productDto)
        //{
        //    var existingProduct = await _productRepository.GetById(id);
        //    if (existingProduct == null)
        //    {
        //        // Handle not found scenario or return null/error
        //        return;
        //    }

        //    _mapper.Map(productDto, existingProduct);

        //    await _productRepository.Update(existingProduct);
        //}

        //public async Task<ProductDto> GetProductById(int id)
        //{
        //    var product = await _productRepository.GetById(id);
        //    return _mapper.Map<ProductDto>(product);
        //}

        // public async Task DeleteProduct(int id) => await _productRepository.Delete(id);
    }
}
