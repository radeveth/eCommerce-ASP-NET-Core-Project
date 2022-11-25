namespace Ecommerce.Services.Mappings
{
    using AutoMapper;
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.ApplicationUsers;
    using Ecommerce.ViewModels.Categories;
    using Ecommerce.ViewModels.Images;
    using Ecommerce.ViewModels.Products;

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
                    .ForMember(x => x.Image, y => y.MapFrom(mapExpression: s => s.ProductCategories
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
