namespace Restaurant.Data.Models.UserModels.Responses
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }
        public string Role { get; set; }
        public long Id { get; set; }
    }
}
