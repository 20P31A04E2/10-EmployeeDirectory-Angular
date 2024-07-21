using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Concerns;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        
        // GET: api/<DepartmentsController>
        [HttpGet]
        public List<Department> GetDepartments()
        {
            return _departmentService.GetAllDepartments();
        }

        [HttpGet("GetDepartmentsByRole/{roleId}")]
        public List<Department> GetFilteredDepartments(int roleId)
        { 
            return _departmentService.FilteredDepartments(roleId);
        }
    }
}
