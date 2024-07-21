using Repository.DataConcerns;
using Repository.Interfaces;

namespace Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext _context;
        public EmployeeRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public List<Employee> JoinTables()
        {
            var query = from emp in _context.Employees
                        join project in _context.Projects on emp.ProjectId equals project.ProjectId
                        join role in _context.Roles on emp.RoleId equals role.RolesId
                        join department in _context.Departments on emp.DepartmentId equals department.DepartmentId
                        join location in _context.Locations on emp.LocationId equals location.LocationId
                        select new Employee
                        {
                            EmployeeId = emp.EmployeeId,
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            DateOfBirth = emp.DateOfBirth,
                            Email = emp.Email,
                            Phone = emp.Phone,
                            JoinDate = emp.JoinDate,
                            RoleId = emp.RoleId,
                            DepartmentId = emp.DepartmentId,
                            LocationId = emp.LocationId,
                            ProjectId = emp.ProjectId,
                            Manager = emp.Manager,
                            Status = emp.Status,
                            Image = emp.Image,
                            Project = emp.Project,
                            Role = emp.Role,
                            Department = emp.Department,
                            Location = emp.Location
                        };
            return query.ToList();
        }

        public bool EmployeeExists(string employeeId)
        {
            return JoinTables().Any(e => e.EmployeeId == employeeId);
        }

        //Adding an employee starts here
        public void AddingEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
        }
        //Adding an employee ends here

        // Selecting all Employees/Single employee starts here
        public List<Employee> DisplayEmployees()
        {
            return JoinTables();
        }

        // Selecting all Employees/Single employee ends here


        // Multiselect filtering starts here
        public List<Employee> FilteringAndSorting(string FilterButtonText, string[] status, string[] locations, string[] departments, string sortBy, string sortOrder)
        {
            var employees = JoinTables().AsQueryable();

            if (FilterButtonText != null)
            {
                employees = employees.Where(e => e.FirstName.StartsWith(FilterButtonText, StringComparison.OrdinalIgnoreCase));
            }

            if (status != null && status.Length > 0)
            {
                employees = employees.Where(e => status.Contains(e.Status));
            }

            if (locations != null && locations.Length > 0)
            {
                employees = employees.Where(e => locations.Contains(e.Location.LocationName));
            }

            if (departments != null && departments.Length > 0)
            {
                employees = employees.Where(e => departments.Contains(e.Department.DepartmentName));
            }
            if (sortBy != null || sortOrder != null)
            {
                switch (sortBy.ToLower())
                {
                    case "users":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.FirstName) : employees.OrderByDescending(e => e.FirstName);
                        break;
                    case "location":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.Location.LocationName) : employees.OrderByDescending(e => e.Location.LocationName);
                        break;
                    case "department":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.Department.DepartmentName) : employees.OrderByDescending(e => e.Department.DepartmentName);
                        break;
                    case "role":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.Role.RoleName) : employees.OrderByDescending(e => e.Role.RoleName);
                        break;
                    case "emp.no":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.EmployeeId) : employees.OrderByDescending(e => e.EmployeeId);
                        break;
                    case "status":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.Status) : employees.OrderByDescending(e => e.Status);
                        break;
                    case "join dt":
                        employees = sortOrder == "asc" ? employees.OrderBy(e => e.JoinDate) : employees.OrderByDescending(e => e.JoinDate);
                        break;
                    default:
                        break;
                }
            }

            return employees.ToList();
        }
        // Multiselect filtering ends here

        public List<Employee> EmployeesByDeptId(int departmentId)
        {
            var employeeQuery = from emp in _context.Employees
                                join dept in _context.Departments on emp.DepartmentId equals dept.DepartmentId
                                where emp.DepartmentId == departmentId
                                select emp;

            return employeeQuery.ToList();
        }

        public Employee EmployeeBasedOnId(string employeeId)
        {
            var employee = JoinTables().FirstOrDefault(e => e.EmployeeId == employeeId);
            return employee;
        }

        public List<Employee> EmployeeByRole(int rolesId)
        {

            var employeequery = JoinTables().AsQueryable();
            employeequery = from emp in _context.Employees
                                join role in _context.Roles on emp.RoleId equals role.RolesId
                                where emp.RoleId == rolesId
                                select emp;

            return employeequery.ToList();
        }



        // Editing an Employee starts here
        public void UpdatingAnEmployee(string employeeIdToEdit, Employee emp)
        {
            var employeeToUpdate = _context.Employees.SingleOrDefault(e => e.EmployeeId == employeeIdToEdit);

            if (employeeToUpdate is not null)
            {
                employeeToUpdate.FirstName = emp.FirstName;
                employeeToUpdate.LastName = emp.LastName;
                employeeToUpdate.DateOfBirth = emp.DateOfBirth;
                employeeToUpdate.Email = emp.Email;
                employeeToUpdate.Phone = emp.Phone;
                employeeToUpdate.JoinDate = emp.JoinDate;
                employeeToUpdate.LocationId = emp.LocationId;
                employeeToUpdate.RoleId = emp.RoleId;
                employeeToUpdate.DepartmentId = emp.DepartmentId;
                employeeToUpdate.Manager = emp.Manager;
                employeeToUpdate.ProjectId = emp.ProjectId;
                employeeToUpdate.Status = emp.Status;
                employeeToUpdate.Image = emp.Image;
                _context.Employees.Update(employeeToUpdate);
                _context.SaveChanges();
            }
        }
        // Editing an Employee ends here

        // Deleting an Employee starts here
        public void DeletingAnEmployee(string inputId)
        {
            var employee = _context.Employees.SingleOrDefault(emp => emp.EmployeeId == inputId);
            if (employee is not null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
        // Deleting an Employee ends here
    }
}
