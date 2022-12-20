namespace Ecommerce.Services.Data.AdminServices
{
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.Admin;

    public class AdminService : BaseService, IAdminService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly IMapper mapper;

        public AdminService(EcommerceDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductsStatisticsServiceModel> GetApplicationProductsStatistics()
        {
            IEnumerable<Product> products = this.dbContext.Products.AsQueryable();
            IEnumerable<Category> categories = this.dbContext.Categories.AsQueryable();

            foreach (var category in categories)
            {
                category.Products = products.Where(p => p.CategoryId == category.Id).ToList();
            }

            ProductsStatisticsServiceModel productsStatisticsServiceModel = new ProductsStatisticsServiceModel()
            {
                Products = mapper.Map<IEnumerable<ProductStatisticsViewModel>>(products),
                Categories = mapper.Map<IEnumerable<CategoryStatisticsViewModel>>(categories),
            };

            return productsStatisticsServiceModel;
        }
    }
}
