
namespace Concerns
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

    }
}
