using AutoMapper;
using MediatR;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Interfaces.Repositories;

namespace RingoMedia.Application.Queries.Department.GetDepartment
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetDepartmentQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
