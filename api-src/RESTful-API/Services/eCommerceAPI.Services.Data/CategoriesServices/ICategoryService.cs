namespace eCommerceAPI.Services.Data.CategoriesServices
{
    using eCommerceAPI.InputModels.Categories;
    using eCommerceAPI.ViewModels.Categories;

    public interface ICategoryService
    {
        public Task<CategoryViewModel> GetByIdAync(int id);

        public IEnumerable<CategoryViewModel> GetAll();

        public Task CreateAsync(CategoryFoemModel categoryFoem);
    }
}
