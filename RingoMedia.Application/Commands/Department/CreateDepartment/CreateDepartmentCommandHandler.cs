using AutoMapper;
using MediatR;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Application.Commands.Department.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartmentRepository _repository;

        public CreateDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.DepartmentName, request.DepartmentLogo, request.ParentId, cancellationToken);
        }
    }
}
