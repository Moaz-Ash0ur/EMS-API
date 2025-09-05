namespace EMS.BLL.DTOs
{
    public class EmployeeFilterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DepartmentId { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public bool? HasPromotion { get; set; }
        public bool? HasAllowance { get; set; }
    }
}
