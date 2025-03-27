using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

/// <summary>
/// AutoMapper profile for GetSales feature
/// </summary>
public class GetSalesProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of GetSalesProfile
    /// </summary>
    public GetSalesProfile()
    {
        CreateMap<GetSaleResult, GetSaleResponse>();
    }
} 