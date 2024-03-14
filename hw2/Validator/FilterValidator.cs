using FluentValidation;
using hw2.Models;

namespace hw2.Validator;

public class FilterValidator: AbstractValidator<Filter>
{
    public FilterValidator()
    {
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
        RuleFor(item => item.PageNumber)
            .NotNull()
            .Must(item => item > 0)
            .WithMessage("PageNumber failed validation");
        RuleFor(item => item.PageSize)
            .NotNull()
            .Must(item => item >= 0)
            .WithMessage("PageSize failed validation");
    }
}