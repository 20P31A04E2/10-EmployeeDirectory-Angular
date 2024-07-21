using Repository.DataConcerns;
using Repository.Interfaces;

namespace Repository
{
    public  class LocationsRepository: ILocationRepository
    {
        private readonly EmployeeDBContext _context;
        public LocationsRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        public List<Location> GetAllLocations()
        {
            return _context.Locations.ToList(); 
        }
    }
}
