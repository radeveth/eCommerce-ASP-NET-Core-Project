namespace Ecommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductWishlistConfiguration : IEntityTypeConfiguration<ProductWishlist>
    {
        public void Configure(EntityTypeBuilder<ProductWishlist> productWishlistBuilder)
        {
            productWishlistBuilder
                .HasKey(p => new { p.UserId, p.ProductId });

            productWishlistBuilder
                .HasOne(p => p.User)
                .WithMany(u => u.ProductsWishlist)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            productWishlistBuilder
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductsWishlist)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
