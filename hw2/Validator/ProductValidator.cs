using FluentValidation;
using hw2.Models;

namespace hw2.Validator;

public class ProductValidator: AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(item => item.ProductId)
            .NotNull()
            .WithMessage("ProductId required field");
        RuleFor(item => item.Name)
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(20)
            .WithMessage("Name failed validation");
        RuleFor(item => item.Price)
            .NotNull()
            .Must(item => item > 0)
            .WithMessage("Price failed validation");
        RuleFor(item => item.Weight)
            .NotNull()
            .Must(item => item > 0)
            .WithMessage("Weight failed validation");
        RuleFor(item => item.WarehouseNumber)
            .NotNull()
            .Must(item => item > 0)
            .WithMessage("WarehouseNumber failed validation");
        RuleFor(item => item.DateCreation)
            .NotNull()
            .WithMessage("DateCreation failed validation");
        RuleFor(item => item.TypeProduct)
            .NotNull()
            .WithMessage("TypeProduct failed validation");
    }
}