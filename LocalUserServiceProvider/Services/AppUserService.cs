using LocalAccountServiceProvider.Models;

namespace LocalUserServiceProvider.Services
{
    public class AppUserService(ProfileContract.ProfileContractClient profileClient,
                                AccountContract.AccountContractClient accountClient) : IAppUserService
    {
        private readonly ProfileContract.ProfileContractClient _profileClient = profileClient;
        private readonly AccountContract.AccountContractClient _accountClient = accountClient;

        public async Task<AppUser> GetAppUserById(string id)
        {
            var profileRequest = new GetProfileRequest { Id = id };
            var userProfile = await _profileClient.GetProfileAsync(profileRequest);

            var accountRequest = new GetAccountRequest { Id = id };
            var userAccount = await _accountClient.GetAccountAsync(accountRequest);

            return new AppUser
            {
                Id = id,
                Email = userAccount.Email,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Image = userProfile.ProfilePictureUrl,
                Role = userAccount.Role,
                StreetAddress = userProfile.StreetAddress,
                PostalCode = userProfile.ZipCode,
                City = userProfile.City,
                Phone = userProfile.Phone,
            };
        }

        public async Task<IEnumerable<AppUser>> GetAllAppUsers()
        {
            var profilesRequest = new GetAllProfilesRequest();
            var userProfiles = await _profileClient.GetAllProfilesAsync(profilesRequest);

            var accountsRequest = new GetAllAccountsRequest();
            var userAccounts = await _accountClient.GetAllAccountsAsync(accountsRequest);

            var appUsers = userProfiles.Profiles.Select(profile =>
            {
                var userAccount = userAccounts.Accounts.FirstOrDefault(user => user.Id == profile.Id);

                return new AppUser
                {
                    Id = userAccount.Id,
                    Email = userAccount.Email,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Image = profile.ProfilePictureUrl,
                    Role = userAccount.Role,
                    StreetAddress = profile.StreetAddress,
                    PostalCode = profile.ZipCode,
                    City = profile.City,
                    Phone = profile.Phone,
                };
            }
            );

            return appUsers;
        }
    }
}