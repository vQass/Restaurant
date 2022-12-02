﻿using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.DB.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface IUserRepository
    {
        User GetUser(long id);
        User GetUser(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetUsers(List<long> ids);

        long AddUser(UserCreateRequest userCreateRequest);
        void UpdateUser(User user, UserUpdateRequest userUpdateRequest);
        void UpdateUserEmail(User user, string newEmail);
        void DisableUser(User user);

        void EnsureEmailHasValidFormat(string email);
        void EnsureEmailNotTaken(string email, long id = 0);
        void EnsureUserExists(User user);
    }
}