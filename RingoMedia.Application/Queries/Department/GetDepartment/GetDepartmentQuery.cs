using MediatR;
using RingoMedia.Application.DTOs;

namespace RingoMedia.Application.Queries.Department.GetDepartment
{
    public class GetDepartmentQuery : IRequest<DepartmentDto>
    {
        public int Id { get; set; }
    }
}
