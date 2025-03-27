using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Command to retrieve all sales
/// </summary>
public record GetSalesCommand : IRequest<IEnumerable<GetSaleResult>>; 