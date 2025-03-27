namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Request model for updating a sale
/// </summary>
public class UpdateSaleRequest
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
} 