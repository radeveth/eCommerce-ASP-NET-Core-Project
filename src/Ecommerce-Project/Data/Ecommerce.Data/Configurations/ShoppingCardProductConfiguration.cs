namespace Ecommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShoppingCardProductConfiguration : IEntityTypeConfiguration<ShoppingCardProduct>
    {
        public void Configure(EntityTypeBuilder<ShoppingCardProduct> shoppingCardProductBuilder)
        {
            shoppingCardProductBuilder
                .HasKey(s => new { s.ShoppingCardId, s.ProductId });

            shoppingCardProductBuilder
                .HasOne(s => s.ShoppingCard)
                .WithMany(s => s.ShoppingCardProducts)
                .HasForeignKey(s => s.ShoppingCardId)
                .OnDelete(DeleteBehavior.Restrict);

            shoppingCardProductBuilder
                .HasOne(s => s.Product)
                .WithMany(p => p.ShoppingCardProducts)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
