using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Entities.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface IUserRepository
    {
        User GetUser(string email);
        Task<IEnumerable<User>> GetUsers(List<long> ids);

        long AddUser(UserCreateRequest userCreateRequest);

        void EnsureEmailHasValidFormat(string email);
        void EnsureEmailNotTaken(string email, long id = 0);
        void EnsureUserExists(User user);
    }
}