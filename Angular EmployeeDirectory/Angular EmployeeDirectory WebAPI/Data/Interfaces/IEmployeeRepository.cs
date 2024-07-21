using Repository.DataConcerns;

namespace Repository.Interfaces
{

    public interface IEmployeeRepository
    {
        bool EmployeeExists(string employeeId);

        void AddingEmployee(Employee emp);

        List<Employee> DisplayEmployees();

        List<Employee> FilteringAndSorting(string FilterButtonText, string[] status, string[] locations, string[] departments, string sortBy, string sortOrder);

        public List<Employee> EmployeesByDeptId(int departmentId);

        public Employee EmployeeBasedOnId(string employeeId);

        public List<Employee> EmployeeByRole(int rolesId);


        void UpdatingAnEmployee(string employeeIdToEdit, Employee emp);

        void DeletingAnEmployee(string inputId);
    }
}
