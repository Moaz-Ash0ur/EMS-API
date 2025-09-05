using EMS.BLL.DTOs;
using EMS.BLL.Interface.Base;
using EMS.DAL.Models;

namespace EMS.BLL.Interface
{
    public interface IPromotionService : IBaseService<Promotion, PromotionDto>
    {

         (int, string) AddPromotionToEmployee(PromotionDto dto);
         PromotionDto GetLastPromotion(int employeeId);
         List<PromotionDto> GetAllEmployeeId(int employeeId);


    }




}


