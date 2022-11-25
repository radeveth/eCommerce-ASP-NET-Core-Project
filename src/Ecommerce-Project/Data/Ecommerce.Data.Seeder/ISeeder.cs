namespace Ecommerce.Data.Seeder
{
    public interface ISeeder
    {
        Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider);
    }
}
