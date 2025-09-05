using System.ComponentModel.DataAnnotations;

namespace EMS.BLL.DTOs
{
    public class AppreciationDto : BaseDto
    {
        [Required(ErrorMessage = "Note is required")]
        [StringLength(250, ErrorMessage = "Note cannot exceed 250 characters")]
        public string Note { get; set; }

        [Required(ErrorMessage = "DateGiven is required")]
        public DateTime DateGiven { get; set; }

        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }
    }



}
