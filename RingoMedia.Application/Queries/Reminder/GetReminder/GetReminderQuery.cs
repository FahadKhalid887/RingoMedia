using MediatR;
using RingoMedia.Application.DTOs;

namespace RingoMedia.Application.Queries.Reminder.GetReminder
{
    public class GetReminderQuery : IRequest<ReminderDto>
    {
        public int Id { get; set; }
    }
}
