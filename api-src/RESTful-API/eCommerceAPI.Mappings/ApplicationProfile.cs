namespace eCommerceAPI.Mappings
{
    using AutoMapper;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.ViewModels.Products;

    public class ApplicationProfile
    {
        public class ProductsProfile : Profile
        {
            public ProductsProfile()
            {
                this.CreateMap<Product, ProductViewModel>()
                    .ForMember(x => x.Brand, y => y.MapFrom(s => s.Brand.Name))
                    .ForMember(x => x.AverageReview, y => y.MapFrom(s => s.Reviews.Sum(r => (int)r.ReviewScale)));
            }
        }

        public class ImageProfile : Profile
        {
            public ImageProfile()
            {
                this.CreateMap<Image, ProductImageViewModel>();
            }
        }

        public class CategoriesProffile : Profile
        {
            public CategoriesProffile()
            {
                // this.CreateMap<>();
            }
        }
    }
}
