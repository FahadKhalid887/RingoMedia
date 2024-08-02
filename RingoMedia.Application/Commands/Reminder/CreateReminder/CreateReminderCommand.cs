using MediatR;

namespace RingoMedia.Application.Commands.Reminder.CreateReminder
{
    public class CreateReminderCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string? Message { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
