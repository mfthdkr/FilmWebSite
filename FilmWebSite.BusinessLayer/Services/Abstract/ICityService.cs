using FilmWebSite.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Abstract
{
    public interface ICityService
    {
        ICollection<City> GetCities();
        City GetCity(int cityId);
        City GetCityByActor(int actorId);
        bool CityExists(int cityId);    
        bool CreateCity(City city);
        bool UpdateCity(City city);
        bool DeleteCity(City city);
        bool Save();
    }
}
