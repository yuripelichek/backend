using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }

    private SaleItem() { } 

    public SaleItem(Guid productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CalculateDiscount();
        CalculateTotalAmount();
    }

    private void CalculateDiscount()
    {
        if (Quantity < 4)
        {
            Discount = 0;
            return;
        }

        if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = 0.20m; // 20% discount
            return;
        }

        if (Quantity >= 4)
        {
            Discount = 0.10m; // 10% discount
            return;
        }

        Discount = 0;
    }

    private void CalculateTotalAmount()
    {
        var subtotal = Quantity * UnitPrice;
        var discountAmount = subtotal * Discount;
        TotalAmount = subtotal - discountAmount;
    }
} 