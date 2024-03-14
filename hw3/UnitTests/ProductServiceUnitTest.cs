using hw2.Models;
using hw2.Repositories;
using hw2.Services;
using Moq;

namespace hw3.UnitTests;

public class ProductServiceUnitTest
{
    [Fact]
    public void CreateProductTest()
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };

        var repository = new ProductRepository();
        var service = new ProductService(repository);
        service.CreateProduct(product);
        Assert.Equal(service.GetProductById(product.ProductId), product);
    }
    
    [Fact]
    public void FailCreateProductTest()
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };

        var repository = new ProductRepository();
        var service = new ProductService(repository);
        service.CreateProduct(product);
        Assert.True(!service.CreateProduct(product));
        Assert.True(repository.GetList().Count == 1);
    }
    
    [Theory]
    [MemberData("GetProductListWithFilterTestData")]
    public void GetProductListWithFilterTest(Filter filter, bool res)
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };

        var repository = new ProductRepository();
        var service = new ProductService(repository);
        service.CreateProduct(product);

        var listProducts = service.GetProductListWithFilter(filter);
        if (res)
        {
            Assert.Equal(listProducts[0], product);
            Assert.True(listProducts.Count == 1);
        }
        else
        {
            Assert.Empty(listProducts);
            Assert.True(listProducts.Count == 0);
        }
    }
    
    [Fact]
    public void GetProductByIdTest()
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };
        
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(productRepository => productRepository.GetById(product.ProductId))
            .Returns(product);
        var repository = repositoryMock.Object;

        var service = new ProductService(repository);
        service.CreateProduct(product);
        Assert.Equal(service.GetProductById(product.ProductId), product);
        Assert.Null(service.GetProductById(product.ProductId + 1));
    }
    
    [Fact]
    public void ExceptionGetProductByIdTest()
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };
        var repository = new ProductRepository();
        var service = new ProductService(repository);
        Assert.Throws<ArgumentException>(() =>
        {
            service.GetProductById(product.ProductId);
        });
    }
    
    [Fact]
    public void UpdatePriceTest()
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };
        var newPrice = 2m;

        var repository = new ProductRepository();
        var service = new ProductService(repository);
        service.CreateProduct(product);
        service.UpdatePriceProduct(product.ProductId, newPrice);
        Assert.Equal(service.GetProductById(product.ProductId).Price, newPrice);
    }
    
    [Fact]
    public void ExceptionUpdatePriceTest()
    {
        var product = new Product()
        {
            ProductId = 1,
            Name = "test",
            Price = 1m,
            Weight = 1,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
            TypeProduct = TypeProduct.FOOD,
            WarehouseNumber = 1,
        };
        var newPrice = 2m;

        var repository = new ProductRepository();
        var service = new ProductService(repository);
        Assert.Throws<ArgumentException>(() =>
        {
            service.UpdatePriceProduct(product.ProductId, newPrice);
        });
    }
    
    public static IEnumerable<object[]> GetProductListWithFilterTestData()
    {
        yield return new object[] { new Filter()
            {
                TypeProduct = TypeProduct.FOOD,
                DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc),
                WarehouseNumber = 1,
                PageNumber = 1,
                PageSize = 0,
            },
            true 
        };
        yield return new object[] { new Filter()
            {
                TypeProduct = TypeProduct.COMMON,
                DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc),
                WarehouseNumber = 1,
                PageNumber = 1,
                PageSize = 0,
            },
            false 
        };
        yield return new object[] { new Filter()
            {
                TypeProduct = TypeProduct.FOOD,
                DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc),
                WarehouseNumber = 1,
                PageNumber = 1,
                PageSize = 0,
            },
            false 
        };
        yield return new object[] { new Filter()
            {
                TypeProduct = TypeProduct.FOOD,
                DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc),
                WarehouseNumber = 2,
                PageNumber = 1,
                PageSize = 0,
            },
            false 
        };
    }
}