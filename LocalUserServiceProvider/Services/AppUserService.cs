using LocalUserServiceProvider.Data.DTOs;
using LocalUserServiceProvider.Data.Models;
using System.Text.Json;

namespace LocalUserServiceProvider.Services
{
    public class AppUserService(HttpClient httpClient) : IAppUserService
    {
        private readonly string _accountClientUrl = "https://ventixe-account-provider.azurewebsites.net/api/account";
        private readonly string _profileClientUrl = "https://ventixe-profile-provider.azurewebsites.net/api/profile";

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