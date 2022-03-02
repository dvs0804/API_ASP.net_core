using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointofinterest")]
    public class PointsOfInterest:ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointOfInterest(int cityId)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        }

        [HttpGet("{id}")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterest.FirstOrDefault(c => c.id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }
    }
}
