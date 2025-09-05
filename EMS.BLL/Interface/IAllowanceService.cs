using EMS.BLL.DTOs;
using EMS.BLL.Interface.Base;
using EMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMS.BLL.Interface
{
    public interface IAllowanceService : IBaseService<Allowance,AllowanceDto>
    {
         (int, string) AddAllowanceToEmployee(AllowanceDto dto);
         List<AllowanceDto> GetByEmployeeId(int empId);
      
    }


}


