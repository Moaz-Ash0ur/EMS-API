using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;

namespace EMS.BLL.Services
{
    public class DepartmentService : BaseService<Department, DepartmentDto>, IDepartmentService
    {
        public DepartmentService(IGenericRepository<Department> repo, IMapper mapper, IUserService userService)
            : base(repo, mapper, userService)
        {
        }
    }




}
