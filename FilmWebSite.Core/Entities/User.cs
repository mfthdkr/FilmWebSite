using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.Core.Entities
{
    public class User
    {
        public User()
        {
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public  ICollection<Comment> Comments { get; set; }
    }
}
