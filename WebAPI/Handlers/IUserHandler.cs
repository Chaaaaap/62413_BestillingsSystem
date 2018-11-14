using System.Collections.Generic;
using Common;

namespace WebAPI.Handlers
{
    interface IUserHandler
    {
        User GetUser(long i);
        User GetUserForLogin(string username);
        List<User> GetAllUsers();
        void CreateUser(User user);
        void UpdateUser(long id, User user);
        void DeleteUser(long id);
    }
}
