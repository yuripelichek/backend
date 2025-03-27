using MediatR;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing sale
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// The unique identifier of the sale to update
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The new sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The new customer ID
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The new branch ID
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 