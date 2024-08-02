using MediatR;
using RingoMedia.Application.DTOs;

namespace RingoMedia.Application.Queries.Department.GetFilteredDepartments
{
    public class GetFilteredDepartmentsQuery : IRequest<IEnumerable<DepartmentDto>>
    {
    }
}
