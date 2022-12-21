namespace Ecommerce.ViewModels.Admin
{
    using Microsoft.AspNetCore.Identity;

    public class AdminStatisticsViewModel : UserStatistsicsViewModel
    {
        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
