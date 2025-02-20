using AutoMapper;
using Core.Application.Dto;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Request;
using Core.Domain.Entities;
using FluentValidation;
using Infrastructure.Persistance.Services;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ValidationException = FluentValidation.ValidationException;

namespace EvaluacionUniversalTest.UserTest
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly Mock<IValidator<UserCreateRequest>> _mockValidator;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockJwtService = new Mock<IJwtService>();
            _mockValidator = new Mock<IValidator<UserCreateRequest>>();
            _userService = new UserService(_mockRepo.Object, _mockMapper.Object, _mockJwtService.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var request = new UserRequest { Email = "test@example.com", Password = "ValidPassword" };
            var user = new User
            {
                Name = "Test User",  
                Email = "test@example.com",
                Password = PasswordHashHelper.HashPassword("ValidPassword")
            };
            var userDto = new UserDto { Name = "Test User", Email = "test@example.com" };

            _mockRepo.Setup(r => r.GetByConditionAsync(It.IsAny<Expression<Func<User, bool>>>()))
                     .ReturnsAsync(user);

            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);
            _mockJwtService.Setup(j => j.GetToken(It.IsAny<UserDto>())).Returns(userDto);

            var result = await _userService.Login(request);

            Assert.NotNull(result);
            Assert.Equal("Test User", result.Name);
            _mockRepo.Verify(r => r.GetByConditionAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnToken_WhenUserIsValid()
        {
            var request = new UserCreateRequest { Name = "New User", Email = "test@example.com", Password = "NewPassword" };
            var user = new User
            {
                Name = "New User",
                Email = "test@example.com",
                Password = PasswordHashHelper.HashPassword("NewPassword")
            };
            var userDto = new UserDto { Name = "New User", Email = "test@example.com" };

            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UserCreateRequest>(), default))
                          .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserCreateRequest>())).Returns(user);

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

            _mockJwtService.Setup(j => j.GetToken(It.IsAny<UserDto>())).Returns(userDto);

            var result = await _userService.AddAsync(request);

            Assert.NotNull(result);
            Assert.Equal("New User", result.Name);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }



    }
}