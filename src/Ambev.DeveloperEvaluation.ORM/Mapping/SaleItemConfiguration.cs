using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(si => si.ProductId).IsRequired();
        builder.Property(si => si.Quantity).IsRequired();
        builder.Property(si => si.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(si => si.Discount).HasPrecision(5, 2).IsRequired();
        builder.Property(si => si.TotalAmount).HasPrecision(18, 2).IsRequired();

        // Configure foreign key relationship with Sale
        builder.Property<Guid>("SaleId").IsRequired();
        builder.HasOne<Sale>()
            .WithMany(s => s.Items)
            .HasForeignKey("SaleId")
            .OnDelete(DeleteBehavior.Cascade);
    }
} 