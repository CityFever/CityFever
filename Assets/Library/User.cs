using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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