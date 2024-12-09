using Application.DTOs.Product;
using FluentValidation;
namespace Application.DTOs.Validators;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(product => product.Name).Length(20, 250).WithMessage("Maximum limit is 250 characters for a product name");
        RuleFor(product => product.Description).Length(10, 500).WithMessage("Maximum limit is 500 characters for the description.");
        RuleFor(product => product.Price).InclusiveBetween(1, int.MaxValue).WithMessage("Not a valid price");
        RuleFor(product => product.StockQuantity).InclusiveBetween(1, int.MaxValue).WithMessage("Not a valid quantity");
    }

}
