using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository);
    }

    /// <summary>
    /// Tests that a valid sale retrieval request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale id When getting sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);
        var sale = CreateSaleHandlerTestData.CreateValidSale();

        var result = new GetSaleResult
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
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(sale.Id);
        getSaleResult.SaleNumber.Should().Be(sale.SaleNumber);
        getSaleResult.CustomerId.Should().Be(sale.CustomerId);
        getSaleResult.BranchId.Should().Be(sale.BranchId);
        getSaleResult.Items.Should().HaveCount(sale.Items.Count);
    }


    /// <summary>
    /// Tests that retrieving a non-existent sale throws a not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale id When getting sale Then throws not found exception")]
    public async Task Handle_NonExistentSale_ThrowsNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand(saleId);
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
} 