using hw2.Models;

namespace hw2.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Dictionary<long,Product> _productsDictionary;

    public ProductRepository()
    {
        _productsDictionary = new Dictionary<long, Product>();
    }

    public List<Product> GetList()
    {
        return _productsDictionary.Values.ToList();
    }

    public bool Insert(Product product)
    {
        return _productsDictionary.TryAdd(product.ProductId, product);
    }

    public bool Update(Product product)
    {
        _productsDictionary[product.ProductId] = product;
        return true;
    }

    public void Delete(Product product)
    {
        _productsDictionary.Remove(product.ProductId);
    }

    public void DeleteById(long id)
    {
        _productsDictionary.Remove(id);
    }

    public Product GetById(long productId)
    {
        if (!Exist(productId))
        {
            throw new ArgumentException("There is no product with this ID");
        }
        return _productsDictionary[productId];
    }

    public bool Exist(long id)
    {
        return _productsDictionary.ContainsKey(id);
    }
}