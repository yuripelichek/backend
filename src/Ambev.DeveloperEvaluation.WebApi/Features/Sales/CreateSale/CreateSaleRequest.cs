namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Request model for creating a new sale
/// </summary>
public class CreateSaleRequest
{
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
    /// The items in the sale
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Request model for a sale item
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// The product ID
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of items
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price
    /// </summary>
    public decimal UnitPrice { get; set; }
} 