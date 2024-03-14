using AutoMapper;
using Grpc.Core;
using hw2.Services;
using FluentValidation;

namespace hw2.GrpcServices;

public class ProductGrpcService : ProductService.ProductServiceBase 
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;


    public ProductGrpcService(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        var productLocal = _mapper.Map<Models.Product>(request.Product);

        var result = _service.CreateProduct(productLocal);
        await Task.CompletedTask;
        return _mapper.Map<CreateProductResponse>(result);
    }

    public override async Task<GetProductListResponse> GetProductList(GetProductListRequest request, ServerCallContext context)
    {
        var filter = _mapper.Map<Models.Filter>(request.Filter);

        var result = _service.GetProductListWithFilter(filter);
        var listGrpc = _mapper.Map<List<ProductModel>>(result);
        var response = new GetProductListResponse()
        {
            Products = { listGrpc }
        };
        
        await Task.CompletedTask;
        return response;
    }

    public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
    {
        var result = _service.GetProductById(request.Id);
        
        var response = new GetProductByIdResponse()
        {
            Product = _mapper.Map<ProductModel>(result)
        };
        
        await Task.CompletedTask;
        return response;
    }

    public override async Task<UpdateProductPriceResponse> UpdateProductPrice(UpdateProductPriceRequest request, ServerCallContext context)
    {
        var result = _service.UpdatePriceProduct(request.Id, Convert.ToDecimal(request.NewPrice));
        await Task.CompletedTask;
        return _mapper.Map<UpdateProductPriceResponse>(result); 
    }
}