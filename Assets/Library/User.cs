using System;

namespace Library
{
    [Serializable]
    public class User
    {
        public string username;

        public User(string username)
        {
            this.username = username;
        }
    }
}