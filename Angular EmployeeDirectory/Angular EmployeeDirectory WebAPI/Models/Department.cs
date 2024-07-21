namespace Concerns
{
    public  class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
