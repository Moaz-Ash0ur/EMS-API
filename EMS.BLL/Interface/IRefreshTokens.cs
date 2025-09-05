using EMS.BLL.DTOs;
using EMS.BLL.Interface.Base;
using EMS.DAL.Models;

namespace EMS.BLL.Interface
{
    public interface IRefreshTokens : IBaseService<RefreshToken, RefreshTokenDto>
    {
        public bool Refresh(RefreshTokenDto tokenDto);
        public bool IsExpireToken(string token);
        public RefreshTokenDto GetByToken(string token);

    }


}






