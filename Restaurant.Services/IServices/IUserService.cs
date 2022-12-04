using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Data.Models.UserModels.Responses;

namespace Restaurant.Business.IServices
{
    public interface IUserService
    {
        public void AddUser(UserCreateRequest dto);
        public LoginResponse SignInUser(LoginRequest dto);
    }
}
