using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.BLL.Services.PaginationHelper;
using EMS.DAL.Contracts;
using EMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto> , IEmployeeService
    {

        private readonly IGenericRepository<Employee> _repo;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> repo, IMapper mapper, IUserService userService, IMemoryCache memoryCache)
            : base(repo, mapper, userService, memoryCache)
        {
            _repo = repo;
            _mapper = mapper;
        }
  
        public PagedResult<EmployeeDto> GetAllPaginted(int pageNumber, int pageSize)
        {
            var allEmployess = _repo.GetAllQueryable();
            var employessDto = _mapper.Map<List<EmployeeDto>>(allEmployess);

            return employessDto.ToPagedResult(pageNumber, pageSize);
        }

        public PagedResult<EmployeeDto> GetFilteredEmployees(EmployeeFilterDto filter, int pageNumber, int pageSize)
        {
            var query = _repo.GetAllQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(e => e.FirstName.Contains(filter.FirstName));


            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(e => e.FirstName.Contains(filter.LastName));

            if (filter.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == filter.DepartmentId.Value);

            if (filter.MinSalary.HasValue)
                query = query.Where(e => e.Salary >= filter.MinSalary.Value);

            if (filter.MaxSalary.HasValue)
                query = query.Where(e => e.Salary <= filter.MaxSalary.Value);

            if (filter.HasPromotion.HasValue)
            {
                query = filter.HasPromotion.Value
                    ? query.Where(e => e.Promotions.Any())
                    : query.Where(e => !e.Promotions.Any());
            }


            var employessDto = _mapper.Map<List<EmployeeDto>>(query);


            return employessDto.ToPagedResult(pageNumber, pageSize);
        }


   }

}
