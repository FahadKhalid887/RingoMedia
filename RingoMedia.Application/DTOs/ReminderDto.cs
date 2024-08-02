using RingoMedia.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace RingoMedia.Application.DTOs
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Message { get; set; }
        public DateTimeOffset DateTime { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public ReminderStatus Status { get; set; }
    }
}
