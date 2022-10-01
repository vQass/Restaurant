using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.DB.Entities;
using System.Security.Claims;

namespace Restaurant.IRepository
{
    public interface IUserRepository
    {
        User GetUser(long id);
        User GetUser(string email);
        Task<IEnumerable<User>> GetUsers();

        long AddUser(UserCreateRequest userCreateRequest);
        void UpdateUser(User user, UserUpdateRequest userUpdateRequest);
        void UpdateUserEmail(User user, string newEmail);
        void DisableUser(User user);

        void EnsureEmailHasValidFormat(string email);
        void EnsureEmailNotTaken(string email, long id = 0);
        void EnsureUserExists(User user);
    }
}