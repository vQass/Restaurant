using Restaurant.Data.Models.UserModels;


namespace Restaurant.IServices.Interfaces
{
    public interface IUserService
    {
        public long AddUser(UserCreateRequestDto dto);
    }
}
