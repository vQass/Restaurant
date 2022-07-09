using Restaurant.Data.Models.UserModels;


namespace Restaurant.Services.Interfaces
{
    public interface IUserService
    {
        public void AddUser(UserCreateRequestDto dto);
    }
}
