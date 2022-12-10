namespace Ecommerce.ViewModels.ProductWishlists
{
    using Ecommerce.ViewModels.Images;

    public class ProductWishlistViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public ImageViewModel ProductImage { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
