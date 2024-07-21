using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Concerns;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController (IProjectService projectService)
        {
            _projectService = projectService;
        }
        // GET: api/<ProjectsController>
        [HttpGet]
        public List<Project> GetProjects()
        {
            return _projectService.DisplayProjects();
        }

        [HttpGet("GetProjectsByDepartment/{departmentId}")]
        public List<Project> GetFilteredProjects(int departmentId)
        {
            return _projectService.FilteredProjects(departmentId);
        }

        // GET api/<ProjectsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProjectsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ProjectsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProjectsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
