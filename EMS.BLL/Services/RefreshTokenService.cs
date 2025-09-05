using AutoMapper;
using EMS.BLL.DTOs;
using EMS.BLL.Interface;
using EMS.BLL.Services.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;

namespace EMS.BLL.Services
{
    public class RefreshTokenService : BaseService<RefreshToken, RefreshTokenDto>, IRefreshTokens
    {

        private IGenericRepository<RefreshToken> _repo;
        private IMapper _mapper;
        private readonly IUserService _userService;

        public RefreshTokenService(IGenericRepository<RefreshToken> repo, IMapper mapper,
            IUserService userService) : base(repo, mapper, userService)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
        }


        public bool Refresh(RefreshTokenDto tokenDto)
        {
            var allTokens = _repo.GetList(r => r.UserId == Guid.Parse(tokenDto.UserId) && r.CurrentState == 0);

            foreach (var dbToken in allTokens)
            {
                _repo.ChangeStatus(dbToken.Id, tokenDto.UserId);
            }

            var newToken = _mapper.Map<RefreshToken>(tokenDto);
            newToken.CreatedBy = "moaz123";
            _repo.Insert(newToken);
            return true;
        }

        public bool IsExpireToken(string token)
        {
            var storedToken = GetByToken(token);

            if (storedToken == null || storedToken.CurrentState == 1 || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }

        public RefreshTokenDto GetByToken(string token)
        {
            var myToken = _repo.GetFirst(r => r.Token == token);
            return _mapper.Map<RefreshTokenDto>(myToken);
        }


    }


}
