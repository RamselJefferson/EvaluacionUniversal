using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Services
{
    public class BaseService<CreateRequest, TDto, T> : IBaseService<CreateRequest, TDto, T>
        where CreateRequest : class
        where TDto : class
        where T : class
    {
        private readonly IBaseRepository<T> _repo;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<T> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var TList = await _repo.GetAllAsync();
            return _mapper.Map<List<TDto>>(TList);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            T T = await _repo.GetByIdAsync(id);
            return _mapper.Map<TDto>(T);
        }

        public virtual async Task<TDto> AddAsync(CreateRequest request)
        {
            T T = _mapper.Map<T>(request);
            T = await _repo.AddAsync(T);
            return _mapper.Map<TDto>(T);
        }

    }
}
