namespace RingoMedia.Application.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? DepartmentLogo { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<DepartmentDto> SubDepartments { get; set; } = Enumerable.Empty<DepartmentDto>();
    }
}
