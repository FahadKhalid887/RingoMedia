using MediatR;
using RingoMedia.Application.DTOs;
using RingoMedia.Domain.Enums;

namespace RingoMedia.Application.Queries.Reminder.GetReminders
{
    public class GetRemindersQuery : IRequest<IEnumerable<ReminderDto>>
    {
        public ReminderStatus? Status { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
