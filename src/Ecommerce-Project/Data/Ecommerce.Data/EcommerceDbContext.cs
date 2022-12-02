namespace Ecommerce.Data
{
    using Ecommerce.Data.Configurations;
    using Ecommerce.Data.Models;
    using Eommerce.Data.Configurations;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EcommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public EcommerceDbContext()
        {
        }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<OrderAddress> OrderAddresses { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ProductWishlist> ProductsWishlist { get; set; }

        public DbSet<ReviewVote> ReviewVotes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewVoteConfiguration());
            modelBuilder.ApplyConfiguration(new OrderAddressConfiguration());
            modelBuilder.ApplyConfiguration(new ProductWishlistConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.DefaultConnection());
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
