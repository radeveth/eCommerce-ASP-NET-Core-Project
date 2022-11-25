﻿namespace eCommerce.ViewModels.Categories
{
    using eCommerce.ViewModels.Images;

    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public ImageViewModel Image { get; set; }
    }
}