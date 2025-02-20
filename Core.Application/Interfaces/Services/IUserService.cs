using Core.Application.Dto;
using Core.Application.Request;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IUserService : IBaseService<UserCreateRequest, UserDto, User>
    {
        Task<UserDto> AddAsync(UserCreateRequest request);

        Task<UserDto>  Login(UserRequest request);
    }
}
