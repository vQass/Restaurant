namespace Restaurant.API.Models.UserModels
{
    public class UserCreateRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public short CityId { get; set; }
        public string Address { get; set; }
    }
}
