using Concerns;

namespace Services.Interfaces
{
    public interface IProjectService
    {
        public List<Project> DisplayProjects();

        public List<Project> FilteredProjects(int departmentId);
    }
}
