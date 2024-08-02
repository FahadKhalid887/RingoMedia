using MediatR;
using Microsoft.AspNetCore.Http;

namespace RingoMedia.Application.Commands.Department.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public IFormFile? DepartmentLogo { get; set; }
    }
}
