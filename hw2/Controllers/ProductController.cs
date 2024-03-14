using FluentValidation;
using hw2.Models;
using hw2.Services;
using Microsoft.AspNetCore.Mvc;

namespace hw2.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IValidator<Product> _validatorProduct;
    private readonly IValidator<Filter> _validatorFilter;

    public ProductController(IProductService service, IValidator<Product> validatorProduct, IValidator<Filter> validatorFilter)
    {
        _service = service;
        _validatorProduct = validatorProduct;
        _validatorFilter = validatorFilter;
    }

    [HttpPost("[action]")]
    public bool CreateProduct(Product product)
    {
        var validationResult = _validatorProduct.Validate(product);
        return validationResult.IsValid && _service.CreateProduct(product);
    }
   
    [HttpGet("[action]")]
    public List<Product> GetProductList(Filter filter)
    {
        var validationResult = _validatorFilter.Validate(filter);
        if (!validationResult.IsValid)
            throw new Exception("Filter");
        
        return _service.GetProductListWithFilter(filter);
    }
   
    [HttpGet("[action]")]
    public Product GetProductById(long id)
    {
        return _service.GetProductById(id);
    }
   
    [HttpPatch("[action]")]
    public bool UpdateProductPrice(long id, decimal newPrice)
    {
        return _service.UpdatePriceProduct(id, newPrice);
    }

}