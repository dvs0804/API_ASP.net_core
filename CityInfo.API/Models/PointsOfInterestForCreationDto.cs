using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointsOfInterestForCreationDto
    {
        [Required(ErrorMessage ="Tienes que escribir un nombre")]
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(200)]
        public string descripcion { get; set; }
    }
}
