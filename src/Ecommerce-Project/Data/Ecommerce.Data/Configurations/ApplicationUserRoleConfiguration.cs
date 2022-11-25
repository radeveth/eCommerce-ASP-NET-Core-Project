namespace Eommerce.Data.Configurations
{
    using Ecommerce.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> applicationUserRoleBuilder)
        {
            applicationUserRoleBuilder
                .HasKey(a => new { a.UserId, a.RoleId });

            applicationUserRoleBuilder
                .HasOne(a => a.User)
                .WithMany(u => u.ApplicationUserRoles)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            applicationUserRoleBuilder
                .HasOne(a => a.Role)
                .WithMany(r => r.ApplicationUserRoles)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
