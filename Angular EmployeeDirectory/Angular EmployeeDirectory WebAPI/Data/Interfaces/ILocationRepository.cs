using Repository.DataConcerns;

namespace Repository.Interfaces
{
    public interface ILocationRepository
    {
        public List<Location> GetAllLocations();

    }
}
