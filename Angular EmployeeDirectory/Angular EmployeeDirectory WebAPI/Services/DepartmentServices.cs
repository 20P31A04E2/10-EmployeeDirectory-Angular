using Repository.Interfaces;
using Services.Interfaces;
using Concerns;

namespace Services
{
    public class DepartmentServices : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public List<Department> GetAllDepartments()
        {
            return ConvertToConcernsDepartments(_departmentRepository.AllDepartments());
        }

        public List<Department> FilteredDepartments(int roleId)
        {
            return ConvertToConcernsDepartments(_departmentRepository.DepartmentsAfterFiltering(roleId));
        }

        private List<Department> ConvertToConcernsDepartments(List<Repository.DataConcerns.Department> dataDepartments)
        {
            List<Department> concernsDepartments = new List<Department>();

            foreach (var dataDepartment in dataDepartments)
            {
                Department concernsDepartment = new Department();
                concernsDepartment.DepartmentId = dataDepartment.DepartmentId;
                concernsDepartment.DepartmentName = dataDepartment.DepartmentName;
                concernsDepartments.Add(concernsDepartment);
            }
            return concernsDepartments;
        }
    }
}
