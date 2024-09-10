using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.Models;
using backend.Dtos;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PizzaDbContext _context;

        public ProductsController(PizzaDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                // Consulta a la base de datos con el modelo Product
                var products = await _context.Products.ToListAsync();

                if (products == null || !products.Any())
                {
                    return NotFound(new { status = false, message = "No products found" });
                }

                // Mapea los productos a ProductDto antes de devolverlos
                var productDtos = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    IsAvailable = p.IsAvailable,
                    CategoryId = p.CategoryId,
                    Image = p.Image
                }).ToList();

                return Ok(new { status = true, data = productDtos });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                return StatusCode(500, new { status = false, message = "An error occurred while fetching products.", error = ex.Message });
            }
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                // Consulta a la base de datos para encontrar el producto por su ID
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new { status = false, message = "Product not found" });
                }

                // Mapea el producto a ProductDto antes de devolverlo
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    IsAvailable = product.IsAvailable,
                    CategoryId = product.CategoryId,
                    Image = product.Image
                };

                return Ok(new { status = true, data = productDto });
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción inesperada
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request.", error = ex.Message });
            }
        }


        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest(new { status = false, message = "Product ID mismatch" });
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { status = false, message = "Product not found" });
            }

            // Mapea ProductDto a Product antes de modificar la entidad en la base de datos
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.IsAvailable = productDto.IsAvailable;
            product.CategoryId = productDto.CategoryId;
            product.Image = productDto.Image;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { status = true, message = "Product updated successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound(new { status = false, message = "Product no longer exists" });
                }
                else
                {
                    return StatusCode(500, new { status = false, message = "Concurrency error occurred while updating the product" });
                }
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción inesperada
                return StatusCode(500, new { status = false, message = "An error occurred while updating the product", error = ex.Message });
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productDto)
        {
            try
            {
                // Mapea ProductDto a Product antes de agregar la entidad a la base de datos
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    IsAvailable = productDto.IsAvailable,
                    CategoryId = productDto.CategoryId,
                    Image = productDto.Image
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.Id }, new { status = true, message = "Product created successfully", data = product });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { status = false, message = "An error occurred while creating the product", error = ex.Message });
            }
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(new { status = false, message = "Product not found" });
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new { status = true, message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { status = false, message = "An error occurred while deleting the product", error = ex.Message });
            }
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
