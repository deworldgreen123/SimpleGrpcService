using hw2.Models;
using hw2.Repositories;

namespace hw2.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool CreateProduct(Product product)
    {
        return _productRepository.Insert(product);
    }

    public List<Product> GetProductListWithFilter(Filter filter)
    {
        if (filter.PageSize < 0)
        {
            throw new ArgumentException("pageSize < 0");
        }
        if (filter.PageNumber <= 0)
        {
            throw new ArgumentException("pageSize <= 0");
        }
        
        var products = _productRepository.GetList();
        if (filter.PageSize == 0)
        {
            return products.Where(product =>
                product.TypeProduct == filter.TypeProduct &&
                product.DateCreation > filter.DateCreation &&
                product.WarehouseNumber == filter.WarehouseNumber).ToList();
        }

        var listProductWithFilter = products.Where(product =>
                product.TypeProduct == filter.TypeProduct &&
                product.DateCreation > filter.DateCreation &&
                product.WarehouseNumber == filter.WarehouseNumber)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        
        return listProductWithFilter;
    }

    public Product GetProductById(long productId)
    {
        return _productRepository.GetById(productId);
    }

    public bool UpdatePriceProduct(long productId, decimal newPrice)
    {
        var product = _productRepository.GetById(productId);
        product.Price = newPrice;
        return _productRepository.Update(product);
    }
}