namespace eCommerce.Services.Data.ApplicationUsersServices
{
    using eCommerce.InputModels.ApplicationUsers;
    using eCommerce.ViewModels.ApplicationUsers;

    public interface IApplicationUserService
    {
        public Task<ApplicationUserViewModel> GetById(string id);

        public IEnumerable<ApplicationUserViewModel> GetAll();

        public Task CreateAsync(ApplicationUserFromModel userForm);

        public string Authorization(ApplicationUserCred userCred);
    }
}
