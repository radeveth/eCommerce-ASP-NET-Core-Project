namespace eCommerceAPI.Data.Common
{
    public static class DataValidation
    {
        public static class ProductValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 10000;
        }

        public static class CategoryValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 3000;
        }

        public static class BrandValidation
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 3000;
        }

        public static class ApplicationUserValidation
        {
            public const int FullNameMaxLength = 100;
            public const int UsernameMaxLength = 30;
        }

        public static class ImageValidation
        {
            public const int NameMaxLength = 50;
        }

        public static class OrderValidation
        {
            public const int CountryMaxLength = 150;
            public const int CityMaxLength = 150;
            public const int AddressMaxLength = 150;
        }

        public static class ApplicationUserAddressValidation
        {
            public const int CountryMaxLength = 150;
            public const int CityMaxLength = 150;
            public const int AddressMaxLength = 150;
        }
    }
}
