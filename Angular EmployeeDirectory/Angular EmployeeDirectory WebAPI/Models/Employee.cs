using System.ComponentModel.DataAnnotations;

namespace Concerns
{
    public class Employee
    {

        public int Id { get; set; }

        public string EmployeeId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateTime JoinDate { get; set; }

        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int LocationId { get; set; }

        public String? LocationName { get; set; }

        public int? ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public string Manager { get; set; } = null!;

        public string? Status { get; set; } = null!;

        public string? Image { get; set; }

    }
}
