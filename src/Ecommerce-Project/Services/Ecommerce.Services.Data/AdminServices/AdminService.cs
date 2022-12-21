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
    using Microsoft.Identity.Client;

    public class AdminService : BaseService, IAdminService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IMapper mapper;

        public AdminService(EcommerceDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Read
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
            IEnumerable<ApplicationUser> users = this.userManager.Users;
            IEnumerable<ApplicationUser> admins = await this.userManager.GetUsersInRoleAsync("Administrator");

            foreach (var user in users)
            {
                user.Orders = this.GetUnDeletedOrders().Where(o => o.UserId == user.Id).ToList();
                user.Reviews = this.dbContext.Reviews.Where(r => r.UserId == user.Id).ToList();
            }

            UsersStatistsicsServiceModel usersStatistsicsServiceModel = new UsersStatistsicsServiceModel()
            {
                Users = this.mapper.Map<IEnumerable<UserStatistsicsViewModel>>(users),
                Admins = this.mapper.Map<IEnumerable<AdminStatisticsViewModel>>(admins),
            };

            foreach (var admin in usersStatistsicsServiceModel.Admins)
            {
                IEnumerable<string> userRoles = this.dbContext.UserRoles
                    .Where(ur => ur.UserId == admin.Id)
                    .Select(ur => ur.RoleId)
                    .ToList();

                admin.Roles = this.roleManager.Roles
                    .Where(r => userRoles.Contains(r.Id))
                    .Select(r => new AdminRoleViewModel()
                        {
                            Id = r.Id,
                            Name = r.Name,
                        });
            }

            return usersStatistsicsServiceModel;
        }

        // Update
        public async Task DeleteUser(string id)
        {
            ApplicationUser user = this.userManager.Users.FirstOrDefault(u => u.Id == id);

            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
            user.ModifiedOn = DateTime.UtcNow;

        }

        public async Task RestoreUser(string id)
        {
            ApplicationUser user = this.userManager.Users.FirstOrDefault(u => u.Id == id);

            user.IsDeleted = false;
            user.DeletedOn = null;
            user.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
