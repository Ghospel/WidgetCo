using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WidgetAndCo.DAL;
using WidgetAndCo.Models;

namespace WidgetAndCo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IOptions<StorageConfig> _storageConfig;

        public ProductsController(ProductContext context, IOptions<StorageConfig> storageConfig)
        {
            _storageConfig = storageConfig;
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("{id}/AddImage")]
        public async Task<IActionResult> AddImage(string id, IFormFile file)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                if (file.ContentType.Contains("image"))
                {
                    if (file.Length > 0)
                    {
                        using (Stream stream = file.OpenReadStream())
                        {

                            Uri blobUri = new Uri("https://" +
                                                    _storageConfig.Value.AccountName +
                                                    ".blob.core.windows.net/" +
                                                    "productImages" +
                                                    "/" + file.Name);

                            StorageSharedKeyCredential storageCredentials =
                                new StorageSharedKeyCredential(_storageConfig.Value.AccountName, _storageConfig.Value.AccountKey);

                            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

                            await blobClient.UploadAsync(stream);
                            product.Image = blobUri.ToString();
                        }
                    }
                }
                else
                {
                    return new UnsupportedMediaTypeResult();
                }

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.id == id);
        }
    }
}
