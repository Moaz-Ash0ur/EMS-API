using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.BLL.Services
{
    public class PromotionService : BaseService<Promotion, PromotionDto>, IPromotionService
    {

        private readonly IEmployeeService _employeeService;
        private readonly IGenericRepository<Promotion> _repo;
        IMapper _mapper;

        public PromotionService(IGenericRepository<Promotion> repo, IMapper mapper, IUserService userService, IEmployeeService employeeService)
            : base(repo, mapper, userService)
        {
            _repo = repo;
            _employeeService = employeeService;
            _mapper = mapper;
        }


        private bool IsPromotionTooRecent(PromotionDto lastPromotion)
        {
            return lastPromotion != null &&
                   (DateTime.Now - lastPromotion.EffectiveDate).TotalDays < 180;
        }

        private string ValidateNewSalary(decimal newSalary, decimal currentSalary)
        {
            if (newSalary <= currentSalary)
                return "New salary must be greater than current salary.";

            decimal minSalary = currentSalary * 1.05m;
            decimal maxSalary = currentSalary * 1.30m;

            if (newSalary < minSalary || newSalary > maxSalary)
                return $"New salary must be between 5% and 30% higher than current salary. Allowed range: {minSalary} - {maxSalary}";

            return string.Empty;
        }

        private int InsertPromotion(PromotionDto dto)
        {
            var newId = 0;
            this.Insert(dto, out newId);
            return newId;
        }

        private void UpdateEmployee(EmployeeDto employee, PromotionDto dto)
        {
            employee.Salary = dto.NewSalary;
            employee.Title = dto.NewTitle;
            _employeeService.Update(employee);
        }


        public (int, string) AddPromotionToEmployee(PromotionDto dto)
        {
            var employee = _employeeService.GetByID(dto.EmployeeId);
            if (employee == null)
                return (0, "Employee not found.");

            var lastPromotion = GetLastPromotion(dto.EmployeeId);
            if (IsPromotionTooRecent(lastPromotion))
                return (0, "Employee cannot be promoted again within 6 months.");

            var salaryValidationMsg = ValidateNewSalary(dto.NewSalary, employee.Salary);
            if (!string.IsNullOrEmpty(salaryValidationMsg))
                return (0, salaryValidationMsg);

            var newId = InsertPromotion(dto);

            UpdateEmployee(employee, dto);

            return (newId, "Promotion added successfully.");
        }


        public PromotionDto GetLastPromotion(int employeeId)
        {
            var lastPromot = _repo.GetList(p => p.EmployeeId == employeeId && p.IsActive)
                           .OrderByDescending(p => p.EffectiveDate)
                           .FirstOrDefault();

            if (lastPromot == null) return null!;


            return _mapper.Map<Promotion,PromotionDto>(lastPromot);
        }


        public List<PromotionDto> GetAllEmployeeId(int employeeId)
        {
            var empPromotions = _repo.GetList(p => p.EmployeeId == employeeId)                               
                                 .OrderByDescending(p => p.EffectiveDate)
                                 .ToList();

            return _mapper.Map<List<Promotion>, List<PromotionDto>>(empPromotions);

        }


    }




}
