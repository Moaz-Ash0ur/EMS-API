using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.DTOs
{
    public class AllowanceDto : BaseDto
    {
        [Required(ErrorMessage = "Allowance type is required")]
        [StringLength(100, ErrorMessage = "Type cannot exceed 100 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "DateGiven is required")]
        public DateTime DateGiven { get; set; }

        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }
    }
}
