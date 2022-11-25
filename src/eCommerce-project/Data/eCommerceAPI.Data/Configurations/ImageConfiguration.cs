namespace eCommerce.Data.Configurations
{
    using eCommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> imageBuilder)
        {
            imageBuilder
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
