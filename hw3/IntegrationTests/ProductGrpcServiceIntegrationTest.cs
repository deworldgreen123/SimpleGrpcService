using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using hw2;
using hw2.Models;
using hw2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using ProductService = hw2.ProductService;

namespace hw3.IntegrationTests;

public class ProductGrpcServiceIntegrationTest:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ProductService.ProductServiceClient _grpcClient;

    public ProductGrpcServiceIntegrationTest(WebApplicationFactory<Program> factory)
    {
        var clientWebApp = factory.CreateClient();
        
        var channel = GrpcChannel.ForAddress(clientWebApp.BaseAddress, new GrpcChannelOptions()
        {
            HttpClient = clientWebApp
        });
        _grpcClient = new ProductService.ProductServiceClient(channel);

        for (var i = 1; i < 4; i++)
        {
            _grpcClient.CreateProduct(new CreateProductRequest()
            {
                Product = new ProductModel()
                {
                    ProductId = i,
                    ProductName = "name" + i,
                    ProductPrice = i,
                    ProductWeight = i,
                    ProductType = hw2.TypeProduct.Food,
                    DateCreation =
                        Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc)),
                    WarehouseNumber = 1,
                }
            });
        }
    }

    [Fact]
    public void IntegrationTestCreateProduct()
    {
        var response = _grpcClient.CreateProduct(new CreateProductRequest()
        {
            Product = new ProductModel()
            {
                ProductId = 5,
                ProductName = "name1",
                ProductPrice = 0.1,
                ProductWeight = 0.1,
                ProductType = hw2.TypeProduct.Food,
                DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
                WarehouseNumber = 1,
            }
        });
        
        response.Should().NotBeNull();
        response.Result.Should().BeTrue();
    }

    [Fact]
    public void IntegrationTestFailCreateProduct()
    {
        var product = new ProductModel()
        {
            ProductId = 6,
            ProductName = "name1",
            ProductPrice = 0.1,
            ProductWeight = 0.1,
            ProductType = hw2.TypeProduct.Food,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
            WarehouseNumber = 1,
        };
        _grpcClient.CreateProduct(new CreateProductRequest()
        {
            Product = product
        });
        var response = _grpcClient.CreateProduct(new CreateProductRequest()
        {
            Product = product
        });
        
        response.Should().NotBeNull();
        response.Result.Should().BeFalse();
    }

    [Fact]
    public void IntegrationTestCreateNullProduct()
    {
        ProductModel product = null;
        
        Assert.Throws<RpcException>(() =>
        {
            _grpcClient.CreateProduct(new CreateProductRequest()
            {
                Product = product
            });
        });
    }
    
    [Fact]
    public void IntegrationTestPageSize()
    {
        for (var i = 1; i < 4; i++)
        {
            var response = _grpcClient.GetProductList(new GetProductListRequest()
            {
                Filter = new FilterModel()
                {
                    ProductType = hw2.TypeProduct.Food,
                    DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
                    WarehouseNumber = 1,
                    PageNumber = 1,
                    PageSize = i,
                }
            });

            response.Should().NotBeNull();
            Assert.Equal(response.Products.Count, i);
            for (var j = 1; j <= i; j++)
            {
                Assert.Equal(response.Products[j - 1].ProductId, j);
            }
        }
    }
    
    [Fact]
    public void IntegrationTestPageNumber()
    {
        for (var i = 1; i < 4; i++)
        {
            var response = _grpcClient.GetProductList(new GetProductListRequest()
            {
                Filter = new FilterModel()
                {
                    ProductType = hw2.TypeProduct.Food,
                    DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
                    WarehouseNumber = 1,
                    PageNumber = i,
                    PageSize = 1,
                }
            });

            response.Should().NotBeNull();
            Assert.Equal(response.Products[0].ProductId, i);
        }
    }
    
    [Theory]
    [MemberData("IntegrationTestGetProductListWithFilterData")]
    public void IntegrationTestGetProductListWithFilter(FilterModel filter, bool res)
    {
        var response = _grpcClient.GetProductList(new GetProductListRequest()
        {
            Filter = filter
        });
        if (res)
        {
            Assert.Equal(response.Products[0], _grpcClient.GetProductById(new GetProductByIdRequest() {Id = 1}).Product);
            Assert.True(response.Products.Count > 0);
        }
        else
        {
            Assert.Empty(response.Products);
            Assert.True(response.Products.Count == 0);
        }
    }

    public static IEnumerable<object[]> IntegrationTestGetProductListWithFilterData()
    {
        yield return new object[] { new FilterModel()
            {
                ProductType = hw2.TypeProduct.Food,
                DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
                WarehouseNumber = 1,
                PageNumber = 1,
                PageSize = 0,
            },
            true 
        };
        yield return new object[] { new FilterModel()
            {
                ProductType = hw2.TypeProduct.Common,
                DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
                WarehouseNumber = 1,
                PageNumber = 1,
                PageSize = 0,
            },
            false 
        };
        yield return new object[] { new FilterModel()
            {
                ProductType = hw2.TypeProduct.Food,
                DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc)),
                WarehouseNumber = 1,
                PageNumber = 1,
                PageSize = 0,
            },
            false 
        };
        yield return new object[] { new FilterModel()
            {
                ProductType = hw2.TypeProduct.Food,
                DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
                WarehouseNumber = 2,
                PageNumber = 1,
                PageSize = 0,
            },
            false 
        };
    }
    
    [Fact]
    public void IntegrationTestGetProductById()
    {
        var product = new ProductModel()
        {
            ProductId = 7,
            ProductName = "name7",
            ProductPrice = 7,
            ProductWeight = 7,
            ProductType = hw2.TypeProduct.Appliances,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 7), DateTimeKind.Utc)),
            WarehouseNumber = 7,
        };

        _grpcClient.CreateProduct(new CreateProductRequest() { Product = product });
        
        var response = _grpcClient.GetProductById(new GetProductByIdRequest()
        {
            Id = product.ProductId,
        });

        response.Should().NotBeNull();
        Assert.Equal(response.Product, product);
    }
    
    [Fact]
    public void IntegrationTestGetProductByIdFalse()
    {
        Assert.Throws<RpcException>(() =>
        {
            _grpcClient.GetProductById(new GetProductByIdRequest()
            {
                Id = 10,
            });
        });
    }
    
    [Fact]
    public void IntegrationTestGetProductByIdNull()
    {
        Assert.Throws<RpcException>(() =>
        {
            _grpcClient.GetProductById(new GetProductByIdRequest());
        });
    }
    
    [Fact]
    public void IntegrationTestUpdatePrice()
    {
        var product = new ProductModel()
        {
            ProductId = 8,
            ProductName = "name8",
            ProductPrice = 8,
            ProductWeight = 8,
            ProductType = hw2.TypeProduct.Appliances,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 7), DateTimeKind.Utc)),
            WarehouseNumber = 8,
        };

        _grpcClient.CreateProduct(new CreateProductRequest() { Product = product });
        
        var response = _grpcClient.UpdateProductPrice(new UpdateProductPriceRequest()
        {
            Id = product.ProductId,
            NewPrice = product.ProductPrice*2,
        });

        response.Should().NotBeNull();
        response.Result.Should().BeTrue();
        
        Assert.Equal(
            _grpcClient.GetProductById(new GetProductByIdRequest(){Id = product.ProductId}).Product.ProductPrice,
            product.ProductPrice * 2);
    }
    
    [Fact]
    public void IntegrationTestUpdatePriceFalse()
    {
        Assert.Throws<RpcException>(() =>
        {
            _grpcClient.UpdateProductPrice(new UpdateProductPriceRequest()
            {
                Id = 9,
                NewPrice = 0,
            });
        });
    }
    
    [Fact]
    public void IntegrationTestUpdatePriceNull()
    {
        Assert.Throws<RpcException>(() =>
        {
            _grpcClient.UpdateProductPrice(new UpdateProductPriceRequest());
        });
    }
}