namespace Ecommerce.ViewModels.Admin
{
    public class UsersStatistsicsServiceModel
    {
        public int UsersCount { get; set; }

        public int AdminsCount { get; set; }

        public IEnumerable<UserStatistsicsViewModel> Users { get; set; }

        public IEnumerable<AdminStatisticsViewModel> Admins { get; set; }
    }
}
