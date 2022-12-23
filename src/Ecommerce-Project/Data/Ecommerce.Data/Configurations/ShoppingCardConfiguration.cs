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
                .HasOne(s => s.User)
                .WithOne(u => u.ShoppingCard)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
