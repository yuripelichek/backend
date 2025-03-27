namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Response model for retrieving a sale
/// </summary>
public class GetSaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was created
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the customer
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the collection of items in the sale
    /// </summary>
    public IReadOnlyCollection<SaleItemResponse> Items { get; set; } = Array.Empty<SaleItemResponse>();
}

/// <summary>
/// Represents an item in the sale
/// </summary>
public class SaleItemResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the item
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the item
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the item
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the item
    /// </summary>
    public decimal TotalAmount { get; set; }
} 