using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.DataAccessLayer.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FilmActor> FilmActors { get; set; }
        public ICollection<FilmCategory> FilmCategories { get; set; }
    }
}
