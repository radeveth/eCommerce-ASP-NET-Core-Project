namespace eCommerceAPI.Services.Data.BrandsServices
{
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Brands;
    using System.Threading.Tasks;

    public class BrandService : IBrandService
    {
        private readonly EcommerceApiDbContext dbContext;

        public BrandService(EcommerceApiDbContext dbContext)
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
