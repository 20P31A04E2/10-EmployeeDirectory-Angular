using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Concerns;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        } 

        // GET: api/<RolesController>
        [HttpGet]
        public List<Role> Get()
        {
            return _roleService.DisplayAll();
        }

        // GET api/<RolesController>/5
        [HttpGet("GetRolesByLocation/{locationId}")]
        public List<Role> Get(int locationId)
        {
            return _roleService.DisplayRole(locationId);
        }

        [HttpPost("RolesFiltering")]
        public IActionResult GetFilteredRoles([FromBody] RolesMultiSelectFilter filter)
        {
            var filteredRoles = _roleService.FilteredRoles( filter.Locations, filter.Departments);
            return Ok(filteredRoles);
        }

        // POST api/<RolesController>
        [HttpPost]
        public IActionResult Post([FromBody] Role role)
        {
            _roleService.AddRole(role);
            return Ok();
        }

        //// PUT api/<RolesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id,Role role)
        //{
        //    _roleService.EditingARole(id, role);
        //}

        //// DELETE api/<RolesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _roleService.DeleteRole(id);
        //}
    }
}
