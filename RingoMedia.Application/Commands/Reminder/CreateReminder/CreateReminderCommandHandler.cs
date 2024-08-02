using AutoMapper;
using MediatR;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Application.Commands.Reminder.CreateReminder
{
    public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, int>
    {
        private readonly IReminderRepository _repository;

        public CreateReminderCommandHandler(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Title, request.Message, request.Email, request.DateTime, cancellationToken);
        }
    }
}
