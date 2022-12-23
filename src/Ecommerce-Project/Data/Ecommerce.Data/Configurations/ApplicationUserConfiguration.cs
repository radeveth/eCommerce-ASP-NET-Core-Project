namespace Ecommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> applicationUserBuilder)
        {
            applicationUserBuilder
                .HasOne(a => a.ShoppingCard)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
