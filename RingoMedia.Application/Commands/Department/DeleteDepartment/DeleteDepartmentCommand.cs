using MediatR;

namespace RingoMedia.Application.Commands.Department.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
