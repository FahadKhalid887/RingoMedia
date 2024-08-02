using AutoMapper;
using MediatR;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Application.Commands.Reminder.DeleteReminder
{
    public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand>
    {
        private readonly IReminderRepository _repository;

        public DeleteReminderCommandHandler(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
