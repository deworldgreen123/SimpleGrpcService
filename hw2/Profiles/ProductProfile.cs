using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace hw2.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductModel, Models.Product>()
            .ForMember(item => item.ProductId,
                operation => operation.MapFrom(from => from.ProductId))
            .ForMember(item => item.Name,
                operation => operation.MapFrom(from => from.ProductName))
            .ForMember(item => item.Price,
                operation => operation.MapFrom(from => from.ProductPrice))
            .ForMember(item => item.Weight,
                operation => operation.MapFrom(from => from.ProductWeight))
            .ForMember(item => item.TypeProduct,
                operation => operation.MapFrom(from => from.ProductType))
            .ForMember(item => item.DateCreation,
                operation => operation.MapFrom(from => from.DateCreation))
            .ForMember(item => item.WarehouseNumber,
                operation => operation.MapFrom(from => from.WarehouseNumber))
            .ReverseMap();
        
        
        CreateMap<Models.Product, ProductModel>()
            .ForMember(item => item.ProductId,
                operation => operation.MapFrom(from => from.ProductId))
            .ForMember(item => item.ProductName,
                operation => operation.MapFrom(from => from.Name))
            .ForMember(item => item.ProductPrice,
                operation => operation.MapFrom(from => from.Price))
            .ForMember(item => item.ProductWeight,
                operation => operation.MapFrom(from => from.Weight))
            .ForMember(item => item.ProductType,
                operation => operation.MapFrom(from => from.TypeProduct))
            .ForMember(item => item.DateCreation,
                operation => operation.MapFrom(from => from.DateCreation))
            .ForMember(item => item.WarehouseNumber,
                operation => operation.MapFrom(from => from.WarehouseNumber))
            .ReverseMap();

        CreateMap<bool, CreateProductResponse>()
            .ForMember(item => item.Result,
                operation => operation.MapFrom(from => from));
        CreateMap<bool, UpdateProductPriceResponse>()
            .ForMember(item => item.Result,
                operation => operation.MapFrom(from => from));

        CreateMap<DateTime, Timestamp>().ConvertUsing(x => x.ToTimestamp());
        CreateMap<Timestamp, DateTime>().ConvertUsing(x => x.ToDateTime());
    }

}