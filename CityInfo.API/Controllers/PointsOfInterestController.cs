using CityInfo.API.Models;
using CityInfo.API.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointofinterest")]
    public class PointsOfInterestController:ControllerBase
    {
        private ILogger<PointsOfInterestController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,ICityInfoRepository cityInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetPointOfInterest(int cityId)
        {
            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id{cityId} doesn`t found when " +
                        $"accesing point of interest");
                    return NotFound();
                }
               var pointOfInterestForCity = _cityInfoRepository.GetPointOfInterestForCity(cityId);

                var pointOfInterestForCityResult = new List<PointOfInterestDto>();
                foreach (var poi in pointOfInterestForCity)
                {
                    pointOfInterestForCityResult.Add(new PointOfInterestDto()
                    {
                        id = poi.Id,
                        name = poi.Name,
                        descripcion = poi.description
                    });
                }
                return Ok(pointOfInterestForCityResult);
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"exception while getting point of interest for city with id {cityId}",ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
        }

        [HttpGet("{id}",Name ="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var PointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if(PointOfInterest == null)
            {
                return NotFound();
            }
            var pointOfInterestResult = new PointOfInterestDto()
            {
                id = PointOfInterest.Id,
                name=PointOfInterest.Name,
                descripcion=PointOfInterest.description 
            };
            return Ok(pointOfInterestResult);
        }
        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId,[FromBody] PointsOfInterestForCreationDto pointofinterest )
        {
            if(pointofinterest.descripcion == pointofinterest.name)
            {
                ModelState.AddModelError("descripcion",
                    "tienes que poner una descripcion diferente al nombre");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var MaxPointOfInterestId = CityDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                id = ++MaxPointOfInterestId,
                name = pointofinterest.name,
                descripcion = pointofinterest.descripcion
            };

            city.PointOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = finalPointOfInterest.id }, finalPointOfInterest);

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointofinterest)
        {
            if (pointofinterest.descripcion == pointofinterest.name)
            {
                ModelState.AddModelError("descripcion",
                    "tienes que poner una descripcion diferente al nombre");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(p => p.id == id);
            if(pointofinterest == null)
            {
                return NotFound();
            }
            pointOfInterestFromStore.name = pointofinterest.name;
            pointOfInterestFromStore.descripcion = pointofinterest.descripcion;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult ParciallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchdoc)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(c => c.id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var PointOfInterestToPatch = new PointOfInterestForUpdateDto() 
            { 
                name = pointOfInterestFromStore.name,
                descripcion = pointOfInterestFromStore.descripcion
            };
            patchdoc.ApplyTo(PointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(PointOfInterestToPatch.descripcion == PointOfInterestToPatch.name)
            {
                ModelState.AddModelError("descripcion",
                    "tienes que poner una descripcion diferente al nombre");
            }
            if (!TryValidateModel(PointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            pointOfInterestFromStore.name = PointOfInterestToPatch.name;
            pointOfInterestFromStore.descripcion = PointOfInterestToPatch.descripcion;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DetelePointOfInterest(int cityId,int id)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(p => p.id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(pointOfInterestFromStore);

            return NoContent();
        }
    }
}
