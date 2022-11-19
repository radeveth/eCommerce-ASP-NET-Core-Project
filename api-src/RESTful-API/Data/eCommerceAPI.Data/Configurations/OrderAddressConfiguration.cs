namespace eCommerceAPI.Data.Configurations
{
    using eCommerceAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderAddressConfiguration : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> orderAddressBuilder)
        {
            orderAddressBuilder
                .HasMany(a => a.Orders)
                .WithOne(a => a.Address)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
