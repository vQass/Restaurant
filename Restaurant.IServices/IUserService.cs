using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Data.Models.UserModels.Responses;
using Restaurant.Data.Models.UserModels.ViewModels;
using System.Security.Claims;

namespace Restaurant.IServices
{
    public interface IUserService
    {
        public long AddUser(UserCreateRequest dto);
        public void DisableUser(long id, List<Claim> userClaims);
        public Task<IEnumerable<UserListViewModel>> GetUsers();
        public UserWithDetailsViewModel GetUser(long id);
        public LoginResponse SignInUser(LoginRequest dto);
        public void UpdateUser(long id, UserUpdateRequest userUpdateRequest);
        public void UpdateUserEmail(long id, string newEmail);
    }
}
