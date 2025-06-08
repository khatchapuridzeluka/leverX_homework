using leverX.Application.Interfaces.Services;
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
        public async Task<ActionResult<LoginResponseDto>> Login(LoginUserDto request)
        {
            _logger.LogInformation("User attempted login: {@Username}", request.Username);

            var result = await _userService.LoginAsync(request);

            _logger.LogInformation("User login successful: {@Username}",request.Username);

            return Ok(result);
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
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (!result)
                return BadRequest("User registration failed");

            _logger.LogInformation("User registered successfully: {@Username}", dto.Username);
            return Ok();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


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
