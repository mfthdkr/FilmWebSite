using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.Core.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactMail { get; set; }
        public City City { get; set; }
        public ICollection<FilmActor> FilmActors { get; set; }
    }
}
