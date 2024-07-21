using Concerns;
namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        public void AddEmployee(Employee emp);

        public List<Employee> ViewAllEmployees();

        public List<Employee> FilteredAndSortedEmployees(string FilterButtonText,string[] status, string[] locations, string[] departments, string sortby, string sortorder);

        public List<Employee> EmployeesByDepartment(int departmentId);

        public List<Employee> EmployeeByRolesId(int rolesId);

        public Employee EmployeeById(string employeeId);

        public void EditEmployee(string employeeToEdit, Employee emp);

        public void DeleteEmployee(string Id);

    }
}
