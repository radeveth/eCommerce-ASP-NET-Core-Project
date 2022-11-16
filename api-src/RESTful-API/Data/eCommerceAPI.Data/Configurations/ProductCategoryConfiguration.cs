namespace eCommerceAPI.Data.Configurations
{
    using eCommerceAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> productCategoryConfigurationBuilder)
        {
            productCategoryConfigurationBuilder
                .HasKey(p => new { p.ProductId, p.CategoryId });

            productCategoryConfigurationBuilder
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            productCategoryConfigurationBuilder
                .HasOne(p => p.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
