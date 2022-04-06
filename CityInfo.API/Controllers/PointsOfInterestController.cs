using AutoMapper;
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
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

                //var pointOfInterestForCityResult = new List<PointOfInterestDto>();
                //foreach (var poi in pointOfInterestForCity)
                //{
                //    pointOfInterestForCityResult.Add(new PointOfInterestDto()
                //    {
                //        id = poi.Id,
                //        name = poi.Name,
                //        descripcion = poi.description
                //    });
                //}
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestForCity));
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
            //if(PointOfInterest == null)
            //{
            //    return NotFound();
            //}
            //var pointOfInterestResult = new PointOfInterestDto()
            //{
            //    id = PointOfInterest.Id,
            //    name=PointOfInterest.Name,
            //    descripcion=PointOfInterest.description 
            //};
            return Ok(_mapper.Map<PointOfInterestDto>(PointOfInterest));
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
            
            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }



            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointofinterest);
           

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);
            _cityInfoRepository.save();
            var CreatePointOfInterestToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = CreatePointOfInterestToReturn.id }, CreatePointOfInterestToReturn);

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

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var PoitOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);   
            if(PoitOfInterestEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(pointofinterest, PoitOfInterestEntity);
            _cityInfoRepository.save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult ParciallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchdoc)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var PoitOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (PoitOfInterestEntity == null)
            {
                return NotFound();
            }
            var PointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(PoitOfInterestEntity); 
           
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
            _mapper.Map(PointOfInterestToPatch, PoitOfInterestEntity);
            _cityInfoRepository.save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DetelePointOfInterest(int cityId,int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var PoitOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (PoitOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DetelePointOfInterestForCity(PoitOfInterestEntity);

            _cityInfoRepository.save();

            return NoContent();
        }
    }
}
