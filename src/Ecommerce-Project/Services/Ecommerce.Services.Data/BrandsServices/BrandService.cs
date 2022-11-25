namespace Ecommerce.Services.Data.BrandsServices
{
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Brands;

    public class BrandService : IBrandService
    {
        private readonly EcommerceDbContext dbContext;

        public BrandService(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(BrandFormModel brandForm)
        {
            Brand brand = new Brand()
            {
                Name = brandForm.Name,
                Description = brandForm.Description,
                YearOfFoundation = brandForm.YearOfFoundation,
                FounderName = brandForm.FounderName,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await this.dbContext.Brands.AddAsync(brand);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
