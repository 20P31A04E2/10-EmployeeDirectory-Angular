using Repository.DataConcerns;


namespace Repository.Interfaces
{
    public interface IProjectRepository
    {
        public List<Project> GetAllProjects();

        public List<Project> ProjectsByDepartmentId(int departmentId);

    }
}
