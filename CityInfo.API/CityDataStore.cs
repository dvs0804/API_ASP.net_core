using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API
{
    public class CityDataStore
    {
        public static CityDataStore Current { get;} = new CityDataStore();
        public List<CityDto>Cities { get; set; }

        public CityDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York",
                    Descripcion = "Es una ciudad con un gran parque",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            id = 1,
                            name = "Central Park",
                            descripcion = "The people to walk in this park"
                        },
                        new PointOfInterestDto()
                        {
                            id = 2,
                            name = " Empire State Building",
                            descripcion = "Es uno de los edificios historicos de USA"
                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name ="Palmares",
                    Descripcion ="Un pueblo para hacer amigos",
                      PointOfInterest = new List<PointOfInterestDto>()
                      {
                            new PointOfInterestDto()
                            {
                                id = 3,
                                name = "Iglesia Catolica",
                                descripcion = "Iglesia historica"
                            },
                           new PointOfInterestDto()
                           {
                                id = 4,
                                name = "Fiestas Civicas",
                                descripcion = "Todo el pais las visita"
                           }

                      }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "San Ramon",
                    Descripcion = "Tierra de Poetas",
                      PointOfInterest = new List<PointOfInterestDto>()
                    {
                            new PointOfInterestDto()
                            {
                                id = 5,
                                name = "Mall",
                                descripcion = "Ubicado en la entrada del canton"
                            },
                            new PointOfInterestDto()
                            {
                                id = 6,
                                name = " Miradores",
                                descripcion = "multiples miradores por todo el canton"
                            }
                      }
                }
            };
        }
    }
}
