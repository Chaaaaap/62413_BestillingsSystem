using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using Common;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    public class UserHandler : IDisposable, IUserHandler
    {
        private readonly MySqlConnection _conn;

        // We need a SQL Connection
        public UserHandler(MySqlConnection conn = null)
        {
            _conn = conn ?? new MySqlConnection(AppSettings.ConnectionString);
            _conn.Open();

        }

        /// <summary>
        /// Gets a single user for login by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserForLogin(string username)
        {

            var sql = "SELECT * FROM Users where Username=@Username";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Username", username);

            var dataReader = cmd.ExecuteReader();

            User user = null;
            

            while (dataReader.Read())
            {
                user = new User
                    {
                       Id = Convert.ToInt64(dataReader["Id"].ToString()),
                       Username = dataReader["Username"].ToString(),
                       Password = dataReader["Password"].ToString(),
                       IsAdmin = Convert.ToBoolean(dataReader["isAdmin"].ToString())
                    };
            }
            dataReader.Close();
            return user;
        }

        /// <summary>
        /// Gets a single user from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(long id)
        {
            var sql = "SELECT * FROM Users where Id = @Id;"; // + id + ";";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            User user = null;
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                user = new User
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Username = dataReader["Username"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    LatestLogin = Convert.ToDateTime(dataReader["LatestLogin"].ToString()),
                    Email = dataReader["Email"].ToString(),
                    IsAdmin = Convert.ToBoolean(dataReader["isAdmin"].ToString())
                };
            }
            return user;
        }

        /// <summary>
        /// Gets a list of all users in the system
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            const string sql = "SELECT * FROM Users;";
            var cmd = new MySqlCommand(sql, _conn);

            var userList = new List<User>();
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var user = new User
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Username = dataReader["Username"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    LatestLogin = null, //Convert.ToDateTime(dataReader["LatestLogin"].ToString()),
                    Email = dataReader["Email"].ToString(),
                    IsAdmin = Convert.ToBoolean(dataReader["isAdmin"].ToString())
                };
                userList.Add(user);
            }
            return userList;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        public void CreateUser(User user)
        {
            var sql = "INSERT INTO Users (Username, Password, salt, Email, isAdmin) VALUES(@Username, @Password, @Salt, @Email, @isAdmin);";

            var cmd = new MySqlCommand(sql, _conn); ;

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Salt", user.Salt);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates a user with a certain id to match a user
        /// given by the client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        public void UpdateUser(long id, User user)
        {
            var sql = "UPDATE Users SET Username = @Username, Password = @Password, Email = @Email, LatestLogin = @LatestLogin, isAdmin = @idAdmin where Id = @Id;"; // + id + ";";

            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@LatestLogin", user.LatestLogin);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Deletes a user with a certain id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(long id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id;";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
