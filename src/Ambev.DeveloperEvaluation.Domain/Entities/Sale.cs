using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    public string SaleNumber { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public Guid BranchId { get; private set; }
    public bool IsCancelled { get; private set; }
    private Sale() { } 

    public Sale(string saleNumber, Guid customerId, Guid branchId)
    {
        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        CustomerId = customerId;
        BranchId = branchId;
        TotalAmount = 0;
        IsCancelled = false;
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale");

        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero");

        if (quantity > 20)
            throw new InvalidOperationException("Cannot sell more than 20 identical items");

        var item = new SaleItem(productId, quantity, unitPrice);
        _items.Add(item);
        RecalculateTotalAmount();
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");

        IsCancelled = true;
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalAmount);
    }
    
    public void Update(string saleNumber, Guid customerId, Guid branchId, bool isCancelled)
    {
        if (string.IsNullOrEmpty(saleNumber))
            throw new InvalidOperationException("Sale number cannot be empty");

        SaleNumber = saleNumber;
        CustomerId = customerId;
        BranchId = branchId;
        IsCancelled = isCancelled;
    }
}
