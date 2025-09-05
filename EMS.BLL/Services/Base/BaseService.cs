using AutoMapper;
using EMS.BLL.Interface;
using EMS.BLL.Interface.Base;
using EMS.DAL.Contracts;
using EMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.Services.Base
{
 
    public class BaseService<T, DTO> : IBaseService<T, DTO> where T : BaseTable
    {

        private readonly IGenericRepository<T> _repo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public BaseService(IGenericRepository<T> repo, IMapper mapper, IUserService userService)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;      
        }

        public BaseService(IGenericRepository<T> repo, IMapper mapper, IUserService userService, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
            _cache = cache;
        }

        public IEnumerable<DTO> GetAll()
        {
            var dbObject = _repo.GetAll();

            return _mapper.Map<List<T>, List<DTO>>((List<T>)dbObject);
        }

        protected string GetCacheKey() => typeof(T).Name + "_list";

        public  IEnumerable<DTO> GetAllCached()
        {
            var cacheKey = GetCacheKey();

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<DTO> list))
            {           
                list = _mapper.Map<List<T>, List<DTO>>((List<T>)_repo.GetAll());

                var options = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromMinutes(2)) // ينتهي بعد 2 دقيقة من عدم الاستخدام
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)); // أقصى حد 5 دقائق

                _cache.Set(cacheKey, list, options);
            }

            return list;
        }

        public IQueryable<DTO> GetAllQueryable()
        {
            var dbObject = _repo.GetAllQueryable();
            return _mapper.Map<IQueryable<T>, IQueryable<DTO>>(dbObject);
        }

        public DTO GetByID(int Id)
        {
            var dbObject = _repo.GetByID(Id);
            return _mapper.Map<T, DTO>(dbObject);
        }

        public bool Insert(DTO entity)
        {
            var dbObject = _mapper.Map<DTO, T>(entity);
            dbObject.CreatedBy = _userService.GetLoggedInUser();
            return _repo.Insert(dbObject);
        }

        public bool Insert(DTO entity,out int Id)
        {
            var dbObject = _mapper.Map<DTO, T>(entity);
            dbObject.CreatedBy = _userService.GetLoggedInUser();
            return _repo.Insert(dbObject,out Id);
        }

        public bool Update(DTO entity)
        {
            var dbObject = _mapper.Map<DTO, T>(entity);
            dbObject.UpdatedBy = _userService.GetLoggedInUser();

            if (_repo.Update(dbObject)) 
            {
               _cache.Remove(GetCacheKey());
               return true;
            }         
            else
                return false;
        }

        public bool ChangeStatus(int ID, string userId, int status = 1)
        {
            return _repo.ChangeStatus(ID, _userService.GetLoggedInUser(), status);
        }



    }


}
