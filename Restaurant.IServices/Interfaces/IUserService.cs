using Restaurant.Data.Models.UserModels;
using System.Security.Claims;

namespace Restaurant.IServices.Interfaces
{
    public interface IUserService
    {
        public long AddUser(UserCreateRequestDto dto);
        public void DisableUser(long id, List<Claim> userClaims);
        public List<UserListItemDto> GetUsersList();
        public string SignInUser(LoginDto dto);
    }
}
