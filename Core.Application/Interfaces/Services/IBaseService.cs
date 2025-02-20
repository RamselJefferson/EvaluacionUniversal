using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IBaseService<CreateRequest,TDto, T>
        where CreateRequest : class
        where TDto : class
        where T : class
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);


        Task<TDto> AddAsync(CreateRequest request);
    }
}
