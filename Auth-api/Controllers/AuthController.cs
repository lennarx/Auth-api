using Auth_api.Forms;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Auth_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public AuthController(IAuthenticationService service, ILogger<AuthController> logger, IConfiguration config)
        {
            _service = service;
            _logger = logger;
            _config = config;
            _service.SetKey((string)_config.GetValue(typeof(string), "Key"));
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate([FromBody] LoginForm user)
        {
            try
            {
                var token = await _service.Authenticate(user.UserName, user.Password);

                if (String.IsNullOrEmpty(token))
                    return Unauthorized();

                return Ok(token);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace, e.InnerException);
                throw new Exception("Ha ocurrido un error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserForm user)
        {
            try
            {
                await _service.CreateUser(user.UserName, user.Password, user.User);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException, e.Message, e.StackTrace);
                throw new Exception("Ha ocurrido un error al crear el usuario");
            }
        }
    }
}
