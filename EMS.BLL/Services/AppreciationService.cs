using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;

namespace EMS.BLL.Services
{
    public class AppreciationService : BaseService<Appreciation, AppreciationDto>, IAppreciationService
    {

        IGenericRepository<Appreciation> _repo;
        IEmployeeService _employeeService;
        IMapper _mapper;


        public AppreciationService(IGenericRepository<Appreciation> repo, IMapper mapper, IUserService userService, IEmployeeService employeeService)
            : base(repo, mapper, userService)
        {
            _repo = repo;
            _mapper = mapper;
            _employeeService = employeeService;
        }



        public (int, string) AddAppreciationToEmployee(AppreciationDto dto)
        {
            var emp = _employeeService.GetByID(dto.EmployeeId);
            if (emp == null) return (0, "Employee Not Found!");

            //semuliate have letterNumber from Table in Company and cehck if emp jave same one before

            // if (HaveSameLetterNumber(dto.EmployeeId, dto.Id))
            // return (0, "Duplicate Letter Number for the same employee");

            var newId = 0;
            this.Insert(dto, out newId);

            return (newId, "Appreciation Letter Added Successfully");
        }

        public List<AppreciationDto> GetAppreciationByEmployee(int empId)
        {
            var empAppreciations = _repo.GetList(a => a.EmployeeId == empId);

            if (empAppreciations == null) return null;

            return _mapper.Map<List<Appreciation>,List<AppreciationDto>>(empAppreciations);
        }

        private bool HaveSameLetterNumber(int empId , int letterNumber)
        {
            //semuliate have letterNumber from Table in Company and cehck if emp jave same one before
           return _repo.GetList(a => a.EmployeeId == empId && a.Id == letterNumber).Any();
        }





    }




}
