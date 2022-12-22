using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmWebSite.Core.Repositories;
using FilmWebSite.Core.UnitOfWorks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class CityManager : ICityService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;
        private readonly IActorRepository _actorRepository;
        public CityManager( ICityRepository cityRepository, IActorRepository actorRepository, IUnitOfWork unitOfWork)
        {
            
            _cityRepository = cityRepository;
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
        }

        public bool CityExists(int cityId)
        {
            return _cityRepository.Any(c => c.Id == cityId);
        }

        public bool CreateCity(City city)
        {
            _cityRepository.Add(city);
            return Save();
        }

        public bool DeleteCity(City city)
        {
            _cityRepository.Remove(city);
            return Save();
        }

        public ICollection<City> GetCities()
        {
            return _cityRepository.GetAll().ToList();
        }

        public City GetCity(int cityId)
        {
            return _cityRepository.GetById(cityId);

        }

        public City GetCityByActor(int actorId)
        {
            return _actorRepository.Where(a => a.Id == actorId)
                .Select(a => a.City)
                .FirstOrDefault();

        }

        public bool Save()
        {
           return _unitOfWork.Commit();
        }

        public bool UpdateCity(City city)
        {
            _cityRepository.Update(city);
            return Save();
        }
    }
}
