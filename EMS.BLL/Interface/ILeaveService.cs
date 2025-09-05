using EMS.BLL.DTOs;
using EMS.BLL.Interface.Base;
using EMS.DAL.Models;
using Microsoft.Identity.Client;

namespace EMS.BLL.Interface
{
    public interface ILeaveService : IBaseService<Leave, LeaveDto>
    {
        (int, string) AddLeaveToEmployee(LeaveDto dto);

        List<LeaveDto> GetActiveLeavesByEmployee(int empId);

        bool UpdateLeaveStatus(int leaveId, bool isActive);

    }



}


