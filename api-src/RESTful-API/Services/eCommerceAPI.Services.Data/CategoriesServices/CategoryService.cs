namespace eCommerceAPI.Services.Data.CategoriesServices
{
    using AutoMapper;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Categories;
    using eCommerceAPI.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly EcommerceApiDbContext dbContext;

        public CategoryService(EcommerceApiDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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

        public async Task CreateAsync(CategoryFoemModel categoryForm)
        {
            Category category = new Category()
            {
                Name = categoryForm.Name,
                Description = categoryForm.Description,
                UserId = categoryForm.UserId,
            };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
