using System.Collections.Concurrent;
using WBAPI.Api.Models;

namespace WBAPI.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<int, Product> _store = new();
        private int _seq = 0;

        public ProductRepository()
        {
            Add(new Product { Name = "Sample", Price = 10m });
        }

        public Product Add(Product p)
        {
            var id = System.Threading.Interlocked.Increment(ref _seq);
            p.Id = id;
            _store[id] = p;
            return p;
        }

        public bool Delete(int id) => _store.TryRemove(id, out _);

        public IEnumerable<Product> GetAll() => _store.Values;

        public Product? GetById(int id) => _store.TryGetValue(id, out var p) ? p : null;

        public bool Update(int id, Product p)
        {
            if (!_store.ContainsKey(id)) return false;
            p.Id = id;
            _store[id] = p;
            return true;
        }
    }
}
