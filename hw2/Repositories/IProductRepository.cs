using hw2.Models;

namespace hw2.Repositories;

public interface IProductRepository
{
    List<Product> GetList();
    bool Insert(Product product);
    bool Update(Product product);
    void Delete(Product product);
    void DeleteById(long id);
    Product GetById(long productId);
    bool Exist(long id);
}