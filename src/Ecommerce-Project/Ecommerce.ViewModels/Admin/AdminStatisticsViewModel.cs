namespace Ecommerce.ViewModels.Admin
{
    using Microsoft.AspNetCore.Identity;

    public class AdminStatisticsViewModel : UserStatistsicsViewModel
    {
        public IEnumerable<AdminRoleViewModel> Roles { get; set; }
    }

    public class AdminRoleViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
