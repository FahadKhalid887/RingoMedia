using MediatR;
using Microsoft.AspNetCore.Http;

namespace RingoMedia.Application.Commands.Department.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public IFormFile? DepartmentLogo { get; set; }
    }
}
