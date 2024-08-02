using AutoMapper;
using MediatR;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Application.Commands.Reminder.UpdateReminder
{
    public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, int>
    {
        private readonly IReminderRepository _repository;

        public UpdateReminderCommandHandler(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(request.Id, request.Status, request.ErrorMessage, cancellationToken);
            return request.Id;
        }
    }
}
