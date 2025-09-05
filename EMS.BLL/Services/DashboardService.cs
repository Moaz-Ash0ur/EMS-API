using EMS.BLL.DTOs;
using EMS.BLL.Interface;

namespace EMS.BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPromotionService _promotionService;
        private readonly ILeaveService _leaveService;
        private readonly IDepartmentService _departmentService;

        public DashboardService(IEmployeeService employeeService,IPromotionService promotionService,
            ILeaveService leaveService,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _promotionService = promotionService;
            _leaveService = leaveService;
            _departmentService = departmentService;
        }

        public DashboardStatsDto GetDashboardStats()
        {
            var employees =  _employeeService.GetAll();
            var promotions =  _promotionService.GetAll();
            var leaves =  _leaveService.GetAll();
            var departments =  _departmentService.GetAll();

            if (employees == null || !employees.Any())
                return null;

            var totalEmployees = employees.Count();
            var totalSalary = employees.Sum(e => e.Salary);
            var averageSalary = employees.Average(e => e.Salary);

            var totalPromotions = promotions.Count();
            var promotionPercentage = totalPromotions * 100m / totalEmployees;


            var totalLeavesTaken = leaves.Sum(l => (l.EndDate - l.StartDate).Days + 1);

            var maxSalary = employees.Max(e => e.Salary);
            var minSalary = employees.Min(e => e.Salary);

            var totalDepartments = departments.Count();


            var maxSalaryByDepartment = employees
                .GroupBy(e => e.DepartmentId)
                .Select(g => g.Max(e => e.Salary))
                .DefaultIfEmpty(0)
                .Max();

            var minSalaryByDepartment = employees
                .GroupBy(e => e.DepartmentId)
                .Select(g => g.Min(e => e.Salary))
                .DefaultIfEmpty(0)
                .Min();

            return new DashboardStatsDto
            {
                TotalEmployees = totalEmployees,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary,
                TotalPromotions = totalPromotions,
                PromotionPercentage = Math.Round(promotionPercentage, 2),                
                TotalLeavesTaken = totalLeavesTaken,
                MaxSalary = maxSalary,
                MinSalary = minSalary,
                MaxSalaryByDepartment = maxSalaryByDepartment,
                MinSalaryByDepartment = minSalaryByDepartment,
                TotalDepartments = totalDepartments
            };
        }
    }


}






