using EMS.BLL.DTOs;
using EMS.BLL.Interface.Base;
using EMS.DAL.Models;

namespace EMS.BLL.Interface
{
    public interface IAppreciationService : IBaseService<Appreciation, AppreciationDto>
    {
         (int, string) AddAppreciationToEmployee(AppreciationDto dto);
         List<AppreciationDto> GetAppreciationByEmployee(int empId);
        
    }


}


