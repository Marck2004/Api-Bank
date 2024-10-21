using Cobo.Api.Configuration.Auth;
using Cobo.Application.Dtos.Users;
using Cobo.Domain.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Cobo.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserInterface _userRepository;
        private readonly ITokenService _tokenService;
        public UsersController(IUserInterface context, ITokenService tokenservice)
        {
            _userRepository = context;
            _tokenService = tokenservice;
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetUser([FromBody] loginRequest request)
        {
            QueriesUserDto user = await _userRepository.GetUser(request.Email, request.Password);

            if (user is null)
                return NotFound("No existe ningun usuario con esas credenciales");

            string token = _tokenService.CreateToken(user);

            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<QueriesUserDto> users = await _userRepository.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        [Authorize]
        public Result AddUser([FromBody] CommandsUserDto newUser)
        {
            return _userRepository.AddUser(newUser);
        }

        [HttpPut("{id}")]
        [Authorize]
        public Result UpdateUser(Guid id, [FromBody] CommandsUserDto user)
        {
            return _userRepository.UpdateUser(id, user);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public Result DeleteUser([FromRoute] Guid id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}
