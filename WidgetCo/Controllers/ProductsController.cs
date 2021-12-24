using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Models;
using WidgetCo.Services;

namespace WidgetAndCo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductService productService, IConfiguration configuration)
        {
            _configuration = configuration;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product> result = await _productService.GetEntities();
            return result.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productService.GetEntity(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            await _productService.UpdateEntity(product);

            return NoContent();
        }

        [HttpPost("{id}/AddImage")]
        public async Task<IActionResult> AddImage(string id, IFormFile file)
        {
            var product = await _productService.GetEntity(id);
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
                                                    _configuration["AccountName"] +
                                                    ".blob.core.windows.net/" +
                                                    "productImages" +
                                                    "/" + file.Name);

                            StorageSharedKeyCredential storageCredentials =
                                new StorageSharedKeyCredential(_configuration["AccountName"], _configuration["AccountKey"]);

                            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

                            await blobClient.UploadAsync(stream);
                            product.Image = blobUri.ToString();
                        }
                        await _productService.UpdateEntity(product);
                    }
                }
                else
                {
                    return new UnsupportedMediaTypeResult();
                }

                return new AcceptedResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            return await _productService.CreateEntity(product);
        }

        public async Task<ActionResult> AddReview(Product product, string review)
        {
            product.Reviews.Add(new Review { Text = review});
            await _productService.UpdateEntity(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteEntity(id);
            return NoContent();
        }

    }
}
