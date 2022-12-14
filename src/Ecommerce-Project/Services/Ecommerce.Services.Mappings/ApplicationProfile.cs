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
    using Ecommerce.ViewModels.Review;

    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Product Mappings
            this.CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.Image, y => y.MapFrom(s => s.Images.FirstOrDefault()))
                .ForMember(x => x.AverageReview, y => y.MapFrom(s => s.Reviews.Sum(r => (int)r.ReviewScale)))
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name));
            this.CreateMap<Product, HomeProductViewModel>()
                .ForMember(x => x.Image, y => y.MapFrom(s => s.Images.FirstOrDefault()))
                .ForMember(x => x.AverageReview, y => y.MapFrom(s => s.Reviews.Count() == 0 ? -1 : s.Reviews.Select(r => (int)r.ReviewScale).Sum() / s.Reviews.Count()))
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name))
                .ForMember(x => x.IsProductIsInWishlist, y => y.MapFrom(s => s.ProductsWishlist.));

            // Brand Mappings
            this.CreateMap<Brand, ProductBrandFormModel>();

            // Category Mappings
            this.CreateMap<Category, ProductCategoryFormModel>();
            this.CreateMap<Category, CategoryViewModel>()
                .ForMember(x => x.Image, y => y.MapFrom(s => s.Products
                .OrderByDescending(p => p.Reviews.Count)
                .FirstOrDefault().Images.FirstOrDefault()));
            this.CreateMap<Category, HomeCategoryViewModel>();

            // Image Mappings
            this.CreateMap<Image, ImageViewModel>()
                .ForMember(x => x.Src, y => y.MapFrom(s => string.Format("data:image/png;base64, {0}", Convert.ToBase64String(s.Src))));

            // User Mappings
            this.CreateMap<ApplicationUser, ApplicationUserViewModel>();

            // Review Mappings
            this.CreateMap<Review, ReviewViewModel>()
                .ForMember(x => x.CreatorCommentUserName, y => y.MapFrom(s => s.User.FullName));
        }
    }
}
