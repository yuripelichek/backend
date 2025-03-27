namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Result of creating a new sale
/// </summary>
public class CreateSaleResult
{
    /// <summary>
    /// The unique identifier of the created sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date when the sale was made
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The customer ID
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The branch ID
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The items in the sale
    /// </summary>
    public List<CreateSaleItemResult> Items { get; set; } = new();
}

/// <summary>
/// Result for a sale item
/// </summary>
public class CreateSaleItemResult
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

    /// <summary>
    /// The discount applied
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total amount for this item
    /// </summary>
    public decimal TotalAmount { get; set; }
} 