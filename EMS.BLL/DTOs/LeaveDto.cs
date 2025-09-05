using System.ComponentModel.DataAnnotations;

namespace EMS.BLL.DTOs
{
    public class LeaveDto : BaseDto
    {
        [Required(ErrorMessage = "Reason is required")]
        [StringLength(200, ErrorMessage = "Reason cannot exceed 200 characters")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }

        public bool IsAccepted { get; set; }

        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }
    }



}
