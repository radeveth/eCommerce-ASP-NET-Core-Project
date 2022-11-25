namespace Ecommerce.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider);
    }
}
