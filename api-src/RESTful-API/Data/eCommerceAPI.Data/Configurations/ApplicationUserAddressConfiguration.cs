namespace eCommerceAPI.Data.Configurations
{
    using eCommerceAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserAddressConfiguration : IEntityTypeConfiguration<ApplicationUserAddress>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserAddress> applicationUserAddressBuilder)
        {
            applicationUserAddressBuilder
                .HasOne(a => a.User)
                .WithOne(a => a.Address)
                .HasForeignKey<ApplicationUser>(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
