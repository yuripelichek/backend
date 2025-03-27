using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Handler for retrieving all sales
/// </summary>
public class GetSalesHandler : IRequestHandler<GetSalesCommand, IEnumerable<GetSaleResult>>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of GetSalesHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    public GetSalesHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the request to retrieve all sales
    /// </summary>
    /// <param name="request">The get sales command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of all sales</returns>
    public async Task<IEnumerable<GetSaleResult>> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(cancellationToken);
        return sales.Select(sale => new GetSaleResult
        {
            Id = sale.Id,
            SaleNumber = sale.SaleNumber,
            SaleDate = sale.SaleDate,
            CustomerId = sale.CustomerId,
            TotalAmount = sale.TotalAmount,
            BranchId = sale.BranchId,
            IsCancelled = sale.IsCancelled,
            Items = sale.Items.Select(item => new SaleItemResult
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                TotalAmount = item.TotalAmount
            }).ToList()
        });
    }
} 