namespace Ecommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShoppingCardConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> shoppingCardBuilder)
        {
            shoppingCardBuilder
                .HasOne(s => s.User)
                .WithOne(u => u.ShoppingCard)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
