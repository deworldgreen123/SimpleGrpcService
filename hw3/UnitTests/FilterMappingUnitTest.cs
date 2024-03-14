using AutoMapper;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using hw2;
using hw2.Models;
using hw2.Profiles;
using hw3.TestHelpers;
using TypeProduct = hw2.Models.TypeProduct;


namespace hw3.UnitTests;

public class FilterMappingUnitTest
{
    [Theory]
    [MemberData("FilterMappingUnitTestMapData")]
    public void FilterMappingUnitTestMap(FilterModel filterModel)
    {
        var mapper = MapperHelper.GetMapper();

        var filter = mapper.Map<Filter>(filterModel);

        filter.Should().NotBeNull();
        filter.TypeProduct.Should().Be((TypeProduct)filterModel.ProductType);
        filter.DateCreation.Should().Be(filterModel.DateCreation.ToDateTime());
        filter.WarehouseNumber.Should().Be(filterModel.WarehouseNumber);
        filter.PageNumber.Should().Be(filterModel.PageNumber);
        filter.PageSize.Should().Be(filterModel.PageSize);
    }

    public static IEnumerable<object[]> FilterMappingUnitTestMapData()
    {
        yield return new object[] { new FilterModel()
        {
            ProductType = hw2.TypeProduct.Food,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
            WarehouseNumber = 2,
            PageNumber = 1,
            PageSize = 0,
        } };
        yield return new object[] { new FilterModel()
        {
            ProductType = hw2.TypeProduct.Food,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 30), DateTimeKind.Utc)),
            WarehouseNumber = 1,
            PageNumber = 1,
            PageSize = 0,
        } };
        yield return new object[] { new FilterModel()
        {
            ProductType = hw2.TypeProduct.Common,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
            WarehouseNumber = 1,
            PageNumber = 1,
            PageSize = 0,
        } };
        yield return new object[] { new FilterModel()
        {
            ProductType = hw2.TypeProduct.Food,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
            WarehouseNumber = 1,
            PageNumber = 2,
            PageSize = 0,
        } };
        yield return new object[] { new FilterModel()
        {
            ProductType = hw2.TypeProduct.Food,
            DateCreation = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2001, 3, 29), DateTimeKind.Utc)),
            WarehouseNumber = 1,
            PageNumber = 1,
            PageSize = 1,
        } };
    }
    
    [Fact]
    public void FilterMappingUnitTestMapNull()
    {
        var mapper = MapperHelper.GetMapper();

        var filter = mapper.Map<Filter>(null);
        filter.Should().BeNull();
    }
    
    [Fact]
    public void FilterMappingUnitTestMapNullReverse()
    {
        var mapper = MapperHelper.GetMapper();

        var filterGrpc = mapper.Map<FilterModel>(null);
        filterGrpc.Should().BeNull();
    }

}
