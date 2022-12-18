using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.DataAccessLayer.Entities;


namespace FilmWebSite.DataAccessLayer.Seeding
{
    public class Seed
    {
        private readonly FilmWebSiteContext _context;
        public Seed(FilmWebSiteContext context)
        {
            _context = context;
        }
        public void SeedDataContext()
        {
            if (!_context.FilmActors.Any())
            {
                var filmActors = new List<FilmActor>()
                {
                    new FilmActor()
                    {
                        Film = new Film()
                        {
                            Name = "Film 1",
                            ReleaseDate = new DateTime(2022,1,1),
                            FilmCategories = new List<FilmCategory>()
                            {
                                new FilmCategory { Category = new Category() { Name = "Kategori 1"}}
                            },
                            Comments = new List<Comment>()
                            {
                                new Comment { Title="Film 1 Yorum 1",Text = "Film 1 Yorum 1", Rating = 5,
                                User = new User(){ Email="user1@gmail.com",Name ="user1@gmail.com",Password="123456" } },

                                new Comment { Title="Film 1 Yorum 2",Text = "Film 1 Yorum 2", Rating = 4,
                                User = new User(){ Email="user2@gmail.com",Name ="user2@gmail.com",Password="123456" } },

                                new Comment { Title="Film 1 Yorum 3",Text = "Film 1 Yorum 3", Rating = 5,
                                User = new User(){ Email="user3@gmail.com",Name ="user3@gmail.com",Password="123456" } },
                            }
                        },
                        Actor = new Actor()
                        {
                            FirstName = "Oyuncu 1 Ad",
                            LastName = "Oyuncu 1 Soyad",
                            ContactMail= "oyuncu1@gmailcom",
                            City = new City()
                            {
                                Name = "İstanbul"
                            }
                        }
                    },
                    new FilmActor()
                    {
                        Film = new Film()
                        {
                            Name = "Film 2",
                            ReleaseDate = new DateTime(2022,1,1),
                            FilmCategories = new List<FilmCategory>()
                            {
                                new FilmCategory { Category = new Category() { Name = "Kategori 2"}}
                            },
                            Comments = new List<Comment>()
                            {
                                new Comment { Title="Film 2 Yorum 1",Text = "Film 2 Yorum 1", Rating = 5,
                                User = new User(){ Email="user4@gmail.com",Name ="user4@gmail.com",Password="123456" } },

                                new Comment { Title="Film 2 Yorum 2",Text = "Film 2 Yorum 2", Rating = 4,
                                User = new User(){ Email="user5@gmail.com",Name ="user5@gmail.com",Password="123456" } },

                                new Comment { Title="Film 2 Yorum 3",Text = "Film 2 Yorum 3", Rating = 3,
                                User = new User(){ Email="user6@gmail.com",Name ="user6@gmail.com",Password="123456" } },
                            }
                        },
                        Actor = new Actor()
                        {
                            FirstName = "Oyuncu 2 Ad",
                            LastName = "Oyuncu 2 Soyad",
                            ContactMail= "oyuncu2@gmailcom",
                            City = new City()
                            {
                                Name = "Ankara"
                            }
                        }
                    },
                    new FilmActor()
                    {
                        Film = new Film()
                        {
                            Name = "Film 3",
                            ReleaseDate = new DateTime(2022,1,1),
                            FilmCategories = new List<FilmCategory>()
                            {
                                new FilmCategory { Category = new Category() { Name = "Kategori 3"}}
                            },
                            Comments = new List<Comment>()
                            {
                                new Comment { Title="Film 3 Yorum 1",Text = "Film 3 Yorum 1", Rating = 5,
                                User = new User(){ Email="user7@gmail.com",Name ="user7@gmail.com",Password="123456" } },

                                new Comment { Title="Film 3 Yorum 2",Text = "Film 3 Yorum 2", Rating = 4,
                                User = new User(){ Email="user8@gmail.com",Name ="user8@gmail.com",Password="123456" } },

                                new Comment { Title="Film 3 Yorum 3",Text = "Film 3 Yorum 3", Rating = 5,
                                User = new User(){ Email="user9@gmail.com",Name ="user9@gmail.com",Password="123456" } },
                            }
                        },
                        Actor = new Actor()
                        {
                            FirstName = "Oyuncu 3 Ad",
                            LastName = "Oyuncu 3 Soyad",
                            ContactMail= "oyuncu3@gmailcom",
                            City = new City()
                            {
                                Name = "İzmir"
                            }
                        }
                    }
                };
                _context.FilmActors.AddRange(filmActors);
                _context.SaveChanges();
            }
        }
    }
}
