namespace Ecommerce.ViewModels.Admin
{
    public class UserStatistsicsViewModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public int OrdersCount { get; set; }

        public int ReviewsCount { get; set; }

        public bool IsDeleted { get; set; }
    }
}