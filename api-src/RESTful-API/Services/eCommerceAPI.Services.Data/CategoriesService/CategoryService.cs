namespace eCommerceAPI.Services.Data.CategoriesService
{
    using System.Threading.Tasks;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Categories;

    public class CategoryService : ICategoryService
    {
        private readonly EcommerceApiDbContext dbContext;

        public CategoryService(EcommerceApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CategoryFoemModel categoryFoem)
        {
            Category category = new Category()
            {
                Name = categoryFoem.Name,
                Description = categoryFoem.Description,
                UserId = categoryFoem.UserId,
            };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
