namespace LocalUserServiceProvider.Data.DTOs
{
    public class ProfileResponseRest
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Phone { get; set; }

        public string? Error { get; set; }
    }
}