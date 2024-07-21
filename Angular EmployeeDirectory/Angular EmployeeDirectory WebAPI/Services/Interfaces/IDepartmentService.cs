using Concerns;

namespace Services.Interfaces
{
    public interface IDepartmentService
    {
        public List<Department> GetAllDepartments();
        public List<Department> FilteredDepartments(int roleId);

    }
}
