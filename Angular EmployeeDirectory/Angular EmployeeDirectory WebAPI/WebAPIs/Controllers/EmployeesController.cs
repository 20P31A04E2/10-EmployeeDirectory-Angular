using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Concerns;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics.Metrics;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public List<Employee> GetAllEmployees()
        {
            return _employeeService.ViewAllEmployees();
        }

        //GET api/<EmployeesController>/a

        // Multiselect filtered employees
        [HttpPost("FilteringAndSorting")]
        public IActionResult GetFilteredAndSortedEmployees([FromBody] MultiselectFilter filter)
        {
            var filteredEmployees = _employeeService.FilteredAndSortedEmployees(filter.FilterButtonText,filter.Status, filter.Locations, filter.Departments, filter.SortBy, filter.SortOrder);
            return Ok(filteredEmployees);
        }

        [HttpGet("GetEmployeesByDepartment/{departmentId}")]
        public List<Employee> GetEmployeeByDepartment(int departmentId)
        {
            return _employeeService.EmployeesByDepartment(departmentId);
        }

        [HttpGet("{employeeId}")]
        public Employee GetEmployeeById (string employeeId)
        {
            return _employeeService.EmployeeById(employeeId);
        }

        [HttpGet("role/{rolesId}")]
        public List<Employee> GetEmployeeByRolesId( int rolesId)
        {
            return _employeeService.EmployeeByRolesId(rolesId);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            try
            {
                _employeeService.AddEmployee(emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //// PUT api/<EmployeesController>/5
        [HttpPut("{EmployeeId}")]
        public void Put([FromRoute] string EmployeeId, Employee emp)
        {
            _employeeService.EditEmployee(EmployeeId, emp);
        }

        //// DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            _employeeService.DeleteEmployee(id);
        }
    }
}
