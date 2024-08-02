using AutoMapper;
using MediatR;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Interfaces.Repositories;

namespace RingoMedia.Application.Queries.Department.GetFilteredDepartments
{
    public class GetFilteredDepartmentsQueryHandler : IRequestHandler<GetFilteredDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetFilteredDepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> Handle(GetFilteredDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repository.FindAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }
    }
}
