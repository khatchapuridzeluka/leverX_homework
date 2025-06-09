using leverX.Application.Interfaces.Services;
using leverX.Domain.Exceptions;
using leverX.Dtos.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginUserDto request)
        {
            _logger.LogInformation("User attempted login: {@Username}", request.Username);

            try
            {
                var result = await _userService.LoginAsync(request);

                if (result == null)
                {
                    _logger.LogWarning("User login failed: {@Username}", request.Username);
                    return Unauthorized("Invalid username or password");
                }

                _logger.LogInformation("User login successful: {@Username}", request.Username);
                return Ok(result);
            }
            catch (LoginFailedException ex)
            {
                _logger.LogError(ex, "Login failed for {@Username}", request.Username);
                return BadRequest("Login failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login for {@Username}", request.Username);
                return StatusCode(500, "Unexpected error occurred.");
            }
        }


        /// <summary>
        /// Get user by id
        /// </summary>
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            try
            {
                var result = await _userService.RegisterAsync(dto);

                if (!result)
                {
                    _logger.LogWarning("Registration failedusername: {Username}", dto.Username);
                    return BadRequest();
                }

                _logger.LogInformation("User registered successfully: {Username}", dto.Username);
                return Ok();
            }
            catch (RegisterFailedException ex)
            {
                _logger.LogError(ex, "Registration failed due  for user: {Username}", dto.Username);
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during registration for user: {Username}", dto.Username);
                return StatusCode(500, "Unexpected error occurred.");
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [ProducesResponseType(401)]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// changes the user role
        /// </summary>
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(Guid id, [FromBody] string newRole)
        {
            var success = await _userService.ChangeRoleAsync(id, newRole);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
