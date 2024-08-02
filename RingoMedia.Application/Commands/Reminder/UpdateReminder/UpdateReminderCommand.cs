using MediatR;
using RingoMedia.Domain.Enums;

namespace RingoMedia.Application.Commands.Reminder.UpdateReminder
{
    public class UpdateReminderCommand : IRequest<int>
    {
        public int Id { get; set; }
        public ReminderStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
