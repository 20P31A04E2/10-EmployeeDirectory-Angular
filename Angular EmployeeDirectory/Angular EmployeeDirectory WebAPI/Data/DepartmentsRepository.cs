using Repository.DataConcerns;
using Repository.Interfaces;


namespace Repository
{
    public class DepartmentsRepository : IDepartmentRepository
    {
        private readonly EmployeeDBContext _context;
        public DepartmentsRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        public List<Department> AllDepartments()
        {
            return _context.Departments.ToList();
        }


        public List<Department> DepartmentsAfterFiltering(int roleId)
        {
            var query = from dept in _context.Departments
                        join role in _context.Roles on dept.DepartmentId equals role.DepartmentId
                        where role.RolesId == roleId
                        select dept;

            return query.ToList();
        }


    }
}
