using RingoMedia.Domain.Enums;
using RingoMedia.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RingoMedia.Domain.Entities
{
    public class ReminderEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Message { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public ReminderStatus Status { get; set; }

        [StringLength(1000)]
        public string? ErrorMessage { get; set; }
    }
}
