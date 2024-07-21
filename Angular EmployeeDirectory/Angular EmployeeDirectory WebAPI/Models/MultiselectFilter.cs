namespace Concerns
{
    public class MultiselectFilter
    {
        public string? FilterButtonText { get; set; }
        public string[]? Status { get; set; }
        public string[]? Locations { get; set; }
        public string[]? Departments { get; set; }

        public string? SortBy { get; set; } 
        public string? SortOrder { get; set;}
    }
}
