using Restaurant.Data.Models.UserModels;
using System.Security.Claims;

namespace Restaurant.IServices
{
    public interface IUserService
    {
        public long AddUser(UserCreateRequest dto);
        public void DisableUser(long id, List<Claim> userClaims);
        public IEnumerable<UserListViewModel> GetUsersList();
        public UserWithDetailsViewModel GetUserById(long id);
        public string SignInUser(LoginRequest dto);
        public void UpdateUser(long id, UserUpdateRequest userUpdateRequest);
        public void UpdateUserEmail(long id, string newEmail);
    }
}
