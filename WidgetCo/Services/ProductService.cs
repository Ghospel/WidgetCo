using Models;
using DAL;

namespace WidgetCo.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<Product> CreateEntity(Product entity)
        {
            return Task.FromResult(_productRepository.Add(entity));
        }

        public Task<Product> DeleteEntity(string id)
        {
            var product = _productRepository.Find(id);
            return Task.FromResult(_productRepository.Delete(product));
        }

        public Task<IEnumerable<Product>> GetEntities()
        {
            return Task.FromResult(_productRepository.FetchAll().AsEnumerable());
        }

        public Task<Product> GetEntity(string id)
        {
            return Task.FromResult(_productRepository.Find(id));
        }

        public Task<Product> UpdateEntity(Product entity)
        {
            return Task.FromResult(_productRepository.Update(entity));
        }
    }
}
