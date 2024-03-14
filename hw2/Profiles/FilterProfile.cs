using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace hw2.Profiles;

public class FilterProfile : Profile
{
    public FilterProfile()
    {
        CreateMap<FilterModel, Models.Filter>()
            .ForMember(item => item.TypeProduct,
                operation => operation.MapFrom(from => from.ProductType))
            .ForMember(item => item.DateCreation,
                operation => operation.MapFrom(from => from.DateCreation))
            .ForMember(item => item.WarehouseNumber,
                operation => operation.MapFrom(from => from.WarehouseNumber))
            .ForMember(item => item.PageNumber,
                operation => operation.MapFrom(from => from.PageNumber))
            .ForMember(item => item.PageSize,
                operation => operation.MapFrom(from => from.PageSize))
            .ReverseMap();
        
        CreateMap<DateTime, Timestamp>().ConvertUsing(
            x => x.ToTimestamp());
        CreateMap<Timestamp, DateTime>().ConvertUsing(x => x.ToDateTime());
    }
}