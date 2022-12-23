namespace Ecommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShoppingCardConfiguration : IEntityTypeConfiguration<ShoppingCard>
    {
        public void Configure(EntityTypeBuilder<ShoppingCard> shoppingCardBuilder)
        {
            shoppingCardBuilder
                .HasKey(s => new { s.UserId, s.ProductId });

            shoppingCardBuilder
                .HasOne(s => s.User)
                .WithMany(u => u.ShoppingCards)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            shoppingCardBuilder
                .HasOne(s => s.Product)
                .WithMany(p => p.ShoppingCards)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
