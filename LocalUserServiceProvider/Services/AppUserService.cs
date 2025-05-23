using LocalUserServiceProvider.Data.DTOs;
using LocalUserServiceProvider.Data.Models;
using System.Net.Http;
using System.Text.Json;

namespace LocalUserServiceProvider.Services
{
    public class AppUserService(ProfileContract.ProfileContractClient profileClient,
                                AccountContract.AccountContractClient accountClient, HttpClient httpClient) : IAppUserService
    {
        private readonly ProfileContract.ProfileContractClient _profileClient = profileClient;
        private readonly AccountContract.AccountContractClient _accountClient = accountClient;

        private readonly string _accountClientUrl = "https://localhost:7166/api/account";
        private readonly string _profileClientUrl = "https://localhost:7275/api/profile";

        private readonly HttpClient _httpClient = httpClient;

        public async Task<AppUserResponseRest> GetAppUserById(string id)
        {
            var userProfile = await GetRequest<ProfileResponseRest>($"{_profileClientUrl}/get-profile/{id}");

            var userAccount = await GetRequest<AccountResponseRest>($"{_accountClientUrl}/get-account/{id}");

            return new AppUserResponseRest
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

        //public async Task<AppUser> GetAppUserById(string id)
        //{
        //    var profileRequest = new GetProfileRequest { Id = id };
        //    var userProfile = await _profileClient.GetProfileAsync(profileRequest);

        //    var accountRequest = new GetAccountRequest { Id = id };
        //    var userAccount = await _accountClient.GetAccountAsync(accountRequest);

        //    return new AppUser
        //    {
        //        Id = id,
        //        Email = userAccount.Email,
        //        FirstName = userProfile.FirstName,
        //        LastName = userProfile.LastName,
        //        Image = userProfile.ProfilePictureUrl,
        //        Role = userAccount.Role,
        //        StreetAddress = userProfile.StreetAddress,
        //        PostalCode = userProfile.ZipCode,
        //        City = userProfile.City,
        //        Phone = userProfile.Phone,
        //    };
        //}

        //public async Task<IEnumerable<AppUser>> GetAllAppUsers()
        //{
        //    var profilesRequest = new GetAllProfilesRequest();
        //    var userProfiles = await _profileClient.GetAllProfilesAsync(profilesRequest);

        //    var accountsRequest = new GetAllAccountsRequest();
        //    var userAccounts = await _accountClient.GetAllAccountsAsync(accountsRequest);

        //    var appUsers = userProfiles.Profiles.Select(profile =>
        //    {
        //        var userAccount = userAccounts.Accounts.FirstOrDefault(user => user.Id == profile.Id);

        //        return new AppUser
        //        {
        //            Id = userAccount.Id,
        //            Email = userAccount.Email,
        //            FirstName = profile.FirstName,
        //            LastName = profile.LastName,
        //            Image = profile.ProfilePictureUrl,
        //            Role = userAccount.Role,
        //            StreetAddress = profile.StreetAddress,
        //            PostalCode = profile.ZipCode,
        //            City = profile.City,
        //            Phone = profile.Phone,
        //        };
        //    }
        //    );

        //    return appUsers;
        //}

        public async Task<T?> GetRequest<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return default;

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(responseString, options);
        }
    }
}