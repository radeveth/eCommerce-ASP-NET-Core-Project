namespace eCommerceAPI.Services.Data.CategoriesService
{
    using eCommerceAPI.InputModels.Categories;

    public interface ICategoryService
    {
        public Task CreateAsync(CategoryFoemModel categoryFoem);
    }
}
