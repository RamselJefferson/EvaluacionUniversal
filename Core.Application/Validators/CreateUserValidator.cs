using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<UserCreateRequest>
    {
        public CreateUserValidator(IUserRepository userRepository,IConfiguration configuration)
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .Matches(configuration["regularExpression:email:pattern"]).WithMessage(configuration["regularExpression:email:message"])
                .MustAsync(async (x,ct)=> !await userRepository.ExistEmail(x)).WithMessage("Este email ya existe.");
            

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .Matches(configuration["regularExpression:password:pattern"])
                .WithMessage(configuration["regularExpression:password:message"]);
        }
    }
}
