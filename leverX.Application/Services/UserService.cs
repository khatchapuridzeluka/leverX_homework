using System.Formats.Tar;
using AutoMapper;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;
using leverX.Dtos.DTOs.Users;
using Microsoft.Extensions.Logging;

namespace leverX.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly ILogger<UserService> _logger;

        public UserService( IUserRepository userRepository, IMapper mapper, IJwtService jwtService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(RegisterUserDto dto)
        {
            if (await _userRepository.ExistsByUsername(dto.Username))
                return false;

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            try
            {
                await _userRepository.AddAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Username}", user.Username);
                throw new RegisterFailedException(ExceptionMessages.RegisterFailed);
            }
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return null;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginUserDto dto)
        {
            try
            {
                var user = await _userRepository.GetByUsername(dto.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                    return null;

                var token = _jwtService.GenerateToken(user);

                return new LoginResponseDto
                {
                    Token = token,
                    ExpiresAt = _jwtService.GetTokenExpiry(),
                    User = _mapper.Map<UserDto>(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for user: {Username}", dto.Username);
                throw new LoginFailedException(ExceptionMessages.LoginFailed);
            }
        }
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _userRepository.ExistsByUsername(username);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAll();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }

        public async Task<bool> ChangeRoleAsync(Guid userId, string newRole)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return false;

            user.Role = newRole;
            return await _userRepository.UpdateAsync(user);
        }
    }
}
