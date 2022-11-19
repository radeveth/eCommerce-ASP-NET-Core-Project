namespace eCommerceAPI.Mappings
{
    using System.Linq;
    using AutoMapper;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.ViewModels.ApplicationUsers;
    using eCommerceAPI.ViewModels.Categories;
    using eCommerceAPI.ViewModels.Images;
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
                this.CreateMap<Image, ImageViewModel>();
            }
        }

        public class CategoriesProffile : Profile
        {
            public CategoriesProffile()
            {
                this.CreateMap<Category, CategoryViewModel>()
                    .ForMember(x => x.Image, y => y.MapFrom(s => s.ProductCategories
                        .Select(p => p.Product)
                        .OrderByDescending(p => p.Reviews.Sum(r => (int)r.ReviewScale) / p.Reviews.Count)
                        .FirstOrDefault()
                        .Images.FirstOrDefault()));
            }
        }

        public class ApplicationUsersProffile : Profile
        {
            public ApplicationUsersProffile()
            {
                this.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            }
        }
    }
}
