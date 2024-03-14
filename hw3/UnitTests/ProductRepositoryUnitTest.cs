using Moq;
using hw2.Models;
using hw2.Repositories;

namespace hw3.UnitTests;

public class ProductRepositoryUnitTest: IClassFixture<ProductRepository>
{
    private readonly ProductRepository _repository;

    public ProductRepositoryUnitTest(ProductRepository repository)
    {
        _repository = repository;

        var product = new Product()
        {
            ProductId = 1,
            Name = "TestName",
            Price = 1.01m,
            Weight = 0.1,
            TypeProduct = TypeProduct.FOOD,
            DateCreation = new DateTime(2001, 3, 30),
            WarehouseNumber = 1,
        };

        _repository.Insert(product);
    }
    
    [Fact]
    public void InsertTest()
    {
        var product = new Product()
        {
            ProductId = 2,
            Name = "TestName2",
            Price = 1.02m,
            Weight = 0.2,
            TypeProduct = TypeProduct.FOOD,
            DateCreation = new DateTime(2002, 3, 30),
            WarehouseNumber = 2,
        };
        var repository = new ProductRepository();
        repository.Insert(product);
        var response  = repository.GetList();
        Assert.NotNull(response);
        Assert.True(response.Exists(item => item.ProductId == product.ProductId),
            "Результат не содержит добавленного продукта");
    }

    [Fact]
    public void GetListTestCount()
    {
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(productRepository => productRepository.GetList())
            .Returns(
                new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "TestName",
                        Price = 1.01m,
                        Weight = 0.1,
                        TypeProduct = TypeProduct.FOOD,
                        DateCreation = new DateTime(2001, 3, 30),
                        WarehouseNumber = 1,
                    }
                }
            );
        var repository = repositoryMock.Object;
        
        //var response  = _repository.GetList();
        var response  = repository.GetList();
        
        Assert.NotNull(response);
        Assert.True(response.Count == 1, "Колличество складов не равно 1");
    }
    
    [Fact]
    public void GetListTest()
    {
        var response  = _repository.GetList();
        Assert.NotNull(response);
        Assert.True(response.Count > 0, "Колличество складов не больше 0");
    }
    
    // [Fact]
    // public void GetListTestWithFilterCount()
    // {
    //     var response1  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.FOOD,
    //         DateCreation = new DateTime(2001, 3, 29),
    //         WarehouseNumber = 1,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     var response2  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.FOOD,
    //         DateCreation = new DateTime(2001, 3, 29),
    //         WarehouseNumber = 2,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     var response3  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.FOOD,
    //         DateCreation = new DateTime(2001, 3, 30),
    //         WarehouseNumber = 1,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     var response4  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.COMMON,
    //         DateCreation = new DateTime(2001, 3, 29),
    //         WarehouseNumber = 1,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     Assert.NotNull(response1);
    //     Assert.Empty(response2);
    //     Assert.Empty(response3);
    //     Assert.Empty(response4);
    //     Assert.True(response1.Count == 1, "Колличество складов не равно 1");
    // }
    //
    // [Fact]
    // public void GetListTestWithFilter()
    // {
    //     var response1  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.FOOD,
    //         DateCreation = new DateTime(2001, 3, 29),
    //         WarehouseNumber = 1,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     var response2  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.FOOD,
    //         DateCreation = new DateTime(2001, 3, 29),
    //         WarehouseNumber = 2,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     var response3  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.FOOD,
    //         DateCreation = new DateTime(2001, 3, 30),
    //         WarehouseNumber = 1,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     var response4  = _repository.GetListWithFilter(new Filter()
    //     {
    //         TypeProduct = TypeProduct.COMMON,
    //         DateCreation = new DateTime(2001, 3, 29),
    //         WarehouseNumber = 1,
    //         PageNumber = 1,
    //         PageSize = 0,
    //     });
    //     
    //     Assert.NotNull(response1);
    //     Assert.Empty(response2);
    //     Assert.Empty(response3);
    //     Assert.Empty(response4);
    //     Assert.True(response1.Count > 0, "Колличество складов не больше 0");
    // }
    
    [Fact]
    public void UpdateTest()
    {
        var product = _repository.GetById(1);
        product.Name = "new name";
        _repository.Update(product);
        var newProduct = _repository.GetById(1);
        Assert.True(newProduct.Name == "new name", "Имя продукта не изменилось");
    }    
    
    [Fact]
    public void Update1Test()
    {
        var product = new Product
        {
            ProductId = 2,
            Name = "TestName",
            Price = 1.01m,
            Weight = 0.1,
            TypeProduct = TypeProduct.FOOD,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            WarehouseNumber = 1,
        };
        var res = _repository.Update(product);
        var response  = _repository.GetList();
        
        Assert.True(response.Count == 1);
        Assert.True(!res, "Создали новый элемент");
    }
    
    [Fact]
    public void DeleteTest()
    {
        var productById1 = _repository.GetById(1);
        _repository.Delete(productById1);
        var response  = _repository.GetList();
        
        Assert.True(!response.Exists(item => item.ProductId == productById1.ProductId),
            "Результат содержит добавленный продукт");
    }
    
    [Fact]
    public void DeleteByIdTest()
    {
        var productId1 = _repository.GetById(1).ProductId;
        _repository.DeleteById(productId1);
        var response  = _repository.GetList();
        
        Assert.True(!response.Exists(item => item.ProductId == productId1),
            "Результат содержит добавленный продукт");
    }
    
    [Fact]
    public void GetByIdTest()
    {
        var repository = new ProductRepository();
        var response  = repository.GetList();
        var product1 = new Product()
        {
            ProductId = 1,
            Name = "TestName1",
            Price = 1.01m,
            Weight = 0.1,
            TypeProduct = TypeProduct.FOOD,
            DateCreation = DateTime.SpecifyKind(new DateTime(2002, 3, 30), DateTimeKind.Utc),
            WarehouseNumber = 2,
        };
        var product2 = new Product()
        {
            ProductId = 2,
            Name = "TestName2",
            Price = 1.02m,
            Weight = 0.2,
            TypeProduct = TypeProduct.COMMON,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            WarehouseNumber = 2,
        };
        repository.Insert(product1);
        repository.Insert(product2);
        
        
        Assert.True(response.All(item => 
                item.ProductId == _repository.GetById(item.ProductId).ProductId), 
            "Выдает неправильный элемент");
    }
    
    [Fact]
    public void ExistTest()
    {
        var productId1 = _repository.GetById(1).ProductId;
        var response  = _repository.GetList();
        Assert.True(response.Exists(item => item.ProductId == productId1) && _repository.Exist(productId1),
            "Нет искомого продукта");
        
        _repository.DeleteById(productId1);
        
        Assert.True(!_repository.Exist(productId1),
            "Exist работает не правильно");
    }

}