using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using hw2;
using hw2.Models;
using hw3.TestHelpers;
using TypeProduct = hw2.Models.TypeProduct;

namespace hw3.UnitTests;

public class ProductMappingUnitTest
{
    [Fact]
    public void ProductMappingUnitTestMap()
    {
        var mapper = MapperHelper.GetMapper();

        var productGrpc = new ProductModel()
        {
            ProductId = 1,
            ProductName = "name1",
            ProductPrice = 0.1,
            ProductWeight = 0.1,
            ProductType = hw2.TypeProduct.Food,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
            WarehouseNumber = 1,
        };
        var product = mapper.Map<Product>(productGrpc);
        
        product.Should().NotBeNull();
        product.ProductId.Should().Be(1);
        product.Name.Should().Be("name1");
        product.Price.Should().Be(0.1m);
        product.Weight.Should().Be(0.1);
        product.TypeProduct.Should().Be(TypeProduct.FOOD);
        product.DateCreation.Should().Be(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc));
        product.WarehouseNumber.Should().Be(1);
    }

    [Fact]
    public void ProductMappingUnitTestMapReverse()
    {
        var mapper = MapperHelper.GetMapper();

        var product = new Product()
        {
            ProductId = 1,
            Name = "name1",
            Price = 0.1m,
            Weight = 0.1,
            TypeProduct = TypeProduct.FOOD,
            DateCreation = DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc),
            WarehouseNumber = 1,
        };
        var productGrpc = mapper.Map<ProductModel>(product);
        
        productGrpc.Should().NotBeNull();
        productGrpc.ProductId.Should().Be(1);
        productGrpc.ProductName.Should().Be("name1");
        productGrpc.ProductPrice.Should().Be(0.1);
        productGrpc.ProductWeight.Should().Be(0.1);
        productGrpc.ProductType.Should().Be(hw2.TypeProduct.Food);
        productGrpc.DateCreation.Should().Be(Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)));
        productGrpc.WarehouseNumber.Should().Be(1);
    }
    
    [Fact]
    public void ProductMappingUnitTestMapNull()
    {
        var mapper = MapperHelper.GetMapper();

        var product = mapper.Map<Product>(null);
        product.Should().BeNull();
    }
    
    [Fact]
    public void ProductMappingUnitTestMapNullRevers()
    {
        var mapper = MapperHelper.GetMapper();

        var productGrpc = mapper.Map<ProductModel>(null);
        productGrpc.Should().BeNull();
    }
}