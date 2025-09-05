using System.ComponentModel.DataAnnotations;

namespace EMS.BLL.DTOs
{
    public class EmployeeDto : BaseDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than zero")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "HireDate is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "DepartmentId is required")]
        public int DepartmentId { get; set; }
    }



}
