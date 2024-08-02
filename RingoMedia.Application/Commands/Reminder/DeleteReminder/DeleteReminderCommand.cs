using MediatR;

namespace RingoMedia.Application.Commands.Reminder.DeleteReminder
{
    public class DeleteReminderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
