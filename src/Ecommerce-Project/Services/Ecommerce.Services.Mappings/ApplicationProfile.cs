namespace Ecommerce.Services.Mappings
{
    using AutoMapper;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Products;
    using Ecommerce.ViewModels.ApplicationUsers;
    using Ecommerce.ViewModels.Categories;
    using Ecommerce.ViewModels.Home;
    using Ecommerce.ViewModels.Images;
    using Ecommerce.ViewModels.Products;

    public class ApplicationProfile
    {
        public class ProductsProfile : Profile
        {
            public ProductsProfile()
            {
                this.CreateMap<Product, ProductViewModel>()
                    .ForMember(x => x.Image, y => y.MapFrom(s => s.Images.FirstOrDefault()))
                    .ForMember(x => x.AverageReview, y => y.MapFrom(s => s.Reviews.Sum(r => (int)r.ReviewScale)))
                    .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name));

                this.CreateMap<Brand, ProductBrandFormModel>();
                this.CreateMap<Category, ProductCategoryFormModel>();
            }
        }

        public class ImageProfile : Profile
        {
            public ImageProfile()
            {
                this.CreateMap<Image, ImageViewModel>()
                    .ForMember(x => x.Src, y => y.MapFrom(s => string.Format("data:image/png;base64, {0}", Convert.ToBase64String(s.Src))));
            }
        }

        public class CategoriesProffile : Profile
        {
            public CategoriesProffile()
            {
                this.CreateMap<Category, CategoryViewModel>()
                    .ForMember(x => x.Image, y => y.MapFrom(s => s.Products
                    .OrderByDescending(p => p.Reviews.Count)
                    .FirstOrDefault().Images.FirstOrDefault()));
            }
        }

        public class ApplicationUsersProffile : Profile
        {
            public ApplicationUsersProffile()
            {
                this.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            }
        }

        public class HomeProffile : Profile
        {
            public HomeProffile()
            {
                this.CreateMap<Category, HomeCategoryViewModel>();
            }
        }
    }
}
