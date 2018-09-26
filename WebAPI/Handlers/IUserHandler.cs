using System.Collections.Generic;
using Common;

namespace WebAPI.Handlers
{
    interface IUserHandler
    {
        User GetUser(int i);
        User GetUser(string username);
        List<User> GetAllUsers();
        void CreateUser(Login user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
