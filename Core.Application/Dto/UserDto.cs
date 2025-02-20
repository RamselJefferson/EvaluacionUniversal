using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; } 
        public required string Name { get; set; }
        public required string Email { get; set; }

        public string? Token { get; set; }
    }
}
