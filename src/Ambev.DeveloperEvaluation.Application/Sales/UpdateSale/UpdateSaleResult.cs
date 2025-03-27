namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Response model for UpdateSale operation
/// </summary>
public class UpdateSaleResult
{
    /// <summary>
    /// The unique identifier of the updated sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The customer ID
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The branch ID
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }
} 