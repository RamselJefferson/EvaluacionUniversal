using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Request
{
    public class UserCreateRequest
    {
        public  string? Name { get; set; }        
        public  string Password { get; set; }
        public  string Email { get; set; }
      
    }


}
