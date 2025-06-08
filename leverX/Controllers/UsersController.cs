using leverX.Application.Interfaces.Services;
using leverX.Dtos.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginUserDto request)
        {
            var result = await _userService.LoginAsync(request);
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

            return Ok();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
    }
}
