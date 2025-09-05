using System.ComponentModel.DataAnnotations;

namespace EMS.BLL.DTOs
{
    public class PromotionDto : BaseDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string NewTitle { get; set; }

        [Required(ErrorMessage = "DatePromoted is required")]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "NewSalary is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "New salary must be greater than zero")]
        public decimal NewSalary { get; set; }

        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }
    }


}
