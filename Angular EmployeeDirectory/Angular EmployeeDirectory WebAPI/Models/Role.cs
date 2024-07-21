namespace Concerns
{
    public class Role
    {
        public int RolesId { get; set; }

        public string RoleName { get; set; } = null!;

        public string RoleDescription { get; set; } = null!;

        public int LocationId { get; set; }

        public string? LocationName { get; set; }

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }


    }
}
