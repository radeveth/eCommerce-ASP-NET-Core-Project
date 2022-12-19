namespace Ecommerce.Services.Data.CategoriesServices
{
    using Ecommerce.InputModels.Categories;
    using Ecommerce.ViewModels.Categories;

    public interface ICategoryService
    {
        // Create
        public Task CreateAsync(CategoryFormModel categoryFoem);

        // Read
        public CategoryViewModel GetViewModelById(int id);

        public IEnumerable<CategoryViewModel> GetAll();

        // Update
        public Task UpdateAsync(int id, CategoryFormModel categoryForm);

        public Task RestoreAsync(int id);

        // Delete
        public Task DeleteAsync(int id);
    }
}
