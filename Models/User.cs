using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class User
    {
        //public int Id { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

       // public List<Post> Posts { get; set; }
    }

    public class LoginUser
    {
        //public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        // public List<Post> Posts { get; set; }
    }
}
