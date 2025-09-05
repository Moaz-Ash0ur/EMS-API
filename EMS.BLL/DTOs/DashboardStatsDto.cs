namespace EMS.BLL.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalEmployees { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AverageSalary { get; set; }
        public int TotalPromotions { get; set; }
        public decimal PromotionPercentage { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalLeavesTaken { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalaryByDepartment { get; set; }
        public decimal MinSalaryByDepartment { get; set; }
    }


}
