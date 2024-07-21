using Services.Interfaces;
using Concerns;
using Repository.Interfaces;

namespace Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        { 
            _projectRepository = projectRepository;
        }

        public List<Project> DisplayProjects()
        {
            return ConvertToConcernsProjects(_projectRepository.GetAllProjects());
        }

        public List<Project> FilteredProjects(int departmentId)
        {
            return ConvertToConcernsProjects(_projectRepository.ProjectsByDepartmentId(departmentId));
        }

        private List<Project> ConvertToConcernsProjects(List<Repository.DataConcerns.Project> dataProjects)
        {
            List<Project> concernsProjects = new List<Project>();

            foreach (var dataProject in dataProjects)
            {
                Project concernsProject = new Project();
                concernsProject.ProjectId = dataProject.ProjectId;
                concernsProject.ProjectName = dataProject.ProjectName;
                concernsProjects.Add(concernsProject);
            }
            return concernsProjects;
        }
    }
}
