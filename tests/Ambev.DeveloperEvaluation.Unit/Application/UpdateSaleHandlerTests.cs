using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale update request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new UpdateSaleCommand
        {
            Id = saleId,
            SaleNumber = "SALE003",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            IsCancelled = true
        };

        var existingSale = CreateSaleHandlerTestData.CreateValidSale();
        var updatedSale = CreateSaleHandlerTestData.CreateValidSale();
        updatedSale.Update(command.SaleNumber, command.CustomerId, command.BranchId, command.IsCancelled);

        var result = new UpdateSaleResult
        {
            Id = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber,
            CustomerId = updatedSale.CustomerId,
            BranchId = updatedSale.BranchId,
            IsCancelled = updatedSale.IsCancelled,
            TotalAmount = updatedSale.TotalAmount
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(existingSale);
        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(updatedSale);
        _mapper.Map<UpdateSaleResult>(updatedSale).Returns(result);

        // When
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateSaleResult.Should().NotBeNull();
        updateSaleResult.Id.Should().Be(updatedSale.Id);
        updateSaleResult.SaleNumber.Should().Be(command.SaleNumber);
        updateSaleResult.CustomerId.Should().Be(command.CustomerId);
        updateSaleResult.BranchId.Should().Be(command.BranchId);
        updateSaleResult.IsCancelled.Should().Be(command.IsCancelled);
    }

    /// <summary>
    /// Tests that an invalid sale update request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existent sale throws a not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale id When updating sale Then throws not found exception")]
    public async Task Handle_NonExistentSale_ThrowsNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new UpdateSaleCommand
        {
            Id = saleId,
            SaleNumber = "SALE003",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            IsCancelled = true
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    /// <summary>
    /// Tests that updating a sale with an existing sale number throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given existing sale number When updating sale Then throws invalid operation exception")]
    public async Task Handle_ExistingSaleNumber_ThrowsInvalidOperationException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new UpdateSaleCommand
        {
            Id = saleId,
            SaleNumber = "SALE003",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            IsCancelled = true
        };

        var existingSale = CreateSaleHandlerTestData.CreateValidSale();
        var saleWithNumber = CreateSaleHandlerTestData.CreateValidSale();
        saleWithNumber.Update(command.SaleNumber, Guid.NewGuid(), Guid.NewGuid(), false);

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(existingSale);
        _saleRepository.GetBySaleNumberAsync(command.SaleNumber, Arg.Any<CancellationToken>())
            .Returns(saleWithNumber);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct sale.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When handling Then maps sale to result")]
    public async Task Handle_ValidSale_MapsSaleToResult()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new UpdateSaleCommand
        {
            Id = saleId,
            SaleNumber = "SALE003",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            IsCancelled = true
        };

        var existingSale = CreateSaleHandlerTestData.CreateValidSale();
        var updatedSale = CreateSaleHandlerTestData.CreateValidSale();
        updatedSale.Update(command.SaleNumber, command.CustomerId, command.BranchId, command.IsCancelled);

        var result = new UpdateSaleResult
        {
            Id = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber,
            CustomerId = updatedSale.CustomerId,
            BranchId = updatedSale.BranchId,
            IsCancelled = updatedSale.IsCancelled,
            TotalAmount = updatedSale.TotalAmount
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(existingSale);
        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(updatedSale);
        _mapper.Map<UpdateSaleResult>(updatedSale).Returns(result);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<UpdateSaleResult>(Arg.Is<Sale>(s => s.Id == updatedSale.Id));
    }
} 