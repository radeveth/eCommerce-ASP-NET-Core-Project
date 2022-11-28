namespace Ecommerce.Services.Data.HomeServices
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.Home;
    using Ecommerce.ViewModels.Products;

    public class HomeService : IHomeService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly IMapper mapper;

        public HomeService(EcommerceDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<HomeServiceModel> GetHomeServiceModel(int countOfProductsPerCategory)
        {
            ICollection<HomeCategoryViewModel> categories = this.GetCategories(countOfProductsPerCategory);

            HomeServiceModel serviceModel = new HomeServiceModel()
            {
                Categories = categories,
            };

            return serviceModel;
        }

        private ICollection<HomeCategoryViewModel> GetCategories(int countOfProductsPerCategory)
        {
            ICollection<HomeCategoryViewModel> categoriesView = new List<HomeCategoryViewModel>();

            IQueryable<Category> categories = this.dbContext.Categories.AsQueryable();

            foreach (var category in categories)
            {
                IEnumerable<Product> products = this.dbContext.Products.Where(p => p.CategoryId == category.Id).Take(countOfProductsPerCategory);

                foreach (var product in products)
                {
                    product.Images = this.dbContext.Images.Where(i => i.ProductId == product.Id).ToList();
                }

                categoriesView.Add(new HomeCategoryViewModel()
                {
                    Name = category.Name,
                    Products = this.mapper.Map<IEnumerable<ProductViewModel>>(products),
                });
            }

            return categoriesView;
        }
    }
}
