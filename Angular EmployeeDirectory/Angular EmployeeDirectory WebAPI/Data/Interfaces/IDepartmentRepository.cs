using Repository.DataConcerns;

namespace Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        public List <Department> AllDepartments();
        public List<Department> DepartmentsAfterFiltering(int roleId);

    }
}
