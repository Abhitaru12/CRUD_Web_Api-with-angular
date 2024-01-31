using CRUD_Web_Api.Dto;
using CRUD_Web_Api.Models;
using CRUD_Web_Api.Repository;
using CRUD_Web_Api.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUD_Web_Api.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Return custom message from the NotFoundException
            }
        }

        [HttpGet]
        public async Task<IActionResult> getallproducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            bool doesExist = await _productRepository.DoesProductExist(product.ProductName);

            if (doesExist)
            {
                return Conflict("Product already exists!"); // Return 409 Conflict status code
            }

            await _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                await _productRepository.Update(product);
                await _productRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool ProductAvailable(int id)
        {
            var existingProduct = _productRepository.GetById(id).Result;
            return existingProduct != null;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetById(id)
        ;
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.Delete(id)
        ;
            await _productRepository.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("CheckProductName")]
        public async Task<IActionResult> CheckProductName([FromBody] string productName)
        {
            bool exists = await _productRepository.DoesProductExist(productName);
            if (exists)
            {
                return Conflict("Product already exists!"); // Return 409 Conflict status code
            }

            return Ok(); // Return 200 OK if the product name doesn't exist
        }



        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        //{
        //    if (_context == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Products.ToListAsync();
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> getproduct(int id)
        //{
        //    if (_context == null)
        //    {
        //        return NotFound();
        //    }
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return product;
        //}

        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //    _context.Products.Add(product); 
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        //}

        //[HttpPut]
        //public async Task<IActionResult> PutProduct(int id , Product product)
        //{
        //    if(id != product.Id) 
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(product).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException ) 
        //    {
        //        if (!ProductAvailable(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return Ok();
        //}
        //private bool ProductAvailable (int id) 
        //{
        //    return (_context.Products?.Any(p => p.Id == id)).GetValueOrDefault();
        //}

        //[HttpDelete]
        //public async Task<ActionResult> DeleteProduct(int id)
        //{
        //    if(_context.Products == null)
        //    {
        //        return NotFound();
        //    }
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Products.Remove(product);

        //     await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}

