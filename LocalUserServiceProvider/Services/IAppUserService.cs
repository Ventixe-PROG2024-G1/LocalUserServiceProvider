using LocalAccountServiceProvider.Models;

namespace LocalUserServiceProvider.Services
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUser>> GetAllAppUsers();
        Task<AppUser> GetAppUserById(string id);
    }
}