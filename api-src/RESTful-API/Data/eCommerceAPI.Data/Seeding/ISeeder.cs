namespace eCommerceAPI.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(EcommerceApiDbContext dbContext, IServiceProvider serviceProvider);
    }
}
