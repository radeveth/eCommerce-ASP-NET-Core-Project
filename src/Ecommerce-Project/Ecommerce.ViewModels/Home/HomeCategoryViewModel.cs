namespace Ecommerce.ViewModels.Home
{
    using Ecommerce.ViewModels.Products;

    public class HomeCategoryViewModel
    {
        public string Name { get; set; }

        public IEnumerable<HomeProductViewModel> Products { get; set; }
    }
}
