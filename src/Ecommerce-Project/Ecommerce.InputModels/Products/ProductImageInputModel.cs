namespace Ecommerce.InputModels.Products
{
    using Microsoft.AspNetCore.Http;

    public class ProductImageInputModel
    {
        public IFormFile Src { get; set; }
    }
}