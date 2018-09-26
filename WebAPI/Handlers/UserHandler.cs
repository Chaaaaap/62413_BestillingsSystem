using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace WebAPI.Handlers
{
    public class UserHandler : IDisposable, IUserHandler
    {
        private readonly SqlConnection _conn;

        // We need a SQL Connection
        public UserHandler()
        {
            var conString = AppSettings.ConnectionString;
            _conn = new SqlConnection(conString);

        }

        /// <summary>
        /// Gets a single user for login by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserForLogin(string username)
        {
            _conn.Open();
            var sql = "SELECT * FROM dbo.[user] where Username = '" + username + "';";
            var cmd = _conn.CreateCommand();
            cmd.CommandText = sql;

            //cmd.Parameters.Add("@Username", SqlDbType.Text);
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
        public User GetUser(int id)
        {
            _conn.Open();
            var sql = "SELECT * FROM dbo.[user] where Id = @Id;"; // + id + ";";
            var cmd = _conn.CreateCommand();
            cmd.CommandText = sql;

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
            const string sql = "SELECT * FROM dbo.[user];";
            var cmd = _conn.CreateCommand();
            cmd.CommandText = sql;

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
            var sql = String.Format("INSERT INTO dbo.[user] (Username, Password) VALUES('{0}', '{1}');", user.Username, user.Password);

            var cmd = _conn.CreateCommand();
            cmd.CommandText = sql;

            //cmd.Parameters.AddWithValue("@Username", user.Username);
            //cmd.Parameters.AddWithValue("@Password", user.Password);
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
        public void UpdateUser(int id, User user)
        {
            _conn.Open();
            //var sql = "";

            _conn.Close();
        }
        
        /// <summary>
        /// Deletes a user with a certain id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int id)
        {
            _conn.Open();
            var sql = "DELETE FROM dbo.[user] WHERE Id = " + id + ";";
            var cmd = new SqlCommand(sql, _conn);

            //cmd.Parameters.Add("@Id", SqlDbType.BigInt);
            //cmd.Parameters["@Id"].Value = id;

            cmd.ExecuteNonQuery();

            _conn.Close();
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
