using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Result containing the retrieved sale details
/// </summary>
public record GetSaleResult
{
    /// <summary>
    /// Gets the unique identifier of the sale
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the sale number
    /// </summary>
    public string SaleNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets the date when the sale was created
    /// </summary>
    public DateTime SaleDate { get; init; }

    /// <summary>
    /// Gets the unique identifier of the customer
    /// </summary>
    public Guid CustomerId { get; init; }

    /// <summary>
    /// Gets the total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; init; }

    /// <summary>
    /// Gets the unique identifier of the branch
    /// </summary>
    public Guid BranchId { get; init; }

    /// <summary>
    /// Gets whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; init; }

    /// <summary>
    /// Gets the collection of items in the sale
    /// </summary>
    public IReadOnlyCollection<SaleItemResult> Items { get; init; } = Array.Empty<SaleItemResult>();
}

/// <summary>
/// Represents an item in the sale
/// </summary>
public record SaleItemResult
{
    /// <summary>
    /// Gets the unique identifier of the item
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the unique identifier of the product
    /// </summary>
    public Guid ProductId { get; init; }

    /// <summary>
    /// Gets the quantity of the item
    /// </summary>
    public int Quantity { get; init; }

    /// <summary>
    /// Gets the unit price of the item
    /// </summary>
    public decimal UnitPrice { get; init; }

    /// <summary>
    /// Gets the discount applied to the item
    /// </summary>
    public decimal Discount { get; init; }

    /// <summary>
    /// Gets the total amount of the item
    /// </summary>
    public decimal TotalAmount { get; init; }
} 