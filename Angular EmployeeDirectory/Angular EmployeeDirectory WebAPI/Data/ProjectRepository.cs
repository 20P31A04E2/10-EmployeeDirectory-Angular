using Repository.Interfaces;
using Repository.DataConcerns;


namespace Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly EmployeeDBContext _context;

        public ProjectRepository(EmployeeDBContext context) 
        {
            _context = context;
        }

        public List<Project> JoinTables()
        {
            var query = from project in _context.Projects
                        join department in _context.Departments on project.DepartmentId equals department.DepartmentId
                        select new Project
                        {
                            ProjectId = project.ProjectId,
                            ProjectName = project.ProjectName,
                            DepartmentId = project.DepartmentId,
                            Department = project.Department,
                        };
            return query.ToList();
        }

        public List<Project> GetAllProjects()
        {
            return JoinTables().ToList();
        }


        public List<Project> ProjectsByDepartmentId(int departmentId)
        {
            var projectQuery = from p in _context.Projects
                               join dept in _context.Departments on p.DepartmentId equals dept.DepartmentId
                               where dept.DepartmentId == departmentId
                               select p;

            return projectQuery.ToList();
        }

    }
}
