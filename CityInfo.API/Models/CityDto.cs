using System.Collections.Generic;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointOfInterest.Count;
            }
        }
        public ICollection<PointOfInterestDto> PointOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}
