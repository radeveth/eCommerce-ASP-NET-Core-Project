namespace Ecommerce.ViewModels.Products
{
    using System.Collections.Generic;
    using Ecommerce.Data.Models.Enums;
    using Ecommerce.ViewModels.Images;
    using Ecommerce.ViewModels.Review;
    using Ganss.Xss;

    public class ProductDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string BrandName { get; set; }

        public IEnumerable<ImageViewModel> Images { get; set; }

        public ProductReviewServiceModel ProductReviewServiceModel { get; set; }

        public IEnumerable<ProductViewModel> RelatedProducts { get; set; }
    }
}
