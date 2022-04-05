using CityInfo.API.Models;
using CityInfo.API.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var results = new List<CityWithoutPointOfInterestDto>();
            foreach(var CityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointOfInterestDto
                {
                    Id = CityEntity.Id,
                    Name = CityEntity.Name,
                    description = CityEntity.descripcion

                });
            } 
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointOfInterest);
            if (city == null)
            {
                return NotFound();
            }
            if (includePointOfInterest)
            {
                var cityResult = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Descripcion = city.descripcion
                };

                foreach(var poi in city.PointOfInterest)
                {
                    cityResult.PointOfInterest.Add(
                        new PointOfInterestDto()
                        {
                            id = poi.Id,
                            name = poi.Name,
                            descripcion = poi.description
                        });
                }
                return Ok(cityResult); 
            }
            var CityWithoutPointOfInteresResult = new CityWithoutPointOfInterestDto()
            {
                Id= city.Id,
                Name= city.Name,
                description = city.descripcion
            };
            return Ok(CityWithoutPointOfInteresResult);
        }
    }
}
