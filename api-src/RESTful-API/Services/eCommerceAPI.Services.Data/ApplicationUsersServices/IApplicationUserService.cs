namespace eCommerceAPI.Services.Data.ApplicationUsersServices
{
    using eCommerceAPI.InputModels.ApplicationUsers;
    using eCommerceAPI.ViewModels.ApplicationUsers;

    public interface IApplicationUserService
    {
        public Task<ApplicationUserViewModel> GetById(string id);

        public IEnumerable<ApplicationUserViewModel> GetAll();

        public Task CreateAsync(ApplicationUserFromModel userForm);

        public string Authorization(ApplicationUserCred userCred);
    }
}
