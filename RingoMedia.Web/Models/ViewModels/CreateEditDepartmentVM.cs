using System.ComponentModel.DataAnnotations;

namespace RingoMedia.Web.Models.ViewModels
{
    public class CreateEditDepartmentVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Department name")]
        public string DepartmentName { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Department logo")]
        public IFormFile? DepartmentLogo { get; set; }
    }
}
