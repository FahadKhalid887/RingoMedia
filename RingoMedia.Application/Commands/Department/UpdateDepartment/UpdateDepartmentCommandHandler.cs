using AutoMapper;
using MediatR;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Application.Commands.Department.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, int>
    {
        private readonly IDepartmentRepository _repository;

        public UpdateDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(request.Id, request.DepartmentName, request.DepartmentLogo, request.ParentId, cancellationToken);
            return request.Id;
        }
    }
}
