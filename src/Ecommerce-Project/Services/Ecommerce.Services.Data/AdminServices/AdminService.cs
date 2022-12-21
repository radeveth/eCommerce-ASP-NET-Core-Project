namespace Ecommerce.Services.Data.AdminServices
{
    using System.Data;
    using System.Linq;
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.Admin;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminService : BaseService, IAdminService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly UserManager userManager;
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public AdminService(EcommerceDbContext dbContext, IMapper mapper, IServiceProvider serviceProvider, Microsoft.AspNetCore.Identity.UserManager userManager)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.serviceProvider = serviceProvider;
            this.userManager = userManager;
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

        public async Task<UsersStatistsicsServiceModel> GetApplicationUsersStatistics()
        {
            var a = await this.userManager.GetUsersInRoleAsync();
            RoleManager<ApplicationRole> roleManager = this.serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            IEnumerable<ApplicationUser> users = this.GetUnDeletedUsers();

            ApplicationRole adminRole = await roleManager.FindByNameAsync("Administrator");

            foreach (var user in users)
            {
                user.Orders = this.GetUnDeletedOrders().Where(o => o.UserId == user.Id).ToList();
                user.Reviews = this.dbContext.Reviews.Where(r => r.UserId == user.Id).ToList();
            }

            return new UsersStatistsicsServiceModel()
            {
                Users = this.mapper.Map<IEnumerable<UserStatistsicsViewModel>>(users),
                Admins
            };
        }
    }
}
