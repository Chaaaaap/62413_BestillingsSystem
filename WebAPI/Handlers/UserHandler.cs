using System.Collections.Generic;
using Common;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    public class UserHandler
    {
        private MySqlConnection Conn;
        // We need a SQL Connection
        public UserHandler()
        {
            Conn = new MySqlConnection();
        }

        /// <summary>
        /// Gets a single user for login by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {

            return new User();
        }

        /// <summary>
        /// Gets a single user from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            return new User();
        }

        /// <summary>
        /// Gets a list of all users in the system
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            //var sql = "SELECT * FROM users;";
            return new List<User>();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        public void CreateUser()
        {

        }

        /// <summary>
        /// Updates a user with a certain id to match a user
        /// given by the client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        public void UpdateUser(int id, User user)
        {
            //var sql = "";
        }
        
        /// <summary>
        /// Deletes a user with a certain id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int id)
        {

        }
    }
}
