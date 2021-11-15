using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Post
    {

        public Post()
        {
            ModifedDate = DateTime.Now;
        }
        public Guid Id { get; set; }

        public string UserId { get; set; }     

        public string Content { get; set; }

        public DateTime ModifedDate { get; set; }

        public bool IsSelected { get; set; }
    }
}
