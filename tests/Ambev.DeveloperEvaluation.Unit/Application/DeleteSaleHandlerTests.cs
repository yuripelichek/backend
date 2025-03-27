using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="DeleteSaleHandler"/> class.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly DeleteSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    /// <summary>
    /// Tests that an invalid sale deletion request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale id When deleting sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteSaleCommand(Guid.Empty); // Empty GUID will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that deleting a non-existent sale throws a not found exception.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale id When deleting sale Then throws not found exception")]
    public async Task Handle_NonExistentSale_ThrowsNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new DeleteSaleCommand(saleId);
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
} 