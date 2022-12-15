namespace Ecommerce.Services.Data.HomeServices
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Home;

    public class HomeService : IHomeService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public HomeService(EcommerceDbContext dbContext, IMapper mapper, IProductService productService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.productService = productService;
        }

        public async Task<HomeServiceModel> GetHomeServiceModel(int countOfProductsPerCategory, string userId = null)
        {
            ICollection<HomeCategoryViewModel> categories = await this.GetCategories(countOfProductsPerCategory, userId);

            HomeServiceModel serviceModel = new HomeServiceModel()
            {
                Categories = categories,
            };

            return serviceModel;
        }

        private async Task<ICollection<HomeCategoryViewModel>> GetCategories(int countOfProductsPerCategory, string userId = null)
        {
            ICollection<HomeCategoryViewModel> categoriesView = new List<HomeCategoryViewModel>();

            IQueryable<Category> categories = this.dbContext.Categories.AsQueryable();

            foreach (var category in categories)
            {
                categoriesView.Add(new HomeCategoryViewModel()
                {
                    Name = category.Name,
                    Products = await this.productService.GetAllByCategory(category.Name, userId),
                });
            }

            return categoriesView;
        }
    }
}
