using AutoMapper;
using MediatR;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Interfaces.Repositories;

namespace RingoMedia.Application.Queries.Reminder.GetReminders
{
    public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, IEnumerable<ReminderDto>>
    {
        private readonly IReminderRepository _repository;
        private readonly IMapper _mapper;

        public GetRemindersQueryHandler(IReminderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReminderDto>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
        {
            var reminder = await _repository.FindAsync(request.Status, request.DateTo, cancellationToken);
            return _mapper.Map<IEnumerable<ReminderDto>>(reminder);
        }
    }
}
