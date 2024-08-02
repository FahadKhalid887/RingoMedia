using System.ComponentModel.DataAnnotations;

namespace RingoMedia.Web.Models.ViewModels
{
    public class CreateReminderVM
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Message { get; set; }

        [Display(Name = "Date and time")]
        public DateTimeOffset DateTime { get; set; }

        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;
    }
}
