using LocalUserServiceProvider.Data.Models;

namespace LocalUserServiceProvider.Services
{
    public interface IAppUserService
    {
        //Task<IEnumerable<AppUser>> GetAllAppUsers();

        Task<AppUserResponseRest> GetAppUserById(string id);
    }
}