using AutoMapper;
using hw2.Profiles;

namespace hw3.TestHelpers;

public static class MapperHelper
{
    public static IMapper GetMapper()
    {
        var configuration = new MapperConfiguration(expression =>
        {
            expression.AddProfile<FilterProfile>();
            expression.AddProfile<ProductProfile>();
        });
        var mapper = new Mapper(configuration);
        return mapper;
    }
}