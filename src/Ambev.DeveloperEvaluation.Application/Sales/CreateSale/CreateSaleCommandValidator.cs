using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for CreateSaleCommand
    /// </summary>
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one item is required");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemCommandValidator());
    }
}

/// <summary>
/// Validator for CreateSaleItemCommand
/// </summary>
public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    /// <summary>
    /// Initializes validation rules for CreateSaleItemCommand
    /// </summary>
    public CreateSaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero")
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 identical items");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");
    }
} 