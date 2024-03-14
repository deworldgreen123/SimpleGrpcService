using hw2.Models;

namespace hw2.Services;

public interface IProductService
{
    bool CreateProduct(Product product);
    List<Product> GetProductListWithFilter(Filter filter);
    Product GetProductById(long productId);
    bool UpdatePriceProduct(long productId, decimal newPrice);
}