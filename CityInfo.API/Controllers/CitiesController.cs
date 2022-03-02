using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {   
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CityDataStore.Current.Cities);
                
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
           var CityToReturn = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if(CityToReturn == null)
            {
                return NotFound();
            }
            return Ok(CityToReturn);
        }
    }
}
