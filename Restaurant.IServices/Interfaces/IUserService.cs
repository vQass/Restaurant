using Restaurant.Data.Models.UserModels;
using System.Security.Claims;

namespace Restaurant.IServices.Interfaces
{
    public interface IUserService
    {
        public long AddUser(UserCreateRequest dto);
        public void DisableUser(long id, List<Claim> userClaims);
        public List<UserListViewModel> GetUsersList();
        public string SignInUser(LoginRequest dto);
        public void UpdateUser(long id, UserUpdateRequest userUpdateRequest);
    }
}
