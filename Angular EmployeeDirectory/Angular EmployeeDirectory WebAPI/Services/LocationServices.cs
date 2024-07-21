using Concerns;
using Services.Interfaces;
using Repository.Interfaces;


namespace Services
{
    public class LocationServices : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationServices(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public List<Location> DisplayAll()
        {
            return ConvertToConcernsLocations(_locationRepository.GetAllLocations());
        }

        private List<Location> ConvertToConcernsLocations(List<Repository.DataConcerns.Location> dataLocations)
        {
            List<Location> concernsLocations = new List<Location>();

            foreach (var dataLocation in dataLocations)
            {
                Location concernsLocation = new Location();
                concernsLocation.LocationId = dataLocation.LocationId;
                concernsLocation.LocationName = dataLocation.LocationName;
                concernsLocations.Add(concernsLocation);
            }
            return concernsLocations;
        }

    }
}
