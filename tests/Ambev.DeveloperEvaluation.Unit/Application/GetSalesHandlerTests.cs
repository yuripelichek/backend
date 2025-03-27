using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="GetSalesHandler"/> class.
/// </summary>
public class GetSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSalesHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSalesHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public GetSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSalesHandler(_saleRepository);
    }

    /// <summary>
    /// Tests that a valid sales retrieval request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid request When getting sales Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new GetSalesCommand();
        var sales = new List<Sale>
        {
            CreateSaleHandlerTestData.CreateValidSale(),
            CreateSaleHandlerTestData.CreateSaleWithDiscount()
        };

        var expectedResults = sales.Select(sale => new GetSaleResult
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
        }).ToList();

        _saleRepository.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        var getSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSalesResult.Should().NotBeNull();
        getSalesResult.Should().HaveCount(sales.Count);
        getSalesResult.Should().BeEquivalentTo(expectedResults);
    }

    /// <summary>
    /// Tests that an empty sales list is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given no sales When getting sales Then returns empty list")]
    public async Task Handle_NoSales_ReturnsEmptyList()
    {
        // Given
        var command = new GetSalesCommand();
        _saleRepository.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(new List<Sale>());

        // When
        var getSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSalesResult.Should().NotBeNull();
        getSalesResult.Should().BeEmpty();
    }
} 