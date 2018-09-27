using System;

namespace Common
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LatestLogin { get; set; }
        public string Token { get; set; }
        public User() { }

        public User(long id, string username, string password, DateTime? latestLogin = null)
        {
            Id = id;
            Username = username;
            Password = password;
            LatestLogin = latestLogin;
        }
    }
}
