using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    public static CreateSaleCommand CreateValidCommand()
    {
        return new CreateSaleCommand
        {
            SaleNumber = "SALE001",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemCommand>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5,
                    UnitPrice = 10.00m
                }
            }
        };
    }

    public static CreateSaleCommand CreateCommandWithDiscount()
    {
        return new CreateSaleCommand
        {
            SaleNumber = "SALE002",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemCommand>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 15,
                    UnitPrice = 10.00m
                }
            }
        };
    }

    public static Sale CreateValidSale()
    {
        var sale = new Sale("SALE001", Guid.NewGuid(), Guid.NewGuid());
        sale.AddItem(Guid.NewGuid(), 5, 10.00m);
        return sale;
    }

    public static Sale CreateSaleWithDiscount()
    {
        var sale = new Sale("SALE002", Guid.NewGuid(), Guid.NewGuid());
        sale.AddItem(Guid.NewGuid(), 15, 10.00m);
        return sale;
    }
} 