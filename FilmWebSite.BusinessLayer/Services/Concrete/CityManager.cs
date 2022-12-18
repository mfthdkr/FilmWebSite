using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class CityManager : ICityService
    {
        private readonly FilmWebSiteContext _context;

        public CityManager(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public bool CreateCity(City city)
        {
            _context.Add(city);
            return Save();
        }

        public bool DeleteCity(City city)
        {
            _context.Remove(city);
            return Save();
        }

        public ICollection<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public City GetCity(int cityId)
        {
            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public City GetCityByActor(int actorId)
        {
            return _context.Actors.Where(a => a.Id == actorId).Select(a => a.City).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCity(City city)
        {
            _context.Update(city);
            return Save();
        }
    }
}
