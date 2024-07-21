using System;
using System.Collections.Generic;

namespace Repository.DataConcerns;

public partial class Employee
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

    public int DepartmentId { get; set; }

    public int LocationId { get; set; }

    public int? ProjectId { get; set; }

    public string Manager { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Image { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual Role Role { get; set; } = null!;
}
