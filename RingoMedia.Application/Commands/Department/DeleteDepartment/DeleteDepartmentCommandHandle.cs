using MediatR;
using RingoMedia.Application.Interfaces.Repositories;

namespace RingoMedia.Application.Commands.Department.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IDepartmentRepository _repository;

        public DeleteDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
