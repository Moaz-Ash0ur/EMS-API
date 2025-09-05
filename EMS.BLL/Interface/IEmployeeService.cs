using EMS.BLL.DTOs;
using EMS.BLL.Interface.Base;
using EMS.BLL.Services;
using EMS.BLL.Services.PaginationHelper;
using EMS.DAL.Models;
using System;


namespace EMS.BLL.Interface
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeDto>
    {
        public PagedResult<EmployeeDto> GetAllPaginted(int pageNumber, int pageSize);
        public PagedResult<EmployeeDto> GetFilteredEmployees(EmployeeFilterDto filter, int pageNumber, int pageSize);

    }


}




