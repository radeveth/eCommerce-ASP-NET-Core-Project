namespace eCommerce.Services.Data.CategoriesServices
{
    using eCommerce.InputModels.Categories;
    using eCommerce.ViewModels.Categories;

    public interface ICategoryService
    {
        public Task<CategoryViewModel> GetByIdAync(int id);

        public IEnumerable<CategoryViewModel> GetAll();

        public Task CreateAsync(CategoryFoemModel categoryFoem);
    }
}
