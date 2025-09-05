using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace EMS.BLL.Services
{
    public class AllowanceService : BaseService<Allowance, AllowanceDto>, IAllowanceService
    {

        private readonly IEmployeeService _employeeService;
        private readonly IGenericRepository<Allowance> _repo;
        IMapper _mapper;

        public AllowanceService(IGenericRepository<Allowance> repo, IMapper mapper, IUserService userService, IEmployeeService employeeService)
            : base(repo, mapper, userService)
        {
            _employeeService = employeeService;
            _repo = repo;
            _mapper = mapper;
        }


        public (int,string) AddAllowanceToEmployee(AllowanceDto dto)
        {
            var employee = _employeeService.GetByID(dto.EmployeeId);
            if (employee == null)
                return(0,"Employee with ID  not found");

            if (dto.Amount <= 0)
            return (0, "Allowance amount must be greater than 0");

            decimal maxAllowed = employee.Salary * 0.3m;
            if (dto.Amount > maxAllowed)
            return (0, $"Allowance cannot exceed 30% of salary. Max allowed {maxAllowed}");

            var allowancesThisYear = GetByEmployeeAndYear(dto.EmployeeId, DateTime.Now.Year);
            if (allowancesThisYear.Count >= 3)
            return (0, "Employee has already received maximum number of allowances for this year");

              var newId = 0;
              this.Insert(dto,out newId);

            return (newId, "Allowance addedd Successfully");
        }

        public List<AllowanceDto> GetByEmployeeId(int empId)
        {
            var empAllowances = _repo.GetList(a => a.EmployeeId == empId);
            return _mapper.Map<List<Allowance>, List<AllowanceDto>>(empAllowances);
        }


        private List<Allowance> GetByEmployeeAndYear(int empId, int year)
        {
          return  _repo.GetList(a =>  a.EmployeeId == empId &&
          a.DateGiven.Year == year && 
          a.IsActive);    
            
        }

         


    }




}
