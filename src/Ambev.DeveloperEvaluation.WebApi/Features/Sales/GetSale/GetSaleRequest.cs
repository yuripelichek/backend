namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request model for retrieving a sale by its ID
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }
} 