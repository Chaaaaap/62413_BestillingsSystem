using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;
using MySql.Data.MySqlClient;

namespace WebAPI.Handlers
{
    public class UserHandler : IDisposable, IUserHandler
    {
        private readonly MySqlConnection _conn;

        // We need a SQL Connection
        public UserHandler()
        {
            var conString = AppSettings.ConnectionString;
            _conn = new MySqlConnection(conString);

        }

        /// <summary>
        /// Gets a single user for login by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserForLogin(string username)
        {

            _conn.Open();
            
            var sql = "SELECT * FROM Users where Username = '@Username'";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.Add(new MySqlParameter("@Username", username));
            //cmd.Parameters["@Username"].Value = username;

            var dataReader = cmd.ExecuteReader();

            User user = null;



            while (dataReader.Read())
            {
                user = new User
                    {
                       Id = Convert.ToInt64(dataReader["Id"].ToString()),
                       Username = dataReader["Username"].ToString(),
                       Password = dataReader["Password"].ToString()
                    };
            }
            _conn.Close();
            return user;
        }

        /// <summary>
        /// Gets a single user from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(long id)
        {
            _conn.Open();
            var sql = "SELECT * FROM Users where Id = @Id;"; // + id + ";";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.Add("@Id", SqlDbType.BigInt);
            cmd.Parameters["@Id"].Value = id;
            User user = null;
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                user = new User
                {
                    Id = Convert.ToInt64(dataReader["Id"].ToString()),
                    Username = dataReader["Username"].ToString(),
                    Password = dataReader["Password"].ToString()
                };
            }
            _conn.Close();
            return user;
        }

        /// <summary>
        /// Gets a list of all users in the system
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            _conn.Open();
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
                    Password = dataReader["Password"].ToString()
                };
                userList.Add(user);
            }
            _conn.Close();
            return userList;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        public void CreateUser(Login user)
        {
            _conn.Open();
            var sql = "INSERT INTO Users (Username, Password) VALUES(@Username, @Password);";

            var cmd = new MySqlCommand(sql, _conn); ;

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            //cmd.Parameters["@Username"].Value = user.Username;
            //cmd.Parameters["@Password"].Value = user.Password;

            cmd.ExecuteNonQuery();

            _conn.Close();
        }

        /// <summary>
        /// Updates a user with a certain id to match a user
        /// given by the client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        public void UpdateUser(long id, User user)
        {
            _conn.Open();
            var sql = "UPDATE Users SET Username = @Username, Password = @Password where Id = @Id;"; // + id + ";";

            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.Add("@Id", SqlDbType.BigInt);
            cmd.Parameters.Add("@Username", SqlDbType.VarChar);
            cmd.Parameters.Add("@Password", SqlDbType.VarChar);
            cmd.Parameters["@Id"].Value = id;
            cmd.Parameters["@Username"].Value = user.Username;
            cmd.Parameters["@Password"].Value = user.Password;


            cmd.ExecuteNonQuery();

            _conn.Close();
        }
        
        /// <summary>
        /// Deletes a user with a certain id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(long id)
        {
            _conn.Open();
            var sql = "DELETE FROM Users WHERE Id = @Id;";
            var cmd = new MySqlCommand(sql, _conn);

            cmd.Parameters.Add("@Id", SqlDbType.BigInt);
            cmd.Parameters["@Id"].Value = id;

            cmd.ExecuteNonQuery();

            _conn.Close();
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
