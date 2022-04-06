using AutoMapper;
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
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            //var results = new List<CityWithoutPointOfInterestDto>();
            //foreach(var CityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointOfInterestDto
            //    {
            //        Id = CityEntity.Id,
            //        Name = CityEntity.Name,
            //        description = CityEntity.descripcion

            //    });
            //} 
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities));
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
                return Ok(_mapper.Map<CityDto>(city));
            //    var cityResult = new CityDto()
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Descripcion = city.descripcion
            //    };

            //    foreach(var poi in city.PointOfInterest)
            //    {
            //        cityResult.PointOfInterest.Add(
            //            new PointOfInterestDto()
            //            {
            //                id = poi.Id,
            //                name = poi.Name,
            //                descripcion = poi.description
            //            });
            //    }
            //    return Ok(cityResult); 
            }
            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
            //var CityWithoutPointOfInteresResult = new CityWithoutPointOfInterestDto()
            //{
            //    Id= city.Id,
            //    Name= city.Name,
            //    description = city.descripcion
            //};
            //return Ok(CityWithoutPointOfInteresResult);
        }
    }
}
