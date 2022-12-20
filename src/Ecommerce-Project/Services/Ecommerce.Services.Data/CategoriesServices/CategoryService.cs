namespace Ecommerce.Services.Data.CategoriesServices
{
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Categories;
    using Ecommerce.Services.Data.ApplicationUsersServices;
    using Ecommerce.ViewModels.Categories;

    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IMapper mapper;
        private readonly EcommerceDbContext dbContext;
        private readonly IApplicationUserService applicationUserService;

        public CategoryService(EcommerceDbContext dbContext, IMapper mapper, IApplicationUserService applicationUserService)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.applicationUserService = applicationUserService;
        }

        // Create
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

        // Read
        public CategoryViewModel GetViewModelById(int id)
        {
            Category category = this.GetUnDeletedCategories()
                .FirstOrDefault(c => c.Id == id);

            return this.mapper.Map<CategoryViewModel>(category);
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            IEnumerable<Category> categories = this.GetUnDeletedCategories();

            return this.mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        // Update
        public async Task UpdateAsync(int id, CategoryFormModel categoryForm)
        {
            Category category = this.GetUndeletedCategoryById(id);

            category.Name = categoryForm.Name;
            category.Description = categoryForm.Description;
            category.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            Category category = this.GetById(id);

            category.IsDeleted = false;
            category.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            Category category = this.GetUndeletedCategoryById(id);

            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            category.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        // Useful methods
        private Category GetUndeletedCategoryById(int id)
        {
            return this.GetUnDeletedCategories()
                       .FirstOrDefault(c => c.Id == id);
        }

        private Category GetById(int id)
        {
            return this.dbContext
                       .Categories
                       .FirstOrDefault(c => c.Id == id);
        }
    }
}
