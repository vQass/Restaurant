namespace Restaurant.Data.Models.UserModels
{
    public class UserCreateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public short? CityId { get; set; }
        public string? Address { get; set; }
    }
}
