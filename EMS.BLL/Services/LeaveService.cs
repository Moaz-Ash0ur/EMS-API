using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EMS.BLL.Services
{
    public class LeaveService : BaseService<Leave, LeaveDto>, ILeaveService
    {

        IEmployeeService _employeeService;
        IGenericRepository<Leave> _repo;
        IMapper _mapper;

        public LeaveService(IGenericRepository<Leave> repo, IMapper mapper, IUserService userService, IEmployeeService employeeService)
            : base(repo, mapper, userService)
        {
            _repo = repo;
            _employeeService = employeeService;
            _mapper = mapper;
        }


        public (int, string) AddLeaveToEmployee(LeaveDto dto)
        {
             
            var employee = _employeeService.GetByID(dto.EmployeeId);
            if (employee == null)
                return (0, "Employee not found.");

           
            if (dto.StartDate >= dto.EndDate)
                return (0, "Leave end date must be after start date.");

            if (dto.StartDate < DateTime.Today)
                return (0, "Leave start date cannot be in the past.");

         
            var overlappingLeave = GetActiveLeavesByEmployee(dto.EmployeeId)
                .Any(l =>((dto.StartDate >= l.StartDate && dto.StartDate <= l.EndDate) ||
                     (dto.EndDate >= l.StartDate && dto.EndDate <= l.EndDate)));


            if (overlappingLeave)
                return (0, "Employee already has leave booked in this period.");

           
            var newId = 0;
            this.Insert(dto, out newId);

            return (newId, "Leave request added successfully.");
        }

        public List<LeaveDto> GetActiveLeavesByEmployee(int empId)
        {
           var empLeaves =  _repo.GetList(l => l.EmployeeId == empId && l.IsActive);
            return _mapper.Map<List<Leave>, List<LeaveDto>>(empLeaves);
        }

        public bool UpdateLeaveStatus(int leaveId, bool isActive)
        {
            var leave = _repo.GetFirst(l => l.Id == leaveId);
            if (leave == null)
                return false;

            leave.IsActive = isActive;
            _repo.Update(leave);
            return true;
        }





    }


}
