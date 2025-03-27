using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateSaleRequest
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");

        RuleFor(x => x.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required")
            .MaximumLength(50)
            .WithMessage("Sale number cannot exceed 50 characters");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
} 