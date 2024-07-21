using Concerns;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{

    public class EmployeeServices : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Adding employee starts here
        public void AddEmployee(Employee emp)
        {
            if (_employeeRepository.EmployeeExists(emp.EmployeeId))
            {
                throw new Exception("EmployeeId already exists.");
            }

            _employeeRepository.AddingEmployee(ConvertToRepositoryEmployee(emp));
        }
        // Adding employee ends here

        // View all employees starts here
        public List<Employee> ViewAllEmployees()
        {
            return ConvertToConcernsEmployee(_employeeRepository.DisplayEmployees());
        }
        // View all employees ends here

        // Multiselect filtering starts here
        public List<Employee> FilteredAndSortedEmployees(string FilterButtonText, string[] status, string[] locations, string[] departments, string sortBy, string sortOrder)
        {
            return ConvertToConcernsEmployee(_employeeRepository.FilteringAndSorting(FilterButtonText, status, locations, departments, sortBy, sortOrder));
        }

        public List<Employee> EmployeesByDepartment(int departmentId)
        {
            return ConvertToConcernsEmployee(_employeeRepository.EmployeesByDeptId(departmentId));
        }

        public Employee EmployeeById(string employeeId)
        {
            return ConvertToConcernsEmployeeSingle(_employeeRepository.EmployeeBasedOnId(employeeId));
        }

        public List<Employee> EmployeeByRolesId(int rolesId)
        {
            return ConvertToConcernsEmployee(_employeeRepository.EmployeeByRole(rolesId));
        }

        // Edit employee details starts here
        public void EditEmployee(string employeeToEdit, Employee emp)
        {
            _employeeRepository.UpdatingAnEmployee(employeeToEdit, ConvertToRepositoryEmployee(emp));
        }
        // Edit employee details ends here

        // Delete an Employee starts here
        public void DeleteEmployee(string employeeIdToDelete)
        {
            _employeeRepository.DeletingAnEmployee(employeeIdToDelete);
        }
        // Delete an employee ends here


        // Converting from Concerns.Employee to Repository.DataConcerns.Employee
        private static Repository.DataConcerns.Employee ConvertToRepositoryEmployee(Employee concernsEmployee)
        {
            Repository.DataConcerns.Employee RepositoryEmployee = new Repository.DataConcerns.Employee();
            RepositoryEmployee.Id = concernsEmployee.Id;
            RepositoryEmployee.EmployeeId = concernsEmployee.EmployeeId;
            RepositoryEmployee.FirstName = concernsEmployee.FirstName;
            RepositoryEmployee.LastName = concernsEmployee.LastName;
            RepositoryEmployee.DateOfBirth = concernsEmployee.DateOfBirth;
            RepositoryEmployee.Email = concernsEmployee.Email;
            RepositoryEmployee.Phone = concernsEmployee.Phone;
            RepositoryEmployee.JoinDate = concernsEmployee.JoinDate;
            RepositoryEmployee.LocationId = concernsEmployee.LocationId;
            RepositoryEmployee.RoleId = concernsEmployee.RoleId;
            RepositoryEmployee.DepartmentId = concernsEmployee.DepartmentId;
            RepositoryEmployee.Manager = concernsEmployee.Manager;
            RepositoryEmployee.ProjectId = concernsEmployee.ProjectId;
            RepositoryEmployee.Status = concernsEmployee.Status;
            RepositoryEmployee.Image = concernsEmployee.Image;
            return RepositoryEmployee;
        }
        // Ends here

        // Converting from Concerns.MultiselectFilters to Repository.DataConcerns.MultiselectFilter
        //private static Repository.DataConcerns.MultiselectFilter ConvertToRepositoryMultiselectFilters(MultiselectFilter concernsMultiselectFilters)
        //{
        //    Repository.DataConcerns.MultiselectFilter RepositoryMultiSelectFilters = new Repository.DataConcerns.MultiselectFilter();
        //    RepositoryMultiSelectFilters.Status = concernsMultiselectFilters.Status;
        //    RepositoryMultiSelectFilters.Locations = concernsMultiselectFilters.Locations;
        //    RepositoryMultiSelectFilters.Departments = concernsMultiselectFilters.Departments;
        //    return RepositoryMultiSelectFilters;
        //}

        // Converting from Repository.DataConcerns.Employee to Concerns.Employee
        private static List<Employee> ConvertToConcernsEmployee(List<Repository.DataConcerns.Employee> RepositoryEmployees)
        {
            List<Employee> concernsEmployees = new List<Employee>();

            foreach (var RepositoryEmployee in RepositoryEmployees)
            {
                Employee concernsEmployee = new Employee();
                concernsEmployee.Id = RepositoryEmployee.Id;
                concernsEmployee.EmployeeId = RepositoryEmployee.EmployeeId;
                concernsEmployee.FirstName = RepositoryEmployee.FirstName;
                concernsEmployee.LastName = RepositoryEmployee.LastName;
                concernsEmployee.DateOfBirth = RepositoryEmployee.DateOfBirth;
                concernsEmployee.Email = RepositoryEmployee.Email;
                concernsEmployee.Phone = RepositoryEmployee.Phone;
                concernsEmployee.JoinDate = RepositoryEmployee.JoinDate;
                concernsEmployee.LocationId = RepositoryEmployee.LocationId;
                concernsEmployee.LocationName = RepositoryEmployee.Location?.LocationName;
                concernsEmployee.RoleId = RepositoryEmployee.RoleId;
                concernsEmployee.RoleName = RepositoryEmployee.Role?.RoleName;
                concernsEmployee.DepartmentId = RepositoryEmployee.DepartmentId;
                concernsEmployee.DepartmentName = RepositoryEmployee.Department?.DepartmentName;
                concernsEmployee.Manager = RepositoryEmployee.Manager;
                concernsEmployee.ProjectId = RepositoryEmployee.ProjectId;
                concernsEmployee.ProjectName = RepositoryEmployee.Project?.ProjectName;
                concernsEmployee.Status = RepositoryEmployee.Status;
                concernsEmployee.Image = RepositoryEmployee.Image;
                concernsEmployees.Add(concernsEmployee);
            }
            return concernsEmployees;
        }
        // Ends here

        private Employee ConvertToConcernsEmployeeSingle(Repository.DataConcerns.Employee repositoryEmployee)
        {
            var concernsEmployee = new Employee
            {
                Id = repositoryEmployee.Id, // Assuming Id property exists in Employee model
                EmployeeId = repositoryEmployee.EmployeeId,
                FirstName = repositoryEmployee.FirstName,
                LastName = repositoryEmployee.LastName,
                DateOfBirth = repositoryEmployee.DateOfBirth,
                Email = repositoryEmployee.Email,
                Phone = repositoryEmployee.Phone,
                JoinDate = repositoryEmployee.JoinDate,
                LocationId = repositoryEmployee.LocationId,
                LocationName = repositoryEmployee.Location?.LocationName,
                RoleId = repositoryEmployee.RoleId,
                RoleName = repositoryEmployee.Role?.RoleName,
                DepartmentId = repositoryEmployee.DepartmentId,
                DepartmentName = repositoryEmployee.Department?.DepartmentName,
                Manager = repositoryEmployee.Manager,
                ProjectId = repositoryEmployee.ProjectId,
                ProjectName = repositoryEmployee.Project?.ProjectName,
                Status = repositoryEmployee.Status,
                Image = repositoryEmployee.Image
            };

            return concernsEmployee;
        }


        //Converting from Repository.DataConcerns.MultiselectFilter to Concerns.MultiselectFilters
        //private static List<MultiselectFilter> ConvertToConcernsMultiselect(List<Repository.DataConcerns.MultiselectFilter> RepositoryFilters)
        //{
        //    List<MultiselectFilter> ConcernsMultiselectFilters = new List<MultiselectFilter>();

        //    foreach (var Repositoryfilter in RepositoryFilters)
        //    {
        //        MultiselectFilter concernsMultiselectFilter = new MultiselectFilter();
        //        concernsMultiselectFilter.Status = Repositoryfilter.Status;
        //        concernsMultiselectFilter.Locations = Repositoryfilter.Locations;
        //        concernsMultiselectFilter.Departments = Repositoryfilter.Departments;

        //        ConcernsMultiselectFilters.Add(concernsMultiselectFilter);
        //    }
        //    return ConcernsMultiselectFilters;
        //}
        //Ends here
    }
}