using WBAPI.Api.Models;

namespace WBAPI.Api.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product Add(Product p);
        bool Update(int id, Product p);
        bool Delete(int id);
    }
}
