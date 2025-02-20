using Core.Application.Dto;
using Core.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IJwtService
    {
        UserDto GetToken(UserDto request);
    }
}
