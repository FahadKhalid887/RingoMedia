using AutoMapper;
using MediatR;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Interfaces.Repositories;

namespace RingoMedia.Application.Queries.Reminder.GetReminder
{
    public class GetReminderQueryHandler : IRequestHandler<GetReminderQuery, ReminderDto>
    {
        private readonly IReminderRepository _repository;
        private readonly IMapper _mapper;

        public GetReminderQueryHandler(IReminderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReminderDto> Handle(GetReminderQuery request, CancellationToken cancellationToken)
        {
            var reminder = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<ReminderDto>(reminder);
        }
    }
}
