using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Concerns;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: api/<LocationsController>
        [HttpGet]
        public List<Location> Get()
        {
            return _locationService.DisplayAll();
        }

    }
}
