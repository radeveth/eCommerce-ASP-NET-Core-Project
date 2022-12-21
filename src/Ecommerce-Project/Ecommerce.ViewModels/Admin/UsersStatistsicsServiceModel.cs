namespace Ecommerce.ViewModels.Admin
{
    public class UsersStatistsicsServiceModel
    {
        public int UsersCount => this.Users.Count();

        public int AdminsCount => this.Admins.Count();

        public IEnumerable<UserStatistsicsViewModel> Users { get; set; }

        public IEnumerable<AdminStatisticsViewModel> Admins { get; set; }
    }
}
