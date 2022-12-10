namespace Ecommerce.Services.Data.CategoriesServices
{
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Categories;
    using Ecommerce.Services.Data.ApplicationUsersServices;
    using Ecommerce.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly EcommerceDbContext dbContext;
        private readonly IApplicationUserService applicationUserService;

        public CategoryService(EcommerceDbContext dbContext, IMapper mapper, IApplicationUserService applicationUserService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.applicationUserService = applicationUserService;
        }

        public async Task<CategoryViewModel> GetByIdAync(int id)
        {
            Category category = await this.dbContext
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            return this.mapper.Map<CategoryViewModel>(category);
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            IEnumerable<Category> categories = this.dbContext.Categories;

            return this.mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task CreateAsync(CategoryFormModel categoryForm)
        {
            Category category = new Category()
            {
                Name = categoryForm.Name,
                Description = categoryForm.Description,
                UserId = categoryForm.UserId,
                User = await this.applicationUserService.GetById(categoryForm.UserId),
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
