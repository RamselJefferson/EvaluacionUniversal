using AutoMapper;
using Core.Application.Dto;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Request;
using Core.Domain.Entities;
using FluentValidation;


namespace Infrastructure.Persistance.Services
{
    public class UserService : BaseService<UserCreateRequest, UserDto, User>, IUserService
    {
        private readonly IUserRepository _repo;

        private readonly IMapper _mapper;

        private readonly IJwtService _jwtService;
        private readonly IValidator<UserCreateRequest> _validator;




        public UserService(IUserRepository repo, IMapper mapper, IJwtService jwtService, IValidator<UserCreateRequest> validator) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _jwtService = jwtService;
            _validator = validator;
        }

        public async Task<UserDto> Login(UserRequest request)
        {

            var user = await _repo.GetByConditionAsync(x => x.Email == request.Email);

            if (user == null || !PasswordHashHelper.VerifyPassword(request.Password, user.Password))
            {
                throw new ValidationException("User or password is incorrect");
            }
            var userDto = _mapper.Map<UserDto>(user);

            return _jwtService.GetToken(userDto);

        }



        public override async Task<UserDto> AddAsync(UserCreateRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            request.Password = PasswordHashHelper.HashPassword(request.Password);
            User T = _mapper.Map<User>(request);
            T = await _repo.AddAsync(T);
            var userDto = _mapper.Map<UserDto>(T);
            return _jwtService.GetToken(userDto);
        }
    }
}
