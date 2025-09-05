using System.ComponentModel.DataAnnotations;

namespace EMS.BLL.DTOs
{
    public class DepartmentDto : BaseDto
    {
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string Name { get; set; }
    }



}
